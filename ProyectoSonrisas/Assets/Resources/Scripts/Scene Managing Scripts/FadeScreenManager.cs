using System.Collections;
using UnityEngine;

public class FadeScreenManager : MonoBehaviour
{
    public bool fadeOnStart = true;
    public float fadeInDuration = 2f;
    public float fadeOutDuration = .5f;
    public Color fadeColor;
    private MeshRenderer rend;


    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<MeshRenderer>();
        if (fadeOnStart)
        {
            FadeIn();
        }
    }

    public void FadeIn()
    {
        Fade(1, 0);
    }

    public void FadeOut()
    {
        Fade(0, 1);
    }

    public void Fade(float alphaIn, float alphaOut)
    {
        StartCoroutine(FadeRoutine(alphaIn, alphaOut));
    }

    public IEnumerator FadeRoutine(float alphaIn, float alphaOut)
    {
        float timer = 0;
        if (alphaIn == 1)
        {
            while (timer <= fadeInDuration)
            {
                Color newColor = fadeColor;
                newColor.a = Mathf.Lerp(alphaIn, alphaOut, timer / fadeInDuration);

                rend.material.SetColor("_BaseColor", newColor);

                timer += Time.deltaTime;
                yield return null;
            }
        }
        else if(alphaIn == 0)
        {
            while (timer <= fadeOutDuration)
            {
                Color newColor = fadeColor;
                newColor.a = Mathf.Lerp(alphaIn, alphaOut, timer / fadeOutDuration);

                rend.material.SetColor("_BaseColor", newColor);

                timer += Time.deltaTime;
                yield return null;
            }
        }
        

        Color newColor2 = fadeColor;
        newColor2.a = alphaOut;

        rend.material.SetColor("_BaseColor", newColor2);
    }
}
