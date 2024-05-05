using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainChar : MonoBehaviour
{
    [SerializeField] protected float Mainspeed = 3f;
    [SerializeField] protected bool isGrounded = false;
    [SerializeField] protected bool isJumping = false;

    [SerializeField] protected float jumpForce = 8f;
    [SerializeField] protected float unitTimetoJump = 0.1f; //

    [SerializeField] protected int Index;

    [SerializeField] protected bool isAcceleration = true;
    [SerializeField] protected float Acceleration = 0.1f;

    public int thisTurnNeedJump = 0;

    void Update()
    {
        transform.Translate(Vector2.right * Mainspeed * Time.deltaTime);
    }
}
