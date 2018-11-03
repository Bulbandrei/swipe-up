using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGameUi : MonoBehaviour
{
    [SerializeField]
    private Text scoreText;

    [SerializeField]
    private Text bestScoreText;

    public void StartGame()
    {
        GameManager.Instance.CurrentGameState = GameManager.GameState.InGame;
    }

    public void Menu()
    {
        GameManager.Instance.CurrentGameState = GameManager.GameState.Menu;
    }

    public void OnEnabled()
    {
        UpdateUi();
    }

    public void Start()
    {
        UpdateUi();
    }

    public void UpdateUi()
    {
        scoreText.text = GameManager.Instance.CurrentScore.ToString();
        bestScoreText.text = GameManager.Instance.CurrentScore.ToString();
    }
}
