using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class humanAbstract : MonoBehaviour
{

    [SerializeField] protected GameObject playerPrefab;
    protected GameManager gameManager;

    [SerializeField] protected float speed = 1f;
    [SerializeField] protected bool isDestroyed = false;

    protected virtual void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }
    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if ((collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("bigPlayer")) && !isDestroyed)
        {
            isDestroyed = true;
            Destroy(gameObject, 0.1f);
            AudioManager.instance.PlaySfxAudio1shot(AudioManager.instance.HumanDestroy);
            // Tạo một player mới
            StartCoroutine(CreateNewPlayerAfterDelay(0.09f));
        }
    }
    protected IEnumerator CreateNewPlayerAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        gameManager.CreateNewPlayer(playerPrefab);
    }

}
