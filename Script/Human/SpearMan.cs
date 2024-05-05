using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearMan : Human
{
    [SerializeField] private Animator Anim;
    [SerializeField] private bool isColl = false;
    [SerializeField] private float TimeDelay = 0.3f;
    private new void Start()
    {
        base.Start();
        Anim = GetComponent<Animator>();
    }

    private new void Update()
    {
        base.Update();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isColl)
        {
            if (isFacingRight)
            {
                originalScale.x = -originalScale.x;
                transform.localScale = new Vector3(originalScale.x, originalScale.y, originalScale.z);
            }
            Anim.SetTrigger("isAttack");
            StartCoroutine(AttackAfterDelay(TimeDelay, collision.gameObject));
        }
        else if(collision.gameObject.CompareTag("bigPlayer") && !isColl)
        {
            if (isFacingRight)
            {
                originalScale.x = -originalScale.x;
                transform.localScale = new Vector3(originalScale.x, originalScale.y, originalScale.z);
            }
            Anim.SetTrigger("isAttack");
        }
    }

    IEnumerator AttackAfterDelay(float delay, GameObject obj)
    {
        yield return new WaitForSeconds(delay);
        isColl = true;
        gameManager.DestroyAnPlayer(obj);
    }
}
