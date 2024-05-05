using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stele : MonoBehaviour
{
    [SerializeField] private GameObject BloodStele;
    [SerializeField] private GameObject Stone;

    private void Start()
    {
        Stone.SetActive(false);
        BloodStele.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("bigPlayer"))
        {
            Stone.SetActive(true);
        }

        if (collision.gameObject == Stone)
        {
            BloodStele.SetActive(true);
            Stone.GetComponent<DeathArea>().enabled = false;
            AudioManager.instance.PlaySfxAudio1shot(AudioManager.instance.Dart);
        }
    }
}
