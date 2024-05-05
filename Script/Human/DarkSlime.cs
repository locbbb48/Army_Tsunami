using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DarkSlime : MonoBehaviour
{
    [SerializeField] protected Animator Anim;
    protected GameManager gameManager;
    [SerializeField] protected bool isColl = false;
    [SerializeField] private GameObject DeadArea;
    [SerializeField] protected float DelayTime = 1f;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        Anim = GetComponent<Animator>();
        DeadArea.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isColl)
        {
            StartCoroutine(AttackAfterDelay(1f));
            gameManager.DestroyAnPlayer(collision.gameObject);
        }
        else if ((collision.gameObject.CompareTag("bigPlayer") && !isColl))
        {
            StartCoroutine (AttackAfterDelay(1f));
        }
    }

    private IEnumerator AttackAfterDelay(float delay)
    {
        isColl = true;
        Anim.SetTrigger("isAttack");
        AudioManager.instance.PlaySfxAudio1shot(AudioManager.instance.Swish);
        yield return new WaitForSeconds(delay);
        DeadArea.SetActive(true);
    }
}
