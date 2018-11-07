using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameUi : MonoBehaviour
{
    [SerializeField]
    private Text scoreText;

    [SerializeField]
    private Text bestScoreText;

    public void EndGame()
    {
        GameManager.Instance.CurrentGameState = GameManager.GameState.EndGame;
    }

    public void Menu()
    {
        GameManager.Instance.CurrentGameState = GameManager.GameState.Menu;
    }

    public void Awake()
    {
        GameManager.Instance.OnScoreUpdate += UpdateUi;
    }

    public void OnDestroy()
    {
        GameManager.Instance.OnScoreUpdate -= UpdateUi;
    }

    public void OnEnable()
    {
        GameManager.Instance.CurrentScore = 0;
        UpdateUi();
    }

    public void Start()
    {
        UpdateUi();
    }

    public void UpdateUi()
    {
        scoreText.text = GameManager.Instance.CurrentScore.ToString();
        bestScoreText.text = GameManager.Instance.BestScore.ToString();
    }
}
