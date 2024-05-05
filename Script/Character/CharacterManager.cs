using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class CharacterManager : MainChar
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float speed = 0f;
    [SerializeField] private Animator Anim;

    private void Awake()
    {
        Anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        speed = 0f;
    }

    GameManager gameManager;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();

        if (gameManager.m_characs.Count > 0 && gameManager != null)
        {
            Index = GetIndexInList();
            thisTurnNeedJump = gameManager.thisTurnNeedJumpForAll;
        }
        else
        {
            Debug.Log("error in Character.cs: gameManager is nullReference in Start()");
        }
    }

    private void OnEnable()
    {
        isJumping = false;
    }

    private void Update()
    {
        if (gameManager.m_characs.Count > 0 && gameManager.m_characs[0] != null && this != null)
        {
            Index = GetIndexInList();
            Move(Index);
            if (Index < 0)
            {
                Destroy(this);
            } 
            transform.Translate(Vector2.right * (Mainspeed + speed) * Time.deltaTime);
        }
        else
        {
            Debug.Log("Error in Character.cs: gamManager.m_chars[0] is null in Update() or Char[" + Index + " ] is null" );
        }
    }

    public int GetIndexInList()
    {
        if (gameManager.m_characs != null)
        {
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
        else { 
            return -1;
        }
    }

    private void fixedPosition()
    {
        if (transform.position.x > gameManager.m_characs[0].transform.position.x - Index / 3 || speed > 0.5f) // điều chỉnh tốc độ sao cho các char sẽ cách nhau 0.5f và speed không vượt quá 0.5f.
        {
            isAcceleration = false;
            speed -= Acceleration * Time.deltaTime;
        }
        else if ((transform.position.x < gameManager.m_characs[0].transform.position.x - Index / 3 || speed < -0.2f))
        {
            isAcceleration = true;
            speed += Acceleration * Time.deltaTime;
        }
        else if(transform.position.x == gameManager.m_characs[0].transform.position.x - Index / 3)
        {
            speed = 0f;
        }
    }

    // Quản lí việc nhảy và tốc độ các Char
    private void Move(int Index)
    {
        if (Index == 0)
        {
            thisTurnNeedJump = gameManager.thisTurnNeedJumpForAll;
            Mainspeed = 3.5f;
            if(speed > 0.4)
            {
                speed = 0.2f;
            }
            firstCharMove();
        }
        else if (Index > 0)
        {
            Mainspeed = 3.0f;
            fixedPosition();
            followFirstChar();
        }
    }

    private void firstCharMove() // Index=0
    {
        // Nhảy
        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space)) && isGrounded )
        {
            gameManager.thisTurnNeedJumpForAll++;
            speed += 0.2f;
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }
    }

    private void followFirstChar() // Index>0
    {
        if(thisTurnNeedJump < gameManager.thisTurnNeedJumpForAll && isGrounded && !isJumping)
        {
            speed += 0.05f;
            float DelayTime = unitTimetoJump * Index;
            StartCoroutine(JumpDelay(DelayTime));
        }
    } 
    IEnumerator JumpDelay(float delay)
    {
        isGrounded = false;
        isJumping = true;
        thisTurnNeedJump++;
        yield return new WaitForSeconds(delay);
        if (this != null)
        {
            isJumping = false;
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }
    }


    // Player chỉ được nhảy khi đã chạm đất
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Tile")
        {
            isGrounded = true;
        }
        if (collision.gameObject.CompareTag("Food"))
        {
            Anim.SetTrigger("attack");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Tile")
        {
            isGrounded = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Human"))
        {
            Anim.SetTrigger("attack");
            gameManager.attackCount++;
        }
        if(collision.gameObject == gameManager.BoxCol2)
        {
            speed = 2f;
        }
    }
}