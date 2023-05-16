using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneFader : MonoBehaviour
{

    public Image image;
    public float speed;
    public AnimationCurve curve;
    
    private void Start()
    {
        StartCoroutine(FadeIn());
    }

    public void FadeTo(string scene)
    {
        StartCoroutine(FadeOut(scene));
    }
   
    //Fade in new screen
    IEnumerator FadeIn()
    {
        float t = 1f;

        while (t > 0)
        {
            t -= Time.deltaTime * speed;
            float a = curve.Evaluate(t);
            image.color = new Color(0,0,0, a);
            yield return 0;
        }
    }
    
    //Fade out of current screen
    IEnumerator FadeOut(string scene)
    {
        float t = 0f;

        while (t < 1)
        {
            t += Time.deltaTime * speed;
            float a = curve.Evaluate(t);
            image.color = new Color(0,0,0, a);
            yield return 0;
        }
        
        SceneManager.LoadScene(scene);
    }
    
}
