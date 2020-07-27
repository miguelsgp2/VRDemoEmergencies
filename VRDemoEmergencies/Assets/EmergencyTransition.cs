using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EmergencyTransition : Singleton<EmergencyTransition>
{
    private EmergencyScenario emergencyToLoad;
    public Image img;
    public AnimationCurve curve;

    //private void Start()
    //{

        
    //}

    //public void LoadSceneFading(string scene)
    //{
    //    StartCoroutine(FadeOut(scene));
    //}

    public void LoadEmergencyFading(EmergencyScenario nextEmergencyScenario)
    {
        emergencyToLoad = nextEmergencyScenario;
        StartCoroutine(FadeOut());
    }



    IEnumerator FadeIn()
    {
        float t = 3f;
        while (t > 0f)
        {
            t -= Time.deltaTime;
            float a = curve.Evaluate(t);
            img.color = new Color(0f, 0f, 0f, a);
            yield return null;
        }
    }

    IEnumerator FadeOut()
    {
        float t = 0f;

        while (t < 3f)
        {
            t += Time.deltaTime;
            float a = curve.Evaluate(t);
            img.color = new Color(0f, 0f, 0f, a);
            yield return null;
        }
        StartCoroutine(FadeIn());
        emergencyToLoad.InitiateScenario();
    }
}
