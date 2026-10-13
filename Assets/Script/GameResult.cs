using UnityEngine;

public class GameResult : MonoBehaviour
{
    [Header("Canvas Menang (CM)")]
    public GameObject cmBIndonesia;
    public GameObject cmMatematika;
    public GameObject cmKewarganegaraan;
    public GameObject cmBInggris;

    [Header("Canvas Kalah (CK)")]
    public GameObject ckBIndonesia;
    public GameObject ckMatematika;
    public GameObject ckKewarganegaraan;
    public GameObject ckBInggris;

    void Start()
    {
        // Matikan semua dulu
        DisableAllCanvas();

        // Baca hasil dan mapel dari scene sebelumnya
        string result = PlayerPrefs.GetString("Result", "NONE");  // MENANG / KALAH
        string subject = PlayerPrefs.GetString("Subject", "NONE");  // BINDO / MTK / KWN / BING

        if (result == "MENANG")
        {
            ShowWinCanvas(subject);
        }
        else if (result == "KALAH")
        {
            ShowLoseCanvas(subject);
        }
        else
        {
            Debug.LogWarning($"GameResult: Result '{result}' tidak dikenali.");
            // Kalau mau, bisa tampilkan salah satu canvas default di sini.
        }
    }

    void DisableAllCanvas()
    {
        // Semua CM
        if (cmBIndonesia != null) cmBIndonesia.SetActive(false);
        if (cmMatematika != null) cmMatematika.SetActive(false);
        if (cmKewarganegaraan != null) cmKewarganegaraan.SetActive(false);
        if (cmBInggris != null) cmBInggris.SetActive(false);

        // Semua CK
        if (ckBIndonesia != null) ckBIndonesia.SetActive(false);
        if (ckMatematika != null) ckMatematika.SetActive(false);
        if (ckKewarganegaraan != null) ckKewarganegaraan.SetActive(false);
        if (ckBInggris != null) ckBInggris.SetActive(false);
    }

    void ShowWinCanvas(string subject)
    {
        switch (subject)
        {
            case "BINDO":
                if (cmBIndonesia != null) cmBIndonesia.SetActive(true);
                break;
            case "MTK":
                if (cmMatematika != null) cmMatematika.SetActive(true);
                break;
            case "KWN":
                if (cmKewarganegaraan != null) cmKewarganegaraan.SetActive(true);
                break;
            case "BING":
                if (cmBInggris != null) cmBInggris.SetActive(true);
                break;
            default:
                Debug.LogWarning($"GameResult: Subject '{subject}' tidak dikenali (MENANG).");
                break;
        }
    }

    void ShowLoseCanvas(string subject)
    {
        switch (subject)
        {
            case "BINDO":
                if (ckBIndonesia != null) ckBIndonesia.SetActive(true);
                break;
            case "MTK":
                if (ckMatematika != null) ckMatematika.SetActive(true);
                break;
            case "KWN":
                if (ckKewarganegaraan != null) ckKewarganegaraan.SetActive(true);
                break;
            case "BING":
                if (ckBInggris != null) ckBInggris.SetActive(true);
                break;
            default:
                Debug.LogWarning($"GameResult: Subject '{subject}' tidak dikenali (KALAH).");
                break;
        }
    }
}
