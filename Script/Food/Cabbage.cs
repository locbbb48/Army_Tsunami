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
            Destroy(gameObject, 0.1f);
            AudioManager.instance.PlaySfxAudio1shot(AudioManager.instance.HumanDestroy);
            // Tạo một player mới
            StartCoroutine(CreateNewPlayerAfterDelay(0.09f));
        }
    }

    protected IEnumerator CreateNewPlayerAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        gameManager.CreateBigPlayer(bigPlayerPrefab);
    }
}
