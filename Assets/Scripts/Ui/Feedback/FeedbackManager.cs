using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedbackManager : MonoBehaviour
{
    public static FeedbackManager Instance { get; private set; }

    [SerializeField]
    private List<FeedbackType> feedbacks = new List<FeedbackType>();

    public delegate void OnFeedbackTriggeredDelegate(FeedbackType feedback);
    public OnFeedbackTriggeredDelegate OnFeedbackTriggered;
    
    public void Awake()
    {
        Instance = this;
        GameManager.Instance.OnScoreUpdate += CheckFeedbacksTriggered;
    }

    public void Destroy()
    {
        GameManager.Instance.OnScoreUpdate -= CheckFeedbacksTriggered;
    }


    public void CheckFeedbacksTriggered()
    {
        foreach(var feedback in feedbacks)
        {
            if(CheckFeedbackTriggered(feedback))
            {
                NotifyFeedbackTriggered(feedback);
                break;
            }
        }
    }

    public void NotifyFeedbackTriggered(FeedbackType feedback)
    {
        if(OnFeedbackTriggered != null)
        {
            OnFeedbackTriggered(feedback);
            
        }
    }

    public bool CheckFeedbackTriggered(FeedbackType feedback)
    {
        return GameManager.Instance.CurrentRoundScore >= feedback.comboCount;
    }
}
