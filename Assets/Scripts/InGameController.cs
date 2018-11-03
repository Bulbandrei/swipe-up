using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameController : MonoBehaviour
{
    [SerializeField]
    private GameObject[] InGameObjects;

    public void Awake()
    {
        GameManager.Instance.OnStateChanged += OnStateChanged;
    }
   
    private void OnDestroy()
    {
        GameManager.Instance.OnStateChanged -= OnStateChanged;
    }

    private void Start()
    {
        OnStopGame();
    }

    private void OnStateChanged(GameManager.GameState oldState, GameManager.GameState newState)
    {
        if(oldState == newState)
        {
            return;
        }

        if(newState == GameManager.GameState.InGame)
        {
            OnStartGame();
        }
        else
        {
            OnStopGame();
        }
    }

    private void OnStartGame()
    {
        SetInGameObjectsActive(true);
    }

    private void OnStopGame()
    {
        SetInGameObjectsActive(false);
    }

    public void SetInGameObjectsActive(bool value)
    {
        if (InGameObjects == null)
        {
            return;
        }

        foreach(var inGameObject in InGameObjects)
        {
            inGameObject.SetActive(value);
        }
    }
}
