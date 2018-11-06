using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
   
    public delegate void UpdateDelegate();
    public UpdateDelegate OnScoreUpdate;
    public UpdateDelegate OnStateUpdate;
  
    public delegate void OnStateChangeDelegate(GameState oldState, GameState newState);
    public OnStateChangeDelegate OnStateChanged;
    
    public enum GameState
    {
        Menu,
        InGame,
        EndGame
    }

    private int currentRoundScore;
    private int currentScore;
    private int bestScore;
    private GameState currentGameState = GameState.Menu;

    public int BestScore
    {
        get
        {
            return bestScore;
        }
    }

    public int CurrentRoundScore
    {
        get
        {
            return currentRoundScore;
        }

        set
        {
            currentRoundScore = value;
            if (OnScoreUpdate != null)
            {
                OnScoreUpdate();
            }
        }
    }
    public int CurrentScore
    {
        get
        {
            return currentScore;
        }
        set
        {
            currentScore = value;
            if(bestScore >= currentScore)
            {
                bestScore = currentScore;
            }
            CurrentRoundScore = 0;
            if(OnScoreUpdate != null)
            {
                OnScoreUpdate();
            }
        }
    }

    public GameState CurrentGameState
    {
        get
        {
            return currentGameState;
        }
        set
        {
            var lastGameState = currentGameState;
            currentGameState = value;
            if (OnStateUpdate != null)
            {
                OnStateUpdate();
            }
            if (OnStateChanged != null)
            {
                OnStateChanged(lastGameState, currentGameState);
            }

        }
    }

    public void Awake()
    {
        Instance = this;
    }

}
