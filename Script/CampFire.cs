using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampFire : MonoBehaviour
{
    [SerializeField] private Animator Anim;
    [SerializeField] private GameObject FireBall;

    private void Start()
    {
        Anim = GetComponent<Animator>();
        FireBall.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Anim.SetTrigger("isBurn");
            FireBall.SetActive(true);
            AudioManager.instance.PlaySfxAudio1shot(AudioManager.instance.Campfire);
        }
    }
}
