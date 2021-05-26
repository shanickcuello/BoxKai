using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using System;

public class GameManager : MonoBehaviourPunCallbacks
{
    PhotonView pv;
    public int playerCount;
    [SerializeField] GameObject counterPanel;
    [SerializeField] Text counterDisplay;
    float countDownToStartGame;
    bool startCounter;
    [SerializeField] GameObject limitScenario;

    GameState currentState;

    private void Start()
    {
        currentState = GameState.WaitingForPlayers;
        countDownToStartGame = 5;
    }

    private void Update()
    {
        if (currentState == GameState.WaitingForPlayers)
        {
            CheckPlayerCount();
        }
        else if (currentState == GameState.CountDown)
        {
            CountDownToStartGame();
        }
    }

    private void CountDownToStartGame()
    {
        countDownToStartGame -= Time.deltaTime;
        counterDisplay.text = countDownToStartGame.ToString();
        if (countDownToStartGame <= 0)
        {
            counterPanel.SetActive(false);
            limitScenario.GetComponent<Animator>().SetTrigger("down");
            currentState = GameState.InGame;
        }
    }

    void CheckPlayerCount()
    {
        playerCount = PhotonNetwork.PlayerList.Length;
        if (playerCount > 1)
        {
            currentState = GameState.CountDown;
            counterPanel.SetActive(true);
        }
    }

}

public enum GameState
{
    WaitingForPlayers,
    CountDown,
    InGame,
    GameFinished
}
