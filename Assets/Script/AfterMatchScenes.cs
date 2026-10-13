using UnityEngine;
using TMPro;

public class AftermathManager : MonoBehaviour
{
    [Header("Canvas Hasil")]
    public GameObject canvasMenang;
    public GameObject canvasKalah;

    [Header("Teks Mapel")]
    public TMP_Text teksMapelMenang;
    public TMP_Text teksMapelKalah;

    [Header("Teks Waktu")]
    public TMP_Text teksWaktuMenang;   // drag 'teks Waktu' di Canvas Menang
    public TMP_Text teksWaktuKalah;    // drag 'teks Waktu' di Canvas Kalah

    void Start()
    {
        if (canvasMenang != null) canvasMenang.SetActive(false);
        if (canvasKalah != null) canvasKalah.SetActive(false);

        string result = PlayerPrefs.GetString("Result", "NONE");
        string subject = PlayerPrefs.GetString("Subject", "NONE");

        // waktu yang terpakai (dalam detik) dan text mm:ss
        float timeUsed = PlayerPrefs.GetFloat("TimeUsed", -1f);
        string timeText = PlayerPrefs.GetString("TimeUsedText", "--:--");

        // Aktifkan canvas sesuai hasil
        if (result == "MENANG")
        {
            if (canvasMenang != null) canvasMenang.SetActive(true);
        }
        else if (result == "KALAH")
        {
            if (canvasKalah != null) canvasKalah.SetActive(true);
        }

        string mapelDisplay = GetMapelDisplayName(subject);

        if (teksMapelMenang != null) teksMapelMenang.text = mapelDisplay;
        if (teksMapelKalah != null) teksMapelKalah.text = mapelDisplay;

        // tampilkan waktu yang dipakai
        if (timeUsed >= 0f)
        {
            if (teksWaktuMenang != null) teksWaktuMenang.text = timeText;
            if (teksWaktuKalah != null) teksWaktuKalah.text = timeText;
        }
    }

    string GetMapelDisplayName(string subjectCode)
    {
        switch (subjectCode)
        {
            case "MTK": return "Matematika";
            case "KWN": return "Kewarganegaraan";
            case "BINDO": return "Bahasa Indonesia";
            case "BING": return "Bahasa Inggris";
            default:
                return "-";
        }
    }
}
