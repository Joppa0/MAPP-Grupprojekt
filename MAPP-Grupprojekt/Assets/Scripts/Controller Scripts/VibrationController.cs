using UnityEngine;
using UnityEngine.UI;
#if UNITY_ANDROID || UNITY_IOS
using CandyCoded.HapticFeedback;
#endif


public class VibrationController : MonoBehaviour
{
#if UNITY_ANDROID || UNITY_IOS
    public void DefaultVibration()
    {

        Debug.Log("Default Vibration performed!");
            Handheld.Vibrate();
    }

    public void LightVibration()
    {
        Debug.Log("Light Vibration performed!");
        //HapticFeedback.LightFeedback();
    }

    public void MediumVibration()
    {
        Debug.Log("Meduim Vibration performed!");
        //HapticFeedback.MediumFeedback();
    }

    public void HeavyVibration()
    {
        Debug.Log("Heavy Vibration performed!");
        //HapticFeedback.HeavyFeedback();
    }

#endif

    
}


