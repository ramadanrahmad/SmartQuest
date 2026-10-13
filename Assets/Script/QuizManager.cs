using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

[System.Serializable]
public class Question
{
    [TextArea]
    public string questionText;

    public string[] answers = new string[4];
    public int correctAnswerIndex;
}

public class QuizManager : MonoBehaviour
{
    // ==========================
    // UI REFERENSI
    // ==========================
    [Header("UI Referensi")]
    public TMP_Text questionTextUI;
    public TMP_Text[] answerTextUI;

    // ==========================
    // SUBJECT (MAPEL)
    // ==========================
    [Header("Subject / Mapel")]
    [Tooltip("BINDO / MTK / KWN / BING")]
    public string subjectCode = "KWN";

    // ==========================
    // DAFTAR SOAL
    // ==========================
    [Header("Daftar Soal")]
    public List<Question> questions = new List<Question>();

    // ==========================
    // HP / NYAWA
    // ==========================
    [Header("HP / Nyawa")]
    public int maxPlayerLives = 3;
    public int maxMonsterLives = 3;

    public GameObject[] playerHearts;
    public GameObject[] monsterHearts;

    private int playerLives;
    private int monsterLives;

    // ==========================
    // SCENE RESULT
    // ==========================
    [Header("Scene Transisi")]
    public string winSceneName = "Pertempuran";
    public string loseSceneName = "Pertempuran";

    // ==========================
    // INTERNAL STATE
    // ==========================
    private List<int> questionOrder = new List<int>();
    private int[] answerOrder = new int[4];
    private int currentQuestionIndex = 0;
    private bool isGameOver = false;

    // ==========================================================
    // START
    // ==========================================================
    void Start()
    {
        playerLives = maxPlayerLives;
        monsterLives = maxMonsterLives;
        UpdateAllHearts();

        // Acak urutan soal
        questionOrder.Clear();
        for (int i = 0; i < questions.Count; i++)
            questionOrder.Add(i);

        for (int i = 0; i < questionOrder.Count; i++)
        {
            int r = Random.Range(i, questionOrder.Count);
            (questionOrder[i], questionOrder[r]) = (questionOrder[r], questionOrder[i]);
        }

        ShowQuestion();
    }

    // ==========================================================
    // TAMPILKAN SOAL
    // ==========================================================
    void ShowQuestion()
    {
        if (currentQuestionIndex >= questionOrder.Count || isGameOver)
            return;

        int realIndex = questionOrder[currentQuestionIndex];
        Question q = questions[realIndex];

        questionTextUI.text = q.questionText;

        // Acak jawaban
        for (int i = 0; i < answerOrder.Length; i++)
            answerOrder[i] = i;

        for (int i = 0; i < answerOrder.Length; i++)
        {
            int r = Random.Range(i, answerOrder.Length);
            (answerOrder[i], answerOrder[r]) = (answerOrder[r], answerOrder[i]);
        }

        // Tampilkan teks jawaban
        for (int i = 0; i < answerTextUI.Length; i++)
        {
            int answerIndex = answerOrder[i];
            answerTextUI[i].text = q.answers[answerIndex];
        }
    }

    // ==========================================================
    // SAAT JAWABAN DIPILIH
    // ==========================================================
    public void OnAnswerSelected(int buttonIndex)
    {
        if (isGameOver)
            return;

        int realIndex = questionOrder[currentQuestionIndex];
        Question q = questions[realIndex];

        int chosenAnswerIndex = answerOrder[buttonIndex];

        if (chosenAnswerIndex == q.correctAnswerIndex)
        {
            DamageMonster(1);
            if (isGameOver) return;

            currentQuestionIndex++;
            ShowQuestion();
        }
        else
        {
            DamagePlayer(1);
        }
    }

    // ==========================================================
    // FUNGSI DAMAGE PLAYER
    // ==========================================================
    void DamagePlayer(int amount)
    {
        playerLives -= amount;
        if (playerLives < 0) playerLives = 0;

        UpdatePlayerHearts();

        if (playerLives <= 0)
        {
            isGameOver = true;

            // simpan waktu dari timer (jika ada)
            QuizTimer timer = FindObjectOfType<QuizTimer>();
            if (timer != null)
                timer.SaveTime();

            PlayerPrefs.SetInt("Stars", 0);

            PlayerPrefs.SetString("Result", "KALAH");
            PlayerPrefs.SetString("Subject", subjectCode);  // subjectCode yang tadi kita buat
            PlayerPrefs.Save();

            SceneManager.LoadScene(loseSceneName);
        }
    }

    // ==========================================================
    // FUNGSI DAMAGE MONSTER
    // ==========================================================
    void DamageMonster(int amount)
    {
        monsterLives -= amount;
        if (monsterLives < 0) monsterLives = 0;

        UpdateMonsterHearts();

        if (monsterLives <= 0)
        {
            isGameOver = true;

            // simpan waktu (kalau ada timer)
            QuizTimer timer = FindObjectOfType<QuizTimer>();
            if (timer != null)
                timer.SaveTime();

            // SIMPAN SISA HATI PLAYER
            PlayerPrefs.SetInt("Stars", playerLives);   // <= tambahan baru

            // SIMPAN RESULT & SUBJECT
            PlayerPrefs.SetString("Result", "MENANG");
            PlayerPrefs.SetString("Subject", subjectCode);
            PlayerPrefs.Save();

            SceneManager.LoadScene(winSceneName);
        }
    }

    // ==========================================================
    // UPDATE HATI
    // ==========================================================
    void UpdateAllHearts()
    {
        UpdatePlayerHearts();
        UpdateMonsterHearts();
    }

    void UpdatePlayerHearts()
    {
        if (playerHearts == null) return;

        for (int i = 0; i < playerHearts.Length; i++)
            playerHearts[i]?.SetActive(i < playerLives);
    }

    void UpdateMonsterHearts()
    {
        if (monsterHearts == null) return;

        for (int i = 0; i < monsterHearts.Length; i++)
            monsterHearts[i]?.SetActive(i < monsterLives);
    }
}
