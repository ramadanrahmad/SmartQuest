using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterClick : MonoBehaviour
{
    public string sceneName;
    public AudioSource audioSource;
    public AudioClip clickSound;
    public float delayBeforeLoad = 0.1f;

    private void OnMouseDown()
    {
        StartCoroutine(PlayAndLoad());
    }

    private System.Collections.IEnumerator PlayAndLoad()
    {
        // mainkan sfx
        if (audioSource != null && clickSound != null)
            audioSource.PlayOneShot(clickSound);

        // tunggu
        yield return new WaitForSeconds(delayBeforeLoad);

        // ganti scene
        SceneManager.LoadScene(sceneName);
    }
}
