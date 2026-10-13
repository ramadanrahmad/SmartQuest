using UnityEngine;
using UnityEngine.SceneManagement;

public class Attack : MonoBehaviour
{
    [Header("Teleport Settings")]
    public Transform monster;
    public float teleportOffset = 1f;

    [Header("Animator Player")]
    public Animator playerAnimator;      // DRAG animator milik player
    public string attackTriggerName = "Attack";

    [Header("SFX")]
    public AudioSource sfxSource;
    public AudioClip attackSFX;

    [Header("Scene Settings")]
    public string nextScene = "Aftermath";

    [Tooltip("Delay sebelum player di-teleport setelah scene mulai")]
    public float startDelay = 0.7f;   // ⬅ Delay AWAL scene

    [Tooltip("Delay setelah attack sebelum pindah scene")]
    public float delayBeforeScene = 0.6f;

    void Start()
    {
        // Tambahkan delay di awal scene sebelum teleport
        Invoke(nameof(StartTeleportSequence), startDelay);
    }

    void StartTeleportSequence()
    {
        TeleportToMonster();

        // Sedikit delay sebelum animasi attack dimulai
        Invoke(nameof(DoAttack), 0.1f);
    }

    void TeleportToMonster()
    {
        if (monster == null) return;

        Vector3 pos = monster.position;
        pos.x -= teleportOffset;
        transform.position = pos;
    }

    void DoAttack()
    {
        // 🔥 Memicu animasi Attack
        if (playerAnimator != null)
            playerAnimator.SetTrigger(attackTriggerName);

        // 🔊 Mainkan SFX
        if (sfxSource != null && attackSFX != null)
            sfxSource.PlayOneShot(attackSFX);

        // Delay sebelum masuk scene berikutnya
        Invoke(nameof(EndScene), delayBeforeScene);
    }

    void EndScene()
    {
        SceneManager.LoadScene(nextScene);
    }
}
