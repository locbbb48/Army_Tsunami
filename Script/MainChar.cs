using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainChar : MonoBehaviour
{
    public float Mainspeed = 3f;
    public bool isGrounded = false; // Biến kiểm tra nhân vật có đang chạm đất
    public float jumpForce = 5.5f;
    public float maxJumpTime = 0.27f;
    public float jumpTime;
    public bool isJumping = false;
    public bool isJumpable = false;

    public float acceleration = 0.1f;
    public bool isAccelerating = true;

    public float LimitX;

    void Update()
    {
        transform.Translate(Vector2.right * Mainspeed * Time.deltaTime);
        LimitX = transform.position.x;
    }
}
