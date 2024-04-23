using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigCharCabbage : MainChar
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator Anim;

    [SerializeField] private float existTime = 10f; // tgian tồn tại
    [SerializeField] protected Transform AllCharacters;

    private void Awake()
    {
        Anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    GameManager gameManager;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        AllCharacters = GameObject.Find("AllCharacter").transform;
        Mainspeed = 3.6f;
    }

    private void Update()
    {
        // move
        transform.Translate(Vector2.right * Mainspeed * Time.deltaTime);
        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space)) && isGrounded)
        {
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }
        
        // ẩn các Char
        foreach (Transform child in AllCharacters.transform)
        {
            child.gameObject.SetActive(false);
        }
        existTime -= Time.deltaTime;
        if( existTime <= 0 ) // hết tgian biến hình
        {
            Vector3 bigCharPosition = transform.position;
            foreach (Transform child in AllCharacters.transform)
            {
                child.position = bigCharPosition; // child xuất hiện tại vị trí của bigChar
                child.gameObject.SetActive(true);
            }
            Destroy(gameObject);
        }
    }

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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Human"))
        {
            Anim.SetTrigger("attack");
        }
    }

}
