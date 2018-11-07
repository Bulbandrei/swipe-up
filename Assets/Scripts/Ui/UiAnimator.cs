using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiAnimator : MonoBehaviour {

    RectTransform rectTransform;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }
    private void OnEnable()
    {
        AudioController.Instance.OnMusicSwitch += OnMusicSwitch;

        StartCoroutine(PositionAnimation());
        StartCoroutine(RotationAnimation());
        if (AudioController.Instance.musicOn)
            StartCoroutine(ScaleAnimation());
    }

    private void OnDisable()
    {
        AudioController.Instance.OnMusicSwitch -= OnMusicSwitch;
    }

    void OnMusicSwitch()
    {
        if (AudioController.Instance.musicOn)
            StartCoroutine(ScaleAnimation());
        else
            StopCoroutine(ScaleAnimation());
    }

    IEnumerator PositionAnimation()
    {
        // Initialize
        float totalTime = 1.0f;
        float currentTime;
        Vector2 startPos;
        Vector2 endPos;
        Vector2 curPos;
        while (true)
        {
            // Move Up
            currentTime = totalTime;
            startPos = rectTransform.anchoredPosition;
            curPos = startPos;
            endPos = startPos;
            endPos.y += 10.0f;
            yield return MoveTo(startPos, endPos, totalTime);
            // Move Down
            currentTime = totalTime;
            startPos = rectTransform.anchoredPosition;
            curPos = startPos;
            endPos = startPos;
            endPos.y -= 10.0f;
            yield return MoveTo(startPos, endPos, totalTime);
        }
    }

    IEnumerator MoveTo(Vector2 startPos, Vector2 endPos, float time)
    {
        float currentTime = time;
        Vector2 curPos = startPos;
        while (currentTime >= 0)
        {
            curPos = Vector2.Lerp(startPos, endPos, (time - currentTime) / time);
            rectTransform.anchoredPosition = curPos;
            currentTime -= Time.deltaTime;
            yield return null;
        }
    }

    IEnumerator RotationAnimation()
    {
        yield return null;
    }

    IEnumerator ScaleAnimation()
    {
        yield return null;
    }
}
