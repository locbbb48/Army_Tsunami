using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadStatue : MonoBehaviour
{
    [SerializeField] private GameObject FireBall;
    [SerializeField] private float UnitTimeFire = 4f;
    [SerializeField] private int FireBallCount = 10;
    [SerializeField] private bool isFire = true;

    private void Update()
    {
        if(FireBall != null && isFire)
        {
            for(int i = 0; i < FireBallCount && isFire; i++)
            {
                StartCoroutine(Delay(UnitTimeFire * i));
            }

            isFire = false;
        }
    }

    IEnumerator Delay(float delay)
    {
        yield return new WaitForSeconds(delay);
        GameObject newFireBall = Instantiate(FireBall, transform.position, Quaternion.identity);
    }
}
