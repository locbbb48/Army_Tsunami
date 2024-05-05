using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanShield : Human
{
    [SerializeField] protected Animator Anim;
    [SerializeField] protected int AttackCountNeedtoDestroy = 7;
    protected new void Start()
    {
        base.Start();
        Anim = GetComponent<Animator>();
    }
    new protected void Update()
    {
        base.Update();
    }
    protected new void OnCollisionEnter2D(Collision2D collision)
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
                gameManager.CreateNewPlayer(playerPrefab);
                AudioManager.instance.PlaySfxAudio1shot(AudioManager.instance.HumanDestroy);
                Destroy(gameObject, 0.1f);
            }
        }
        else if(collision.gameObject.CompareTag("bigPlayer"))
        {
            isDestroyed = true;
            gameManager.CreateNewPlayer(playerPrefab);
            AudioManager.instance.PlaySfxAudio1shot(AudioManager.instance.HumanDestroy);
            Destroy(gameObject, 0.1f);
        }
    }
}
