using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPanelUi : MonoBehaviour
{
    public void StartGame()
    {
        GameManager.Instance.CurrentGameState = GameManager.GameState.InGame;
    }
}
