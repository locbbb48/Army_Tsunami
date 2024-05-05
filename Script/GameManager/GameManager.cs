using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class GameManager : MonoBehaviour
{
    public GameObject GameOverPanel;
    [SerializeField] private GameObject PlayerDead;
    public GameObject BoxCol2;
    public GameObject BoxCol1;

    public Transform allCharactersTransform;
    public List<CharacterManager> m_characs; // Số Char thực chạy.
    public List<GameObject> charTypes; // Các loại Chars.


    [SerializeField] private List<SpawnMap> m_spawnMaps; // Số lượng map thực chạy.
    [SerializeField] private List<SpawnMap> mapTypes;    // thể loại map.
    [SerializeField] private List<SpawnMap> temptMaps = new List<SpawnMap>();

    public int attackCount = 0; // số attack để kill object.
    public float TimeToReseAattackCount = 3f;
    public int thisTurnNeedJumpForAll = 0; // quản lí lượt nhảy, các char không bị lỗi đứng yên không nhảy.

    private void Awake()
    {
        m_characs = new List<CharacterManager>();
        m_spawnMaps = new List<SpawnMap>();
    }

    private void OnEnable()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in players)
        {
            m_characs.Add(player.GetComponent<CharacterManager>());
        }

        GameObject[] maps = GameObject.FindGameObjectsWithTag("SpawnMap");
        foreach (GameObject map in maps)
        {
            m_spawnMaps.Add(map.GetComponent<SpawnMap>());
        }
    }

    void Update()
    {
        if (m_characs.Count == 0 || m_characs[0] == null)
        {
            GameOver();
        }
        else
        {
            SpawnNewMap();

            ResetAttackCount();
        }
    }

    private void FixedUpdate()
    {
        if (m_characs.Count == 0 || m_characs[0] == null)
        {
            Debug.Log("m_chars[0] died");
        }
        else
        {
            SortListm_Chars();
        }
    }

    private void SortListm_Chars()
    {
        m_characs.Sort((a, b) =>
        {
            float distA = Vector3.Distance(a.transform.position, BoxCol1.transform.position);
            float distB = Vector3.Distance(b.transform.position, BoxCol1.transform.position);
            return distA.CompareTo(distB);
        });
    }

    private void ResetAttackCount()
    {
        if(attackCount > 0)
        {
            StartCoroutine(ReSetAttackCountbyTime(TimeToReseAattackCount));
        }
    }

    private void SpawnNewMap()
    {
        if (m_spawnMaps.Count > 0 && m_characs.Count > 0)
        {
            Vector2 CreatNewMapPoint = m_spawnMaps[m_spawnMaps.Count - 1].CreateNewMapPoint;
            CreatNewMapPoint.x += m_spawnMaps[m_spawnMaps.Count - 1].lengthMap;
            if (m_spawnMaps[m_spawnMaps.Count - 1].isCreateMap(m_characs[0].transform.position))
            {
                Destroy(m_spawnMaps[m_spawnMaps.Count - 1].m_CreateNewMapPoint);
                SpawnMap mapType; 
                do
                {
                    mapType = mapTypes[Random.Range(0, mapTypes.Count)];
                }
                while (temptMaps.Contains(mapType)); // Chọn một map chưa được spawn
                temptMaps.Add(mapType);
                if(temptMaps.Count >= mapTypes.Count ) {
                    temptMaps.Clear();
                }
                GameObject newMap = Instantiate(mapType.thisMap, CreatNewMapPoint, Quaternion.identity);
                if (newMap != null)
                {
                    m_spawnMaps.Add(newMap.GetComponent<SpawnMap>());
                    Debug.Log("NewMap");
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

            // Tạo hiệu ứng Destroy
            GameObject deathObj = Instantiate(PlayerDead, PlayerNeedDestroy.transform.position + new Vector3(0, 0.7f, 0), Quaternion.identity);
            Destroy(deathObj, 1f);
        }
    }

    public void CreateNewPlayer(GameObject PlayerNeedCreate, float PosY = 0f)
    {
        Vector3 Pos = m_characs[0].transform.position;
        Pos.x = m_characs[0].transform.position.x - m_characs.Count / 3;
        if(Pos.x < m_characs[0].transform.position.x - 4)
        {
            Pos.x = m_characs[0].transform.position.x - 4;
        }
        if(PosY != 0f)
        {
            Pos.y = PosY;
        }
        GameObject newPlayer = Instantiate(PlayerNeedCreate, Pos, Quaternion.identity);

        // Thêm player mới vào danh sách trong GameManager
        m_characs.Add(newPlayer.GetComponent<CharacterManager>());

        // Đặt AllCharacters làm parent của Player mới
        newPlayer.transform.SetParent(allCharactersTransform);
        AudioManager.instance.PlaySfxAudio1shot(AudioManager.instance.CreateAnPlayer);
    }

    public void CreateBigPlayer(GameObject PlayerNeedCreate, Vector3 defaultPos = default(Vector3))
    {
        Vector3 Pos;
        if (defaultPos == default(Vector3))
        {
            Pos = m_characs[0].transform.position;
        }
        else
        {
            Pos = defaultPos;
        }
        GameObject newPlayer = Instantiate(PlayerNeedCreate, Pos, Quaternion.identity);
    }

    // Hiển thị GameOverPanel
    public void GameOver()
    {
        StartCoroutine(ShowGameOverPanelAfterDelay(2f));
    }

    IEnumerator ShowGameOverPanelAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Time.timeScale = 0f;
        //GameOverPanel
        GameOverPanel.SetActive(true);
    }

    IEnumerator ReSetAttackCountbyTime(float delay)
    {
        yield return new WaitForSeconds(delay);
        attackCount = 0;
    }
}