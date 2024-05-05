using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transport : MonoBehaviour
{
    [SerializeField] private float Speed = 3f;
    [SerializeField] private GameObject Smoke;
    [SerializeField] private bool isRun = false;

    private void Update()
    {
        if(isRun && this != null)
        {
            transform.Translate(Vector2.right * Speed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("bigPlayer"))
        {
            StartCoroutine(Delay(0.5f));
            AudioManager.instance.PlaySfxAudio1shot(AudioManager.instance.Transport);
            Smoke.SetActive(true);
        }
        else if(collision.gameObject.CompareTag("Tile"))
        {
            Speed = 0;
            isRun = false;
            Destroy(gameObject, 2f);
        }
    }

    IEnumerator Delay(float delay)
    {
        yield return new WaitForSeconds(delay);
        isRun = true;
    }
}
