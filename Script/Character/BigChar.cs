using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BigChar : MonoBehaviour
{
    [SerializeField] protected float Mainspeed = 3f;
    [SerializeField] protected float jumpForce = 8f;
    [SerializeField] protected Rigidbody2D rb;
    [SerializeField] protected float existTime = 10f; // tgian tồn tại
    [SerializeField] protected Transform AllCharacters;
    [SerializeField] protected GameObject TransfigObj;
    [SerializeField] protected bool isTrans = false;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    GameManager gameManager;

    protected virtual void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        AllCharacters = GameObject.Find("AllCharacter").transform;
        // Tạo hiệu ứng biến hình
        GameObject Transfig = Instantiate(TransfigObj, transform.position, Quaternion.identity);
        Destroy(Transfig, 0.4f);
    }

    protected void Update()
    {
        Move();

        // ẩn các Char
        if (!isTrans && this != null)
        {
            foreach (Transform child in AllCharacters.transform)
            {
                child.gameObject.SetActive(false);
            }
        }
        existTime -= Time.deltaTime;
        if (existTime <= 0 && !isTrans && this != null) // hết tgian biến hình
        {
            isTrans = true;
            Vector3 bigCharPosition = transform.position;
            int charCountActived = 0;
            foreach (Transform child in AllCharacters.transform)
            {
                child.position = bigCharPosition; // child xuất hiện tại vị trí của bigChar
                child.gameObject.SetActive(true);
                charCountActived++;
            }
            // Tạo hiệu ứng biến hình
            GameObject Transfig = Instantiate(TransfigObj, transform.position, Quaternion.identity);
            AudioManager.instance.PlaySfxAudio1shot(AudioManager.instance.Transfiguation);
            Destroy(Transfig, 0.4f);
            if (charCountActived == gameManager.m_characs.Count)
            {
                Destroy(gameObject, 0.5f);
            }
        }
    }

    protected virtual void Move()
    {
        transform.Translate(Vector2.right * Mainspeed * Time.deltaTime);
    }

}
