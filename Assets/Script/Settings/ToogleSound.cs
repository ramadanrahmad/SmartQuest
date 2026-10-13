using UnityEngine;
using UnityEngine.UI;

public class ToggleSound : MonoBehaviour
{
    public Image icon;
    public Sprite iconOn;
    public Sprite iconOff;

    bool isMuted;

    void Start()
    {
        if (BGMController.instance != null)
            isMuted = BGMController.instance.IsMuted();

        UpdateIcon();
    }

    public void Toggle()
    {
        if (BGMController.instance == null)
        {
            Debug.Log("BGMController instance NOT FOUND!");
            return;
        }

        isMuted = !isMuted;
        BGMController.instance.SetMute(isMuted);

        UpdateIcon();
    }

    void UpdateIcon()
    {
        icon.sprite = isMuted ? iconOff : iconOn;
    }
}
