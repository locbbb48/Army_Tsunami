using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class GameManager : MonoBehaviour
{
    public GameObject GameOverPanel;
    public GameObject PlayerDead;

    public Transform allCharactersTransform;
    public List<CharacterManager> m_characs;
    public int charCount;

    public List <SpawnMap> m_spawnMaps;

    void Start()
    {
        m_characs = new List<CharacterManager>();
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in players)
        {
            m_characs.Add(player.GetComponent<CharacterManager>());
        }

        m_spawnMaps = new List<SpawnMap>();
        GameObject[] maps = GameObject.FindGameObjectsWithTag("SpawnMap");
        foreach (GameObject map in maps)
        {
            m_spawnMaps.Add(map.GetComponent<SpawnMap>());
        }
    }

    void Update()
    {
        charCount = m_characs.Count;
        if (charCount == 0)
        {
            GameOver();
        }
        SpawnNewMap();
    }

    private void SpawnNewMap()
    {
        if (m_spawnMaps.Count > 0 && m_characs.Count > 0)
        {
            Vector2 CreatNewMapPoint = m_spawnMaps[m_spawnMaps.Count - 1].CreateNewMapPoint;
            CreatNewMapPoint.x += m_spawnMaps[m_spawnMaps.Count - 1].lengthMap;
            if (m_spawnMaps[m_spawnMaps.Count - 1].isCreateMap(m_characs[0].transform.position))
            {
                GameObject newMap = Instantiate(m_spawnMaps[m_spawnMaps.Count - 1].thisMap, CreatNewMapPoint, Quaternion.identity);
                if (newMap != null)
                {
                    m_spawnMaps.Add(newMap.GetComponent<SpawnMap>());
                }
            }

            if (m_spawnMaps.Count >= 3)
            {
                Destroy(m_spawnMaps[0].thisMap);
                m_spawnMaps.RemoveAt(0);
            }
        }
    }

    public void DestroyAnPlayer(GameObject PlayerNeedDestroy)
    {
        CharacterManager anPlayer = PlayerNeedDestroy.GetComponent<CharacterManager>();
        if (anPlayer != null)
        {
            m_characs.Remove(anPlayer);
            Destroy(PlayerNeedDestroy);
            AudioManager.instance.PlaySfxAudio1shot(AudioManager.instance.DestroyAnPlayer);

            // Tạo hiệu ứng Destroy cho Player

            GameObject deathObj = Instantiate(PlayerDead, PlayerNeedDestroy.transform.position + new Vector3(0, 0.7f, 0), Quaternion.identity);
            Destroy(deathObj, 1f);
        }
    }

    public void CreateNewPlayer(GameObject PlayerNeedCreate, Transform trans)
    {
        GameObject newPlayer = Instantiate(PlayerNeedCreate, trans.position, Quaternion.identity);

        // Thêm player mới vào danh sách trong GameManager
        m_characs.Add(newPlayer.GetComponent<CharacterManager>());

        // Đặt AllCharacters làm parent của Player mới
        newPlayer.transform.SetParent(allCharactersTransform);
        AudioManager.instance.PlaySfxAudio1shot(AudioManager.instance.CreateAnPlayer);
    }

    public void GameOver()
    {
        // Bắt đầu Coroutine để tạo độ trễ trước khi hiển thị GameOverPanel
        StartCoroutine(ShowGameOverPanelAfterDelay(2f));
    }

    IEnumerator ShowGameOverPanelAfterDelay(float delay)
    {
        // Chờ trong một khoảng thời gian
        yield return new WaitForSeconds(delay);
        Time.timeScale = 0f;
        // Hiển thị GameOverPanel
        GameOverPanel.SetActive(true);
    }
}