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

    [SerializeField]
    AudioClip gameOverSoundClip;

    [SerializeField]
    AudioClip gameOverNewBestSoundClip;

    public void StartGame()
    {
        GameManager.Instance.CurrentGameState = GameManager.GameState.InGame;
    }

    public void Menu()
    {
        GameManager.Instance.CurrentGameState = GameManager.GameState.Menu;
    }

    public void OnEnable()
    {
        UpdateUi();
        PlayGameOverSound();
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

    void PlayGameOverSound()
    {
        // Sets the audio clip and play the game over sound
        GetComponent<AudioSource>().clip = GameManager.Instance.CurrentScore == GameManager.Instance.BestScore ? gameOverNewBestSoundClip : gameOverSoundClip;
        GetComponent<AudioSource>().Play();
    }
}
