using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    [SerializeField] private Animator Anim;
    [SerializeField] private bool isColl = false;
    [SerializeField] private float speed = 1f;

    private GameManager gameManager;

    private void Start()
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
        if((collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Tile")) && !isColl)
        {
            isColl = true;
            Anim.SetTrigger("isColl");
            gameManager.DestroyAnPlayer(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
