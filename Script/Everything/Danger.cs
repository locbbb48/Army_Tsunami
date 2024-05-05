using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Danger : MonoBehaviour
{
    private GameManager gameManager;
    [SerializeField] private float timeExist = 0.3f;
    [SerializeField] private float Distance = 34f;
    [SerializeField] private GameObject BoxCol;
    [SerializeField] private bool isWarning = true;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        BoxCol = GameObject.Find("BoxCol1");
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
    }

    private void Update()
    {
        if (BoxCol != null)
        {
            if ((gameObject.transform.position.x - BoxCol.transform.position.x < Distance) && isWarning)
            {
                isWarning = false;
                gameObject.GetComponent<SpriteRenderer>().enabled = true;
                AudioManager.instance.PlaySfxAudio1shot(AudioManager.instance.Warning);
                Destroy(gameObject, timeExist);
            }
        }
    }
}
