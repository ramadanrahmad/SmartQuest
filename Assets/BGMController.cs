using UnityEngine;

public class BGMController : MonoBehaviour
{
    public static BGMController instance;
    public AudioSource bgm;

    void Awake()
    {
        // Jika instance sudah ada, hancurkan yang baru
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        // Hanya play jika belum playing
        if (!bgm.isPlaying)
            bgm.Play();
    }

    public void SetMute(bool mute)
    {
        if (mute)
            bgm.Pause();
        else
            bgm.UnPause();
    }

    public bool IsMuted()
    {
        return !bgm.isPlaying;
    }
}
