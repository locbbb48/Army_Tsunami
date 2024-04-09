using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Human : MonoBehaviour
{
    public GameObject playerPrefab;
    private GameManager gameManager;

    [SerializeField] float speed = 1f;
    private bool isFacingRight = false;
    [SerializeField] float x1, x2;
    private bool isDestroyed = false;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        x1 = transform.position.x - 5;
        x2 = transform.position.x + 5;

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
            isFacingRight = false;
            transform.localScale = new Vector3(1, 1, 1); // quay mặt sang trái
        }
        else if (transform.position.x <= x1)
        {
            isFacingRight = true;
            transform.localScale = new Vector3(-1, 1, 1); // quay mặt sang phải

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
            gameManager.CreateNewPlayer(playerPrefab, this.transform);
        }
    }
}