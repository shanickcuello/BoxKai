using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System;

public class Player : MonoBehaviour
{
    PhotonView photonView;

    [SerializeField] float speedMovement;
    [SerializeField] GameObject bulletSpawnPosition;
    Rigidbody rb;
    float shootCoolDown;
    public bool imSecondPlayer;

    void Start()
    {
        if (PhotonNetwork.PlayerList.Length > 1)
        {
            imSecondPlayer = true;
        }

        rb = GetComponent<Rigidbody>();
        photonView = GetComponent<PhotonView>();

        if (speedMovement == 0)
        {
            speedMovement = 5;
        }

        if (!photonView.IsMine)
        {
            return;
        }

        //ChangeColorMyColor();
        photonView.RPC("RPCChangeColor", RpcTarget.OthersBuffered, 1, 0, 0);
    }

    private void ChangeColorMyColor()
    {
        GetComponent<Material>().color = Color.blue;
    }



    void Update()
    {
        if (!photonView.IsMine) return;
        Movement();
        Attack();
        Reloading();
        Aim();
    }

    private void Aim()
    {
        //Get the Screen positions of the object
        Vector2 positionOnScreen = Camera.main.WorldToViewportPoint(transform.position);

        //Get the Screen position of the mouse
        Vector2 mouseOnScreen = (Vector2)Camera.main.ScreenToViewportPoint(Input.mousePosition);

        //Get the angle between the points
        float angle = AngleBetweenTwoPoints(positionOnScreen, mouseOnScreen);

        if (!imSecondPlayer)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0f, (-angle) + 90, 0f));
        }
        else
        {
            transform.rotation = Quaternion.Euler(new Vector3(0f, (-angle) -90, 0f));
        }

    }

    float AngleBetweenTwoPoints(Vector3 a, Vector3 b)
    {
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }

    private void Attack()
    {

        if (Input.GetMouseButtonUp(0) && shootCoolDown <= 0)
        {
            PhotonNetwork.Instantiate("Bullet", bulletSpawnPosition.transform.position, transform.rotation);
            shootCoolDown += 0.3f;
        }

        
    }

    void Reloading()
    {
        if (shootCoolDown <= 0)
        {
            shootCoolDown = 0;
        }
        else
        {
            shootCoolDown -= Time.deltaTime;
        }
    }

    void Movement()
    {
        transform.position += transform.right * Input.GetAxis("Horizontal") * speedMovement * Time.deltaTime;
        transform.position += transform.forward * Input.GetAxis("Vertical") * speedMovement * Time.deltaTime;
    }

    [PunRPC]
    public void RPCChangeColor(float r, float g, float b)
    {
        GetComponent<Material>().color = new Color(r, g, b);
    }
}
