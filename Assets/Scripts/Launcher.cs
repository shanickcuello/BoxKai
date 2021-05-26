using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class Launcher : MonoBehaviourPunCallbacks //Hacerlo heredar de singleton
{
    [SerializeField] GameObject notConnectedToServer;
    [SerializeField] Text errorDisplay, serverStatus;
    [SerializeField] Button joinRoomBtn, createRoomBtn;

    private void Start()
    {
        ConnectToPhoton();
        joinRoomBtn.interactable = false;
        createRoomBtn.interactable = false;
    }

    //Called from btn
    public void ConnectToPhoton()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby(); //join default lobby
    } 

    public override void OnDisconnected(DisconnectCause cause)
    {
        joinRoomBtn.interactable = false;
        createRoomBtn.interactable = false;
        Debug.LogError("Conexion fallida " + cause.ToString());
        notConnectedToServer.SetActive(true);
        errorDisplay.text = "Imposible to connect with server, trying again. Please check status button right of the screen." +
            cause.ToString();
        serverStatus.color = Color.red;
        serverStatus.text = "Not connected: " + cause.ToString();
        Invoke("ConnectToPhoton", 3); // Pasar esto a corrutina
    }

    public override void OnJoinedLobby()
    {
        joinRoomBtn.interactable = true;
        createRoomBtn.interactable = true;
        serverStatus.color = Color.green;
        serverStatus.text = "Connected";
        Debug.Log("Conectado wachoooo");
    }

}
