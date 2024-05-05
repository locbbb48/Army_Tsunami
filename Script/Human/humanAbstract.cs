using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class humanAbstract : MonoBehaviour
{

    protected GameObject playerPrefab;
    protected GameManager gameManager;

    [SerializeField] protected float speed = 1f;
    [SerializeField] protected bool isDestroyed = false;

    protected virtual void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        playerPrefab = gameManager.charTypes[Random.Range(0, gameManager.charTypes.Count)];
    }
    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if ((collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("bigPlayer")) && !isDestroyed)
        {
            isDestroyed = true;
            AudioManager.instance.PlaySfxAudio1shot(AudioManager.instance.HumanDestroy);
            // Tạo một player mới
            gameManager.CreateNewPlayer(playerPrefab);
            Destroy(gameObject, 0.1f);
        }
    }
}
