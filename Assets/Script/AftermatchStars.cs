using UnityEngine;

public class AftermathStars : MonoBehaviour
{
    [Header("Star Objects")]
    public GameObject star1;
    public GameObject star2;
    public GameObject star3;

    void Start()
    {
        int stars = PlayerPrefs.GetInt("Stars", 0);

        // Matikan semua dulu
        star1.SetActive(false);
        star2.SetActive(false);
        star3.SetActive(false);

        // Tampilkan sesuai jumlah bintang
        if (stars >= 1) star1.SetActive(true);
        if (stars >= 2) star2.SetActive(true);
        if (stars >= 3) star3.SetActive(true);
    }
}
