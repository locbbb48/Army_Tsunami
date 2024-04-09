using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    public Animator Anim;
    public bool iscoll = false;
    public float speed = 1f;

    GameManager gameManager;

    private void Awake()
    {
        gameManager= FindObjectOfType<GameManager>();
        AudioManager.instance.PlaySfxAudio1shot(AudioManager.instance.FireBall);
    }
    private void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Tile"))
        {
            iscoll = true;
            Anim.SetTrigger("isColl");
            gameManager.DestroyAnPlayer(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
