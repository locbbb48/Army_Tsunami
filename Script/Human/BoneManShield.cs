using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class BoneManShield : HumanShield
{
    [SerializeField] private GameObject DeadArea;
    [SerializeField] private float TimeDelay = 0.3f;

    private new void Start()
    {
        base.Start();
        AttackCountNeedtoDestroy = 3;
        DeadArea.SetActive(false);
    }

    private new void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D (collision);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            if (isFacingRight)
            {
                originalScale.x = -originalScale.x;
                transform.localScale = new Vector3(originalScale.x, originalScale.y, originalScale.z);
            }
            Anim.SetTrigger("isAttack");
            StartCoroutine(AttackAfterDelay(TimeDelay));
        }
        else if (collision.gameObject.CompareTag("bigPlayer"))
        {
            if (isFacingRight)
            {
                originalScale.x = -originalScale.x;
                transform.localScale = new Vector3(originalScale.x, originalScale.y, originalScale.z);
            }
            Anim.SetTrigger("isAttack");
        }
    }

    IEnumerator AttackAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        DeadArea.SetActive(true);
    }
}
