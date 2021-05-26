using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class CameraPositioning : MonoBehaviour
{
    [SerializeField] GameObject cameraTwoPosition;

    private void Start()
    {
        if (PhotonNetwork.PlayerList.Length > 0)
        {
            Debug.LogError("Deberia posicionarme en el otro lado");
            transform.position = cameraTwoPosition.transform.position;
            transform.rotation = cameraTwoPosition.transform.rotation;
        }
    }
}
