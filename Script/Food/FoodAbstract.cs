using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FoodAbstract : MonoBehaviour
{
    protected GameManager gameManager;
    [SerializeField] protected Transform AllCharacters;
    [SerializeField] protected bool isColl = false;
    protected void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        AllCharacters = GameObject.Find("AllCharacter").transform;
    }
}
