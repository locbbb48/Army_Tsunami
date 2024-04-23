using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eagle : MonoBehaviour
{
    [SerializeField] private float speed = 1f;
    [SerializeField] private float BoostSpeed = 10f;
    [SerializeField] private Animator Anim;
    [SerializeField] private float distanceX = 50f; // khoang cach bay duoc truoc khi bi huy
    [SerializeField] private float X;

    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private bool isCreate = false;
    private GameManager gameManager;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        Anim = GetComponent<Animator>();
        X = this.gameObject.transform.position.x + distanceX;
    }

    private void Update()
    {
            Move();
    }

    private void Move()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
        StartCoroutine(DelayAfterBoost(2f));
        if(this.gameObject.transform.position.x > X)
        {
            Destroy(this.gameObject);
        }
    }

    IEnumerator DelayAfterBoost(float Delay)
    {
        yield return new WaitForSeconds(Delay);
        if (!isCreate)
        {
            isCreate = true;
            gameManager.CreateNewPlayer(playerPrefab, this.gameObject.transform.position.y);
        }
        Anim.SetBool("Boost", true);
        speed = BoostSpeed;

    }
}
