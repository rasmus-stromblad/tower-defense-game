using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;


public class SceneFader : MonoBehaviour
{
    public Image img;
    public AnimationCurve curve;

    void Start()
    {
        StartCoroutine(FadeIn());
    }

    public void FadeTo(string scene)
    {
        StartCoroutine(FadeOut(scene));
    }

    IEnumerator FadeIn()
    {
        float t = 1f;

        while(t > 0f)
        {
            t -= Time.deltaTime;
            // The alpha value is the y value on the curve when the x is t
            float a = curve.Evaluate(t);
            // Change the alpha value of the fade image 
            img.color = new Color(0f, 0f, 0f, a);
            // Wait a frame, then continue
            yield return 0;
        }
    }

    IEnumerator FadeOut(string scene)
    {
        float t = 0f;

        while(t < 1f)
        {
            t += Time.deltaTime;
            // The alpha value is the y value on the curve when the x is t
            float a = curve.Evaluate(t);
            // Change the alpha value of the fade image 
            img.color = new Color(0f, 0f, 0f, a);
            // Wait a frame, then continue
            yield return 0;
        }

        SceneManager.LoadScene(scene);
    }

}
