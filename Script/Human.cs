using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Human : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    private GameManager gameManager;

    [SerializeField] private float speed = 1f;
    [SerializeField] private bool isFacingRight;
    [SerializeField] private float x1, x2;
    [SerializeField] private float radius = 5f;
    [SerializeField] private bool isDestroyed = false;

    private Vector3 originalScale;

    void Start()
    {
        originalScale = transform.localScale;
        gameManager = FindObjectOfType<GameManager>();
        x1 = transform.position.x - radius;
        x2 = transform.position.x + radius;

    }
    private void Update()
    {
        if (isFacingRight)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        }

        if (transform.position.x >= x2) 
        {
            isFacingRight = !isFacingRight;
            originalScale.x = -originalScale.x;
            transform.localScale = new Vector3(originalScale.x, originalScale.y, originalScale.z); // quay mặt sang trái
        }
        else if (transform.position.x <= x1)
        {
            isFacingRight = !isFacingRight; ;
            originalScale.x = -originalScale.x;

            transform.localScale = new Vector3(originalScale.x, originalScale.y, originalScale.z); // quay mặt sang phải

        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isDestroyed)
        {
            isDestroyed = true;
            Destroy(gameObject);
            AudioManager.instance.PlaySfxAudio1shot(AudioManager.instance.HumanDestroy);

            // Tạo một player mới
            Vector3 newPos = transform.position;
            newPos.x = gameManager.m_characs[0].transform.position.x - gameManager.charCount / 2;
            gameManager.CreateNewPlayer(playerPrefab, newPos);
        }
    }
}