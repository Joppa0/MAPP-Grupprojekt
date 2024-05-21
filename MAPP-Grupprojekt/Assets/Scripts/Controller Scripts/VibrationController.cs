using UnityEngine;
using UnityEngine.UI;
#if UNITY_ANDROID || UNITY_IOS
using CandyCoded.HapticFeedback;
#endif


public class VibrationController : MonoBehaviour
{


public void DefaultVibration()
    {
#if UNITY_ANDROID || UNITY_IOS
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
#else
        return;
#endif


    
}

