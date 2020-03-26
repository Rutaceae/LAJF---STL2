﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using STL2.Events;

public class GreatChest : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D rb;

    private Vector3 inactivePosition; //Position from which the box drops

    private bool finishedAnimation;
    private bool playerOpenedBox = false;

    [SerializeField]
    private VoidEvent whenPlayerOpenedBox;

    void Start()
    {
        inactivePosition = transform.position;
    }

    void Update()
    {
        if (playerOpenedBox)
        {
            //animate and whathaveyounot, then raise event.
            finishedAnimation = true; //Obviously needs to animate first.
        }

        if (finishedAnimation)
        {
            whenPlayerOpenedBox.Raise();
        }
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerOpenedBox = true;
        }
    }

    public void OnPlayerFinishChallenge()
    {
        rb.gravityScale = 1;
    }

    public void ReInitialize(){
        rb.gravityScale = 0;
        transform.position = inactivePosition;
    }
}
