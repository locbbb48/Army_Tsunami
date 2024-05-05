using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigCharCabbage : BigChar
{
    [SerializeField] protected Animator Anim;
    [SerializeField] private bool isGrounded  = false;

    protected override void Awake()
    {
        base.Awake();
        Anim = GetComponent<Animator>();
    }

    protected override void Start()
    {
        base.Start();
    }

    protected new void Update()
    {
        base.Update();
        Move();
    }

    protected override void Move()
    {
        base.Move();
        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space)) && isGrounded)
        {
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            isGrounded = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
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
