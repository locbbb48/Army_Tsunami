using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    [SerializeField] private bool isColl = false;
    public Vector3 direction = new Vector3(-2f, -1f, 0);

    private GameManager gameManager;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    private void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Tile")) && !isColl)
        {
            isColl = true;
            gameManager.DestroyAnPlayer(collision.gameObject);
            Destroy(gameObject);
        }
        else if(collision.gameObject.CompareTag("bigPlayer") && !isColl)
        {
            isColl = true;
            Destroy(gameObject);
        }
    }
}
