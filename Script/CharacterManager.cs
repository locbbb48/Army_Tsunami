using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class CharacterManager : MainChar
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float speed;

    GameManager gameManager;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        rb = GetComponent<Rigidbody2D>();
        speed = Random.Range(-0.2f, 0.2f);
        Index = GetIndexInList();
    }
    private void Update()
    {
        if (gameManager.m_characs[0] == null)
        {
            gameManager.m_characs[0] = gameManager.m_characs[1];
        }
        Index = GetIndexInList();
        Move(Index);
        if(Index > 0)
        {
            fixedPosition();
        }
        transform.Translate(Vector2.right * (Mainspeed + speed) * Time.deltaTime);
    }

    private void Move(int Index)
    {
        if (Index == 0)
        {
            Mainspeed = 3.2f;
            firstCharMove();
        }
        else if (Index > 0)
        {
            followFirstChar();
        }
    }

    private void fixedPosition()
    {
        if (transform.position.x > gameManager.m_characs[0].transform.position.x - Index / 2 || speed > 0.21f)
        {
            isAcceleration = false;
            speed -= Acceleration * Time.deltaTime;
        }
        else
        {
            isAcceleration = true;
            speed += Acceleration * Time.deltaTime;
        }
    }

    public int GetIndexInList()
    {
        // Kiểm tra xem nhân vật này có trong danh sách không
        if (gameManager.m_characs.Contains(this))
        {
            // Trả về chỉ mục của nhân vật trong danh sách
            return gameManager.m_characs.IndexOf(this);
        }
        else
        {
            // Trả về -1 nếu nhân vật không có trong danh sách
            return -1;
        }
    }

    private void firstCharMove()
    {
        // Nhảy
        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space)) && isGrounded )
        {
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }
    }

    private void followFirstChar()
    {
        if((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space)) && isGrounded )
        {
            float DelayTime = unitTimetoJump * Index;
            StartCoroutine(JumpDelay(DelayTime));
        }
    }

    // Player chỉ được nhảy khi đã chạm đất
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Tile")
        {
            isGrounded = true; // Nhân vật đang chạm đất
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Tile")
        {
            isGrounded = false; // Nhân vật đang không chạm đất
        }
    }

    IEnumerator JumpDelay(float delay)
    {
        isGrounded = false;
        yield return new WaitForSeconds(delay);
        rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
    }
}