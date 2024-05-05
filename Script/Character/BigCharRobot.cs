using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BigCharRobot : BigChar
{
    [SerializeField] private GameObject Tesla;
    [SerializeField] private GameObject Smoke;
    [SerializeField] private float maxJumpForce;

    protected override void Awake()
    {
        base.Awake();
    }


    protected override void Start()
    {
        base.Start();
        Smoke.SetActive(false);
        Tesla.SetActive(false);
    }

    new private void Update()
    {
        base.Update();
        Move();
    }
 
    protected override void Move()
    {

        base.Move();
        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space)))
        {
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            Smoke.SetActive(true);
            Tesla.SetActive(true);
        }
        else if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.Space)) && jumpForce < maxJumpForce)
        {
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            jumpForce += Time.deltaTime;
            Smoke.SetActive(true);
            Tesla.SetActive(true);
        }
        else if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.Space) || jumpForce > maxJumpForce)
        {
            jumpForce = 0;
            Smoke.SetActive(false);
            Tesla.SetActive(false);
        }
    }
}
