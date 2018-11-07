using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FeedbackPanel : MonoBehaviour {
    private Animator animator;

    [SerializeField]
    private Text text;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        FeedbackManager.Instance.OnFeedbackTriggered += PlayFeedback;
    }

    private void OnDestroy()
    {
        FeedbackManager.Instance.OnFeedbackTriggered -= PlayFeedback;
    }

    void PlayFeedback(FeedbackType feedback)
    {
        text.text = feedback.text;
        text.color = feedback.color;
        animator.Play("Feedback");
    }
}
