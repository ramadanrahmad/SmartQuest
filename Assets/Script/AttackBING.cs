using UnityEngine;
using UnityEngine.SceneManagement;

public class AttackBING : MonoBehaviour
{
    [Header("Teleport Settings")]
    public Collider2D monsterCollider;   // ⬅ Collider2D untuk 2D
    public float teleportOffset = 1f;

    [Header("Animator Player")]
    public Animator playerAnimator;
    public string attackTriggerName = "Attack";

    [Header("SFX")]
    public AudioSource sfxSource;
    public AudioClip attackSFX;

    [Header("Scene Settings")]
    public string nextScene = "Aftermath";

    [Tooltip("Delay sebelum player di-teleport setelah scene mulai")]
    public float startDelay = 0.7f;

    [Tooltip("Delay setelah attack sebelum pindah scene")]
    public float delayBeforeScene = 0.6f;

    void Start()
    {
        Invoke(nameof(StartTeleportSequence), startDelay);
    }

    void StartTeleportSequence()
    {
        TeleportToMonster();
        Invoke(nameof(DoAttack), 0.1f);
    }

    void TeleportToMonster()
    {
        if (monsterCollider == null) return;

        // Ambil posisi tengah collider monster
        Vector3 pos = monsterCollider.bounds.center;

        // Pindahkan player ke kiri monster
        pos.x -= teleportOffset;

        // Pastikan posisi z tetap (agar tidak menggeser kamera)
        pos.z = transform.position.z;

        // Teleport player
        transform.position = pos;
    }

    void DoAttack()
    {
        if (playerAnimator != null)
            playerAnimator.SetTrigger(attackTriggerName);

        if (sfxSource != null && attackSFX != null)
            sfxSource.PlayOneShot(attackSFX);

        Invoke(nameof(EndScene), delayBeforeScene);
    }

    void EndScene()
    {
        SceneManager.LoadScene(nextScene);
    }
}
