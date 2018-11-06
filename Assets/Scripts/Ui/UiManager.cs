using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    public GameObject StartMenuScreen;
    public GameObject InGameScreen;
    public GameObject EndGameScreen;

    private void Awake()
    {
        GameManager.Instance.OnStateUpdate += UpdateUi;
    }
    
    private void OnDestroy()
    {
        GameManager.Instance.OnStateUpdate -= UpdateUi;
    }

    private void Start()
    {
        UpdateUi();
    }

    public void UpdateUi()
    {
        StartMenuScreen.SetActive(false);
        InGameScreen.SetActive(false);
        EndGameScreen.SetActive(false);

        switch (GameManager.Instance.CurrentGameState)
        {
            case GameManager.GameState.Menu:
                StartMenuScreen.SetActive(true);
                break;
            case GameManager.GameState.InGame:
                InGameScreen.SetActive(true);
                break;
            case GameManager.GameState.EndGame:
                EndGameScreen.SetActive(true);
                break;
            default:
                break;
        }
    }
}
