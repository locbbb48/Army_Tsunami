using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Note: Vị trí của SpawnMap và CreateNewMapPoint phải trùng nhau và giống nhau giữa tất cả các Map.
// Các map có cùng length là 320
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
