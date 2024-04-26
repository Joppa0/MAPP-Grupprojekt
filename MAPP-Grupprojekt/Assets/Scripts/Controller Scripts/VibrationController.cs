using UnityEngine;
using UnityEngine.UI;
using CandyCoded.HapticFeedback;


public class VibrationController : MonoBehaviour
{

    public void DefaultVibration()
    {
        Debug.Log("Default Vibration performed!");
            Handheld.Vibrate();
    }

    public void LightVibration()
    {
        Debug.Log("Light Vibration performed!");
        HapticFeedback.LightFeedback();
    }

    public void MediumVibration()
    {
        Debug.Log("Meduim Vibration performed!");
        HapticFeedback.MediumFeedback();
    }

    public void HeavyVibration()
    {
        Debug.Log("Heavy Vibration performed!");
        HapticFeedback.HeavyFeedback();
    }


}
