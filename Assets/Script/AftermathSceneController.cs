using UnityEngine;
using UnityEngine.SceneManagement;

public class AftermathSceneController : MonoBehaviour
{
    [Header("Nama Scene Soal per Mapel")]
    [Tooltip("Scene soal Bahasa Indonesia")]
    public string sceneBIndonesia;   // mis: "P_BIndonesia"

    [Tooltip("Scene soal Matematika")]
    public string sceneMatematika;   // mis: "P_Matematika"

    [Tooltip("Scene soal Kewarganegaraan")]
    public string sceneKewarganegaraan; // mis: "P_Kewarganegaraan"

    [Tooltip("Scene soal Bahasa Inggris")]
    public string sceneBInggris;     // mis: "P_BInggris"

    // Dipanggil dari tombol Reward / Retry
    public void OnRewardButtonPressed()
    {
        string subject = PlayerPrefs.GetString("Subject", "NONE"); // BINDO / MTK / KWN / BING
        string targetScene = "";

        switch (subject)
        {
            case "BINDO":
                targetScene = sceneBIndonesia;
                break;
            case "MTK":
                targetScene = sceneMatematika;
                break;
            case "KWN":
                targetScene = sceneKewarganegaraan;
                break;
            case "BING":
                targetScene = sceneBInggris;
                break;
            default:
                Debug.LogWarning($"AftermathSceneController: Subject '{subject}' tidak dikenali.");
                break;
        }

        if (!string.IsNullOrEmpty(targetScene))
        {
            SceneManager.LoadScene(targetScene);
        }
        else
        {
            Debug.LogWarning("AftermathSceneController: targetScene masih kosong, cek pengaturan di Inspector.");
        }
    }

    // Opsional: tombol Home
    public void OnHomeButtonPressed()
    {
        SceneManager.LoadScene("MainMenu"); // ganti dengan nama scene menu utama kamu
    }
}
