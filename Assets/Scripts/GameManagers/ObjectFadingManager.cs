using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ObjectFadingManager : MonoBehaviour
{
    /*public delegate void Activate();
    public static event Activate active;

    public static event Action<int> act;*/

    /*active +=Placed;
        active?.Invoke();
    act?.Invoke(1);*/
    public Coroutine FadeGameObjectIn(GameObject fadeObj, float timeToFade, UnityEvent postFade)
    {
        return StartCoroutine(FadeProcess(fadeObj, 0, 1, timeToFade,postFade));
    }

    public Coroutine FadeGameObjectOut(GameObject fadeObj, float timeToFade, UnityEvent postFade)
    {
        return StartCoroutine(FadeProcess(fadeObj, 1, 0, timeToFade,postFade));
    }

    public Coroutine FadeGameObjectToRed(GameObject fadeObj, float timeToFade)
    {
        return StartCoroutine(FadeToRed(fadeObj, timeToFade));
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
        //Color newColor = fadeObj.GetComponent<SpriteRenderer>().material.color;
        Color newColor = fadeObj.GetComponentInChildren<SpriteRenderer>().color;
        newColor = new Color(newColor.r, newColor.g, newColor.b, newAlpha);
        //fadeObj.GetComponent<SpriteRenderer>().material.color = newColor;
        fadeObj.GetComponentInChildren<SpriteRenderer>().color = newColor;
    }

    private IEnumerator FadeToRed(GameObject fadeObj, float timeToFade)
    {
        float fadeProcessTimer = 0;
        float currentRed;

        while (fadeProcessTimer < 1)
        {
            fadeProcessTimer += Time.deltaTime / timeToFade;
            currentRed = Mathf.Lerp(1, 0.25f, fadeProcessTimer);
            ChangeObjectRed(fadeObj, currentRed);
            yield return null;
        }
    }

    private void ChangeObjectRed(GameObject fadeObj, float newRed)
    {
        if (fadeObj == null) return;
        //Color newColor = fadeObj.GetComponent<SpriteRenderer>().material.color;
        Color newColor = fadeObj.GetComponentInChildren<SpriteRenderer>().color;
        newColor = new Color(newColor.r, newRed, newRed, newColor.a);
        //fadeObj.GetComponent<SpriteRenderer>().material.color = newColor;
        fadeObj.GetComponentInChildren<SpriteRenderer>().color = newColor;
    }

    public void StopSpecifiedCoroutine(Coroutine toStop)
    {
        StopCoroutine(toStop);
    }

    public void StartTrailFadeOut(GameObject fadeObj, float timeToFade)
    {
        StartCoroutine(FadeTrail(fadeObj, fadeObj.GetComponent<TrailRenderer>().time, 0, timeToFade, null));
    }
    private IEnumerator FadeTrail(GameObject fadeObj, float startA, float endA, float timeToFade, UnityEvent postFade)
    {
        float fadeProcessTimer = 0;
        float currentAlpha;

        while (fadeProcessTimer < 1)
        {
            fadeProcessTimer += Time.deltaTime / timeToFade;
            currentAlpha = Mathf.Lerp(startA, endA, fadeProcessTimer);
            ChangeTrailLength(fadeObj, currentAlpha);
            //ChangeTrailAlpha(fadeObj, currentAlpha);
            yield return null;
        }
        fadeObj.SetActive(false);
        if (postFade != null)
            postFade.Invoke();
    }

    private void ChangeTrailLength(GameObject fadeObj, float newLength)
    {
        if (fadeObj == null) return;
        //Color newColor = fadeObj.GetComponent<SpriteRenderer>().material.color;
        //fadeObj.GetComponent<TrailRenderer>().time;
        fadeObj.GetComponent<TrailRenderer>().time = newLength;
        
        //fadeObj.GetComponent<TrailRenderer>().colorGradient.co
        //Debug.Log(fadeObj.GetComponent<TrailRenderer>().material.color);
    }

    private void ChangeTrailAlpha(GameObject fadeObj, float newAlpha)
    {
        if (fadeObj == null) return;
        //Color newColor = fadeObj.GetComponent<SpriteRenderer>().material.color;
        Color newColor = fadeObj.GetComponent<TrailRenderer>().material.color;
        newColor = new Color(newColor.r, newColor.g, newColor.b, newAlpha);
        //fadeObj.GetComponent<SpriteRenderer>().material.color = newColor;
        fadeObj.GetComponent<TrailRenderer>().material.color = newColor;
        //fadeObj.GetComponent<TrailRenderer>().colorGradient.co
        Debug.Log(fadeObj.GetComponent<TrailRenderer>().material.color);
    }
}

