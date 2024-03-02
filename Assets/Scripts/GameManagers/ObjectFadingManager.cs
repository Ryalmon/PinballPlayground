using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFadingManager : MonoBehaviour
{
    public void FadeGameObjectIn(GameObject fadeObj, float timeToFade)
    {
        StartCoroutine(FadeProcess(fadeObj, 0, 1, timeToFade));
    }

    public void FadeGameObjectOut(GameObject fadeObj, float timeToFade)
    {
        StartCoroutine(FadeProcess(fadeObj, 1, 0, timeToFade));
    }

    private IEnumerator FadeProcess(GameObject fadeObj, float startA, float endA, float timeToFade)
    {
        float fadeProcessTimer = 0;
        float currentAlpha;

        while (fadeProcessTimer < 1)
        {
            fadeProcessTimer += Time.deltaTime / timeToFade;
            currentAlpha = Mathf.Lerp(startA, endA, fadeProcessTimer);
            ChangeObjectAlpha(fadeObj, currentAlpha);
            yield return null;
        }
    }

    private void ChangeObjectAlpha(GameObject fadeObj, float newAlpha)
    {
        Color newColor = fadeObj.GetComponent<SpriteRenderer>().material.color;
        newColor = new Color(newColor.r, newColor.g, newColor.b, newAlpha);
        fadeObj.GetComponent<SpriteRenderer>().material.color = newColor;
    }
}
