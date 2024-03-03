using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ObjectFadingManager : MonoBehaviour
{
    public UnityEvent eventa;
    
    public void FadeGameObjectIn(GameObject fadeObj, float timeToFade, UnityEvent postFade)
    {
        StartCoroutine(FadeProcess(fadeObj, 0, 1, timeToFade,postFade));
    }

    public void FadeGameObjectOut(GameObject fadeObj, float timeToFade, UnityEvent postFade)
    {
        StartCoroutine(FadeProcess(fadeObj, 1, 0, timeToFade,postFade));
    }

    private IEnumerator FadeProcess(GameObject fadeObj, float startA, float endA, float timeToFade, UnityEvent postFade)
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
        if(postFade != null)
            postFade.Invoke();
    }

    private void ChangeObjectAlpha(GameObject fadeObj, float newAlpha)
    {
        if (fadeObj == null) return;
        Color newColor = fadeObj.GetComponent<SpriteRenderer>().material.color;
        newColor = new Color(newColor.r, newColor.g, newColor.b, newAlpha);
        fadeObj.GetComponent<SpriteRenderer>().material.color = newColor;
    }
}
