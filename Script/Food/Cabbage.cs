using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Cabbage : FoodAbstract
{
    [SerializeField] private GameObject bigPlayerPrefab;

    new private void Start()
    {
        base.Start();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isColl)
        {
            isColl = true;
            foreach (Transform child in AllCharacters.transform)
            {
                child.gameObject.SetActive(false);
            }
            AudioManager.instance.PlaySfxAudio1shot(AudioManager.instance.Transfiguation);
            // Tạo một player mới
            gameManager.CreateBigPlayer(bigPlayerPrefab);
            Destroy(gameObject, 0.1f);
        }
        else if(collision.gameObject.CompareTag("bigPlayer") && !isColl) 
        {
            isColl = true;
            foreach (Transform child in AllCharacters.transform)
            {
                child.gameObject.SetActive(false);
            }
            AudioManager.instance.PlaySfxAudio1shot(AudioManager.instance.Transfiguation);
            gameManager.CreateBigPlayer(bigPlayerPrefab, transform.position);
            Destroy(collision.gameObject);
            Destroy(gameObject, 0.1f);
        } 
    }
 
}
