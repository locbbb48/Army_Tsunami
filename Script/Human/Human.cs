using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Human : humanAbstract
{
    [SerializeField] protected bool isFacingRight;
    [SerializeField] protected float x1, x2;
    [SerializeField] protected float radius = 5f;

    protected Vector3 originalScale;

    protected new void Start()
    {
        base.Start();
        originalScale = transform.localScale;
        x1 = transform.position.x - radius;
        x2 = transform.position.x + radius;
    }
    protected void Update()
    {
        if (isFacingRight)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        }

        if (transform.position.x > x2 && isFacingRight) 
        {
            isFacingRight = false;
            originalScale.x = -originalScale.x;
            transform.localScale = new Vector3(originalScale.x, originalScale.y, originalScale.z); // quay mặt sang trái
        }
        else if (transform.position.x < x1 && !isFacingRight)
        {
            isFacingRight = true;
            originalScale.x = -originalScale.x;
            transform.localScale = new Vector3(originalScale.x, originalScale.y, originalScale.z); // quay mặt sang phải
        }
    }
}