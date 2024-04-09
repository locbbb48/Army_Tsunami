using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MainChar
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float speed;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        speed = Random.Range(-0.2f, 0.2f);
    }
    private void Update()
    {
        transform.Translate(Vector2.right * (Mainspeed + speed) * Time.deltaTime);
        Move();
    }

    public void Move()
    {
        // Nhảy
        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space)) && isGrounded )
        {
            if (isJumpable)
            {
                isJumping = true;
                jumpTime = maxJumpTime;
                rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            }
            else
            {
                StartCoroutine(JumpAfterDelay(0.3f));
            }
        }


        if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.Space)) && !isGrounded && isJumping)
        {
            if (jumpTime > 0)
            {
                rb.velocity = Vector2.up * jumpForce;
                jumpTime -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
        }
        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.Space))
        {
            isJumping = false;
        }

        // Tang Giảm Mainspeed
        if (isAccelerating)
        {
            speed += acceleration * Time.deltaTime;
            if(Mainspeed + speed > 3)
            {
                speed = Random.Range(0f, 0.2f);
                acceleration = 0;
            }
        }
        else
        {
            acceleration = 0.15f;
            speed -= acceleration * Time.deltaTime;
        }

    }

    // Player chỉ được nhảy khi đã chạm đất
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Tile")
        {
            isGrounded = true; // Nhân vật đang chạm đất
        }

        if (collision.gameObject.name == "LimitX1")
        {
            isAccelerating = true;
        }
        else if (collision.gameObject.name == "LimitX2")
        {
            isAccelerating = false;
        }

        if (collision.gameObject.name == "JumpablePos")
        {
            isJumpable = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Tile")
        {
            isGrounded = false; // Nhân vật đang không chạm đất
        }

        if (collision.gameObject.name == "JumpablePos")
        {
            isJumpable = false;
        }
    }

    IEnumerator JumpAfterDelay(float delay)
    {
        isGrounded = false;
        yield return new WaitForSeconds(delay);
        rb.AddForce(new Vector2(0, jumpForce * 1.5f), ForceMode2D.Impulse);
        
    }
}