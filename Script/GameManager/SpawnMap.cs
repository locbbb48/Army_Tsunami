using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMap : MonoBehaviour
{
    public GameObject m_CreateNewMapPoint;
    public Vector2 CreateNewMapPoint;
    public GameObject thisMap;
    public float lengthMap = 320;

    public void Start()
    {
        CreateNewMapPoint = m_CreateNewMapPoint.transform.position;
    }
    public bool isCreateMap(Vector2 pos)
    {
        return pos.x > CreateNewMapPoint.x;
    }
}
