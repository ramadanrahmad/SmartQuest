using UnityEngine;

public class HeroAnimDebugger : MonoBehaviour
{
    public Animator heroAnimator;

    [Tooltip("Nama parameter float Speed (kalau ada).")]
    public string speedParamName = "Speed";

    [Tooltip("Nama parameter bool Grounded (kalau ada).")]
    public string groundedParamName = "Grounded";

    void Update()
    {
        if (heroAnimator == null) return;

        // Info state saat ini
        var stateInfo = heroAnimator.GetCurrentAnimatorStateInfo(0);
        string stateName = "unknown";
        // Ini agak kasar, tapi cukup buat debug
        if (stateInfo.IsName("Run")) stateName = "Run";
        else if (stateInfo.IsName("Idle")) stateName = "Idle";
        else if (stateInfo.IsName("HeroKnight_Attack 3")) stateName = "Attack3";
        else stateName = stateInfo.shortNameHash.ToString();

        float speedVal = 0f;
        bool groundedVal = false;

        if (!string.IsNullOrEmpty(speedParamName) && heroAnimator.HasParameterOfType(speedParamName, AnimatorControllerParameterType.Float))
        {
            speedVal = heroAnimator.GetFloat(speedParamName);
        }

        if (!string.IsNullOrEmpty(groundedParamName) && heroAnimator.HasParameterOfType(groundedParamName, AnimatorControllerParameterType.Bool))
        {
            groundedVal = heroAnimator.GetBool(groundedParamName);
        }

        Debug.Log($"[HeroAnimDebugger] State: {stateName}, Speed={speedVal}, Grounded={groundedVal}");
    }
}

// Helper extension
public static class AnimatorExtensions
{
    public static bool HasParameterOfType(this Animator self, string name, AnimatorControllerParameterType type)
    {
        foreach (var p in self.parameters)
        {
            if (p.type == type && p.name == name)
                return true;
        }
        return false;
    }
}
