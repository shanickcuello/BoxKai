using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviourPunCallbacks
{
    PhotonView pv;
    public int playerCount;
    [SerializeField] GameObject counterPanel;
    [SerializeField] Text counterDisplay;
    float countDownToStartGame;
    [SerializeField] GameObject limitScenario;

    [SerializeField] GameObject winScreen, looseScreen;

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
        else if (currentState == GameState.InGame)
        {
            CheckforWinOrLoose();
        }
    }

    private void CheckforWinOrLoose()
    {
        //Chequear quien gana y quien pierde
        //Si algun player colisiono, mostrarles los carteles de win y loose.
    }

    private void CountDownToStartGame()
    {
        countDownToStartGame -= Time.deltaTime;
        counterDisplay.text = countDownToStartGame.ToString("00:00");
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

    public void ActiveLooseScreen()
    {
        looseScreen.SetActive(true);
        Invoke("GoBackToMenu", 5);
    }

    public void ActiveWinScreen()
    {
        winScreen.SetActive(true);
        Invoke("GoBackToMenu", 5);
    }

    //Called From Btn
    public void GoBackToMenu()
    {
        SceneManager.LoadScene(0); 
    }

}

public enum GameState
{
    WaitingForPlayers,
    CountDown,
    InGame,
    GameFinished
}
