using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fader : MonoBehaviour
{
    public Image img;
    public float vel = 2f;
    float t;
    bool waitingFade;
    bool isFadeIn;
    Color c = Color.black;

    public void FadeIn()
    {
        isFadeIn = true;
        StartCoroutine("Fade");
    }

    public void FadeOut()
    {
        isFadeIn = false;
        StartCoroutine("Fade");
    }
    IEnumerator Fade()
    {
        t = 0;
        while (t < 1)
        {
            t += Time.deltaTime * vel;
            if (isFadeIn)
            {
                c.a = Mathf.Lerp(1, 0, t);

            }
            else
            {
                c.a = Mathf.Lerp(0, 1, t);
            }

            yield return null;
        }

        if (isFadeIn)
            img.enabled = false;
    }

    public void FadeArterTime(float v)
    {
        if (waitingFade)
            return;
        waitingFade = true;
        StartCoroutine(FadeInArterTime(v));
    }

    public IEnumerator FadeInArterTime(float v)
    {
        yield return new WaitForSeconds(v);
        FadeIn();
        waitingFade = false;
    }
}
