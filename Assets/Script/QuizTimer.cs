using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class QuizTimer : MonoBehaviour
{
    [Header("UI")]
    public TMP_Text timerText;      // drag Text (TMP)

    [Header("Pengaturan Waktu")]
    public float totalTimeInSeconds = 300f;   // 5 menit

    private float remainingTime;
    private bool isRunning = true;

    private QuizManager quiz;

    void Start()
    {
        quiz = FindObjectOfType<QuizManager>(); // cari QuizManager di scene

        remainingTime = totalTimeInSeconds;
        UpdateTimerText();
    }

    void Update()
    {
        if (!isRunning) return;

        remainingTime -= Time.deltaTime;

        if (remainingTime <= 0f)
        {
            remainingTime = 0f;
            isRunning = false;
            UpdateTimerText();
            OnTimeUp();   // waktu habis -> kalah
            return;
        }

        UpdateTimerText();
    }

    void UpdateTimerText()
    {
        if (timerText == null) return;

        timerText.text = FormatTime(remainingTime);
    }

    string FormatTime(float time)
    {
        if (time < 0) time = 0;
        int minutes = Mathf.FloorToInt(time / 60f);
        int seconds = Mathf.FloorToInt(time % 60f);
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    // ====== SIMPAN WAKTU YANG TERPAKAI ======
    public void SaveTime()
    {
        float elapsed = totalTimeInSeconds - remainingTime;
        if (elapsed < 0) elapsed = 0;

        PlayerPrefs.SetFloat("TimeUsed", elapsed);
        PlayerPrefs.SetString("TimeUsedText", FormatTime(elapsed));
        PlayerPrefs.Save();
    }

    // ====== WAKTU HABIS -> KALAH ======
    void OnTimeUp()
    {
        Debug.Log("Waktu habis! Pemain kalah.");

        // simpan waktu saat habis
        SaveTime();

        if (quiz != null)
        {
            PlayerPrefs.SetString("Result", "KALAH");
            PlayerPrefs.SetString("Subject", quiz.subjectCode);
            PlayerPrefs.Save();

            SceneManager.LoadScene(quiz.loseSceneName);
        }
        else
        {
            Debug.LogError("QuizTimer: QuizManager tidak ditemukan di scene!");
        }
    }

    // Opsional
    public void StopTimer() { isRunning = false; }
    public void ResumeTimer() { if (remainingTime > 0f) isRunning = true; }
    public void ResetTimer()
    {
        remainingTime = totalTimeInSeconds;
        isRunning = true;
        UpdateTimerText();
    }
}
