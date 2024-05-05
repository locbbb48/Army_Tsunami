using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class Slime : DarkSlime
{
    [SerializeField] protected float JumpForce = 8f ;
    [SerializeField] protected Rigidbody2D rb;
    [SerializeField] protected bool isJump = true;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        Anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    protected void Update()
    {
        if(isJump && !isColl)
        {
            isJump = false;
            StartCoroutine(JumpAfterDelay(DelayTime));
        }
    }

    IEnumerator JumpAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (rb != null)
        {
            rb.AddForce(new Vector2(0, JumpForce), ForceMode2D.Impulse);
            Anim.SetTrigger("isJump");
            isJump = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isColl)
        {
            isColl = true;
            AudioManager.instance.PlaySfxAudio1shot(AudioManager.instance.Swish);
            gameManager.DestroyAnPlayer(collision.gameObject);
            Destroy(gameObject);
        }
        else if ((collision.gameObject.CompareTag("bigPlayer") && !isColl))
        {
            isColl = true;
            AudioManager.instance.PlaySfxAudio1shot(AudioManager.instance.Swish);
            Destroy(gameObject);
        }
    }
}
