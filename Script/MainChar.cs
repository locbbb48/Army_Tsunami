using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainChar : MonoBehaviour
{
    [SerializeField] protected float Mainspeed = 3f;
    [SerializeField] protected bool isGrounded = false; // Biến kiểm tra nhân vật có đang chạm đất
    [SerializeField] protected float jumpForce = 8f;
    [SerializeField] protected float unitTimetoJump = 0.2f;

    [SerializeField] protected int Index;

    [SerializeField] protected bool isAcceleration = true;
    [SerializeField] protected float Acceleration = 0.2f;

    void Update()
    {
        transform.Translate(Vector2.right * Mainspeed * Time.deltaTime);
    }
}
