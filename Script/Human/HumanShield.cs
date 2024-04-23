using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanShield : Human
{
    [SerializeField] private Animator Anim;
    [SerializeField] private int AttackCountNeedtoDestroy = 7;
    private new void Start()
    {
        base.Start();
        Anim = GetComponent<Animator>();
    }
    new private void Update()
    {
        base.Update();
    }
    private new void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isDestroyed)
        {
            if(isFacingRight)
            {
                originalScale.x = -originalScale.x;
                transform.localScale = new Vector3(originalScale.x, originalScale.y, originalScale.z);
            }

            Anim.SetTrigger("isHasShield");
            speed = 0;

            if (gameManager.attackCount >= AttackCountNeedtoDestroy)
            {
                isDestroyed = true;
                Destroy(gameObject, 0.1f);
                AudioManager.instance.PlaySfxAudio1shot(AudioManager.instance.HumanDestroy);
                // Tạo một player mới
                StartCoroutine(CreateNewPlayerAfterDelay(0.09f));
            }
        }
        else if(collision.gameObject.CompareTag("bigPlayer"))
        {
            isDestroyed = true;
            Destroy(gameObject, 0.1f);
            AudioManager.instance.PlaySfxAudio1shot(AudioManager.instance.HumanDestroy);
            // Tạo một player mới
            StartCoroutine(CreateNewPlayerAfterDelay(0.09f));
        }
    }
}
