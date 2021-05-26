using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnPlayer : MonoBehaviour
{
    public GameObject playerPosition0, playerPosition1, playerPosition2, playerPosition3;
    private void Start()
    {
        if (PhotonNetwork.PlayerList.Length == 0)
        {
            PhotonNetwork.Instantiate("CatPlayer", playerPosition0.transform.position, playerPosition0.transform.rotation);
        }
        else if (PhotonNetwork.PlayerList.Length == 1)
        {
            PhotonNetwork.Instantiate("CatPlayer", playerPosition1.transform.position, playerPosition1.transform.rotation);
        }
        else if (PhotonNetwork.PlayerList.Length == 2)
        {
            PhotonNetwork.Instantiate("CatPlayer", playerPosition2.transform.position, playerPosition2.transform.rotation);
        }
        else if (PhotonNetwork.PlayerList.Length >= 3)
        {
            PhotonNetwork.Instantiate("CatPlayer", playerPosition3.transform.position, playerPosition3.transform.rotation);
        }
    }
}
