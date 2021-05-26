using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class LobbyUI : MonoBehaviourPunCallbacks
{
    //Private variables
    [SerializeField] InputField createRoomInput, joinRoomInput;
    [SerializeField] Dropdown playersCount;
    [SerializeField] Text roomIdDisplay, joinRoomError;
    RoomOptions roomOptions;

    //public Variables
    public string roomId;


    #region CREATE ROOM
    //Called from Btn
    public void CreateRoomBTN()
    {
        if (createRoomInput.text != "")
        {
            roomOptions = new RoomOptions();
            //roomOptions.MaxPlayers = (byte)playersCount.value;
            roomId = createRoomInput.text + GetRandomNumbers().ToString();
            roomIdDisplay.text = "Sala creada. ID: " + roomId;
            Debug.LogWarning("Room ID: " + roomId);
            Invoke("CreateRoom", 5);
        }
    }

    void CreateRoom()
    {
        PhotonNetwork.CreateRoom(roomId, roomOptions);
    }

    public override void OnCreatedRoom()
    {

    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.LogWarning(returnCode + message);
    }

    int GetRandomNumbers()
    {
        string randomNumber = "";
        for (int i = 0; i < 4; i++)
        {
            randomNumber += Random.Range(0, 10);
        }
        return int.Parse(randomNumber);
    }
    #endregion

    #region JOIN ROOM

    //Called from Button
    public void JoinRoomBTN()
    {
        PhotonNetwork.JoinRoom(joinRoomInput.text);
    }

    //Callled on room created too
    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("Game");
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        joinRoomError.text = returnCode + message;
        Debug.LogError("no pudee joinear");
    }

    #endregion


}
