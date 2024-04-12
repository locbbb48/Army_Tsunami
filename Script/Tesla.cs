using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tesla : MonoBehaviour
{
    private GameManager gameManager;

    [SerializeField] private Animator animator;
    [SerializeField] private bool iscoll = false;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player") && iscoll == false)
        {
            iscoll = true;
            animator.SetTrigger("SetTrigger");
            AudioManager.instance.PlaySfxAudio1shot(AudioManager.instance.Tesla);
            gameManager.DestroyAnPlayer(collision.gameObject);
            Destroy(gameObject, 1f);
        }
    }
}
