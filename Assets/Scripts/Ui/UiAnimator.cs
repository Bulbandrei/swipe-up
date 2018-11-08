using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiAnimator : MonoBehaviour {

    const float DEFAULT_ANIMATION_TIME = 2.0f;

    RectTransform rectTransform;

    IEnumerator scaleRoutine; // I'm keeping this separated to stop it later

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        scaleRoutine = ScaleAnimation();
    }
    private void OnEnable()
    {
        AudioController.Instance.OnMusicSwitch += OnMusicSwitch;

        StartCoroutine(PositionAnimation());
        StartCoroutine(RotationAnimation());
        if (AudioController.Instance.musicOn)
            StartCoroutine(scaleRoutine);
    }

    private void OnDisable()
    {
        AudioController.Instance.OnMusicSwitch -= OnMusicSwitch;
    }

    void OnMusicSwitch()
    {
        if (AudioController.Instance.musicOn)
            StartCoroutine(scaleRoutine);
        else
        {
            StopCoroutine(scaleRoutine);
            transform.localScale = Vector3.one;
        }
    }

    IEnumerator PositionAnimation()
    {
        // Initialize
        Vector2 endPos;
        float yAmount = 10.0f;
        // Initialize Position
        endPos = rectTransform.anchoredPosition;
        endPos.y -= yAmount / 2;
        yield return MoveTo(rectTransform.anchoredPosition, endPos);
        while (true)
        {
            // Move Up
            endPos = rectTransform.anchoredPosition;
            endPos.y += yAmount;
            yield return MoveTo(rectTransform.anchoredPosition, endPos);
            // Move Down
            endPos = rectTransform.anchoredPosition;
            endPos.y -= yAmount;
            yield return MoveTo(rectTransform.anchoredPosition, endPos);
        }
    }

    IEnumerator MoveTo(Vector2 startPos, Vector2 endPos, float time = DEFAULT_ANIMATION_TIME)
    {
        float currentTime = time;
        Vector2 curPos = startPos;
        while (currentTime >= 0)
        {
            curPos = Vector3.Lerp(startPos, endPos, (time - currentTime) / time);
            rectTransform.anchoredPosition = curPos;
            currentTime -= Time.deltaTime;
            yield return null;
        }
        rectTransform.anchoredPosition = endPos;
    }

    IEnumerator RotationAnimation()
    {
        // Initialize
        Vector3 endRot;
        float rotationAmount = 2.0f;
        // Initialize Rotation
        endRot = transform.eulerAngles;
        endRot.z -= rotationAmount / 2;
        yield return RotateTo(transform.eulerAngles, endRot, DEFAULT_ANIMATION_TIME / 2);

        while (true)
        {
            // Rotate Left
            endRot = transform.eulerAngles;
            endRot.z += rotationAmount;
            yield return RotateTo(transform.eulerAngles, endRot);
            // Rotate Right
            endRot = transform.eulerAngles;
            endRot.z -= rotationAmount;
            yield return RotateTo(transform.eulerAngles, endRot);
        }
    }

    IEnumerator RotateTo(Vector3 startRotation, Vector3 endRotation, float time = DEFAULT_ANIMATION_TIME)
    {
        float currentTime = time;
        Vector3 curRot = startRotation;
        while (currentTime >= 0)
        {
            curRot = Vector3.Lerp(startRotation, endRotation, (time - currentTime) / time);
            transform.eulerAngles = curRot;
            currentTime -= Time.deltaTime;
            yield return null;
        }
        transform.eulerAngles = endRotation;
    }

    IEnumerator ScaleAnimation()
    {
        // Initialize
        Vector3 endScale;
        float scaleAmount = 0.05f;
        float beatTime = 0.5f; // Here I'm trying to make the animation match the speed of the music beat
        // Initialize Scale
        endScale = Vector3.one;
        endScale.x -= scaleAmount / 2; 
        endScale.y -= scaleAmount / 2;
        yield return ScaleTo(transform.localScale, endScale, beatTime);

        while (true)
        {
            // Scale Up
            endScale = Vector3.one;
            endScale.x += scaleAmount;
            endScale.y += scaleAmount;
            yield return ScaleTo(transform.localScale, endScale, beatTime / 4);
            // Scale Down
            endScale = transform.localScale;
            endScale.x -= scaleAmount;
            endScale.y -= scaleAmount;
            yield return ScaleTo(transform.localScale, endScale, beatTime);
        }
    }

    IEnumerator ScaleTo(Vector3 startScale, Vector3 endScale, float time = DEFAULT_ANIMATION_TIME)
    {
        float currentTime = time;
        Vector3 curScale = startScale;
        while (currentTime >= 0)
        {
            curScale = Vector3.Lerp(startScale, endScale, (time - currentTime) / time);
            transform.localScale = curScale;
            currentTime -= Time.deltaTime;
            yield return null;
        }
        transform.localScale = endScale;
    }
}
