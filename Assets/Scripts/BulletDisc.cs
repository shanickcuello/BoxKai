using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System;

public class BulletDisc : MonoBehaviour
{
    [SerializeField] float speedMovement;
    float timeLife;
    PhotonView photonView;

    private void Start()
    {
        timeLife = 8;
        photonView = GetComponent<PhotonView>();
    }

    private void Update()
    {
        Move();
        Die();
    }

    private void Die()
    {
        if (timeLife <= 0)
            Destroy(gameObject);
        else
            timeLife -= Time.deltaTime;
    }

    void Move()
    {
        transform.position += transform.forward * speedMovement * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject, 0.1f);
    }
}
