using UnityEngine;
using UnityEngine.SceneManagement;

public class SFXandScene : MonoBehaviour
{
    public AudioSource audioSource;     // sumber audio
    public AudioClip clickSound;        // sfx button
    public float delayBeforeLoad = 0.1f; // lama delay

    // Dipanggil dari button
    public void PlaySFXAndLoadScene(string sceneName)
    {
        StartCoroutine(PlayAndLoad(sceneName));
    }

    private System.Collections.IEnumerator PlayAndLoad(string sceneName)
    {
        // mainkan suara
        if (audioSource != null && clickSound != null)
        {
            audioSource.PlayOneShot(clickSound);
        }

        // tunggu
        yield return new WaitForSeconds(delayBeforeLoad);

        // ganti scene
        SceneManager.LoadScene(sceneName);
    }

    // Keluar Aplikasi tanpa delay SFX
    public void QuitApp()
    {
        if (audioSource != null && clickSound != null)
            audioSource.PlayOneShot(clickSound); // langsung main

        StartCoroutine(QuitAfterSound());
    }

    private System.Collections.IEnumerator QuitAfterSound()
    {
        // tunggu durasi SFX agar terdengar penuh
        if (clickSound != null)
            yield return new WaitForSeconds(clickSound.length * 0.9f);
        else
            yield return null;

        Application.Quit();

        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}

