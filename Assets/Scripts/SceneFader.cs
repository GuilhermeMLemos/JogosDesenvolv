using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneFader : MonoBehaviour
{
    public Image image;
    public AnimationCurve fadeCurve;

    void Start()
    {
        StartCoroutine(FadeIn()); //chamando funcao do tipo coroutine
    }
    
    public void FadeTo(string scene)
    {
        StartCoroutine(FadeOut(scene));

    }

    IEnumerator FadeIn()  //coroutine
    {
        float t = 1f; //tempo

        while(t > 0f)
        {
            t -= Time.deltaTime;
            float a = fadeCurve.Evaluate(t);
            image.color = new Color(0f, 0f, 0f, a); //alterando o alpha da imagem
            yield return 0;
        }
    }

    IEnumerator FadeOut(string scene)  //coroutine
    {
        float t = 0f; //tempo

        while(t < 1f)
        {
            t += Time.deltaTime;
            float a = fadeCurve.Evaluate(t);
            image.color = new Color(0f, 0f, 0f, a); //alterando o alpha da imagem
            yield return 0;
        }

        SceneManager.LoadScene(scene); //carregando a scene
    }
}
