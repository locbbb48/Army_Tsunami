﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : humanAbstract
{
    [SerializeField] private GameObject ArrowPrefab;
    [SerializeField] private Animator Anim;
    [SerializeField] private Transform FirePoint;
    [SerializeField] private bool isFire = true;
    [SerializeField] private int ArrowCount = 2;
    [SerializeField] private float timeDelay = 1f;
    [SerializeField] private int controlArrowParameter  = 1; // thông số điều chỉnh hướng và tốc độ cho arrow( =0 : đi thẳng; =1 : đi chéo )


    private new void Start()
    {
        base.Start();
        Anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (gameManager.m_characs.Count > 0 && gameManager.m_characs[0] != null)
        {
            GameObject bigPlayer = GameObject.FindGameObjectWithTag("bigPlayer");
            if ((gameManager.m_characs[0].transform.position.x > FirePoint.position.x 
                    || (bigPlayer != null && bigPlayer.transform.position.x > FirePoint.position.x))
                        && isFire)
            {
                for (int i = 1; i <= ArrowCount && isFire; i++)
                {
                    StartCoroutine(Delay(timeDelay * i, i * controlArrowParameter));
                }
                isFire = false;

            }
        } 
    }

    private void Fire(int directionY)
    {
        Anim.SetTrigger("isFire");
        GameObject arrowObject = Instantiate(ArrowPrefab, transform.position, ArrowPrefab.transform.rotation);
        Arrow arrow = arrowObject.GetComponent<Arrow>();
        arrow.direction = new Vector3(-2, -directionY, 0);
    }
    IEnumerator Delay(float delay, int directionY)
    {
        yield return new WaitForSeconds(delay);
        Fire(directionY);
    }
}
