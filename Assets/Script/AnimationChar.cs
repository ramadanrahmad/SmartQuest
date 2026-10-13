using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinCutsceneController : MonoBehaviour
{
    [Header("Referensi Karakter")]
    public Transform heroKnight;      // drag HeroKnight
    public Transform blackKnight;     // drag black_knight

    [Tooltip("Posisi berhenti hero relatif terhadap black_knight.")]
    public Vector3 heroOffsetFromBlackKnight = new Vector3(-1f, 0f, 0f);

    [Header("Gerakan")]
    public float moveSpeed = 3f;
    public float stopDistance = 0.05f;

    [Header("Animator Hero (cutscene controller)")]
    public Animator heroAnimator;
    public string runStateName = "HeroKnight_Run";
    public string idleStateName = "HeroKnight_Idle";
    public string attackStateName = "HeroKnight_Attack3"; // atau Attack1/2 sesuai yang kamu pakai

    [Header("SFX")]
    public AudioSource sfxSource;
    public AudioClip slashSfx;

    [Header("Timing")]
    public float waitBeforeSlash = 2f;
    public float waitAfterSlash = 1.5f;

    [Header("Scene Tujuan")]
    public string nextSceneName;

    private bool started = false;

    void Start()
    {
        StartCutscene();
    }

    public void StartCutscene()
    {
        if (started) return;
        started = true;
        StartCoroutine(CutsceneRoutine());
    }

    private IEnumerator CutsceneRoutine()
    {
        if (heroKnight == null || blackKnight == null || heroAnimator == null)
        {
            Debug.LogError("WinCutsceneController: reference belum lengkap.");
            yield break;
        }

        // posisi target di samping monster
        Vector3 targetPos = blackKnight.position + heroOffsetFromBlackKnight;

        // 1. Play animasi RUN
        heroAnimator.Play(runStateName, 0, 0f);

        // 2. Gerakkan hero ke target
        while (Vector3.Distance(heroKnight.position, targetPos) > stopDistance)
        {
            heroKnight.position = Vector3.MoveTowards(
                heroKnight.position,
                targetPos,
                moveSpeed * Time.deltaTime
            );
            yield return null;
        }

        heroKnight.position = targetPos;

        // 3. Berhenti → Idle
        heroAnimator.Play(idleStateName, 0, 0f);

        // 4. Tunggu sebelum slash
        yield return new WaitForSeconds(waitBeforeSlash);

        // 5. Attack
        heroAnimator.Play(attackStateName, 0, 0f);

        if (sfxSource != null && slashSfx != null)
            sfxSource.PlayOneShot(slashSfx);

        // 6. Tunggu setelah slash
        yield return new WaitForSeconds(waitAfterSlash);

        // 7. Pindah scene
        if (!string.IsNullOrEmpty(nextSceneName))
            SceneManager.LoadScene(nextSceneName);
    }
}
