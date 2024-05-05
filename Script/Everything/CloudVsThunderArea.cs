using UnityEngine;

public class CloudVsThunderArea : MonoBehaviour
{
    [SerializeField] private float speed = 1.0f;
    [SerializeField] private float amplitude = 1.0f;

    private Vector2 startPosition;
    [SerializeField] private Transform WarningPoint;
    [SerializeField] private Transform CheckPoint;
    private GameManager gameManager;


    void Start()
    {
        // Lưu vị trí ban đầu của đối tượng
        startPosition = transform.position;
        gameManager = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        // Tính toán vị trí mới dựa trên thời gian và tốc độ
        float movementX = Mathf.Sin(Time.time * speed) * amplitude*2;
        float movementY = Mathf.Cos(Time.time * speed) * amplitude/2;

        // Cập nhật vị trí của đối tượng
        transform.position = startPosition + new Vector2(movementX, movementY);


        if (gameManager.m_characs.Count > 0 && gameManager.m_characs[0] != null)
        {
            GameObject bigPlayer = GameObject.FindGameObjectWithTag("bigPlayer");
            if ((gameManager.m_characs[0].transform.position.x > WarningPoint.position.x
                    || (bigPlayer != null && bigPlayer.transform.position.x > WarningPoint.position.x)))
            {
                AudioManager.instance.PlaySfxAudio(AudioManager.instance.Clouds);

            }
            if ((gameManager.m_characs[0].transform.position.x > CheckPoint.position.x
                    || (bigPlayer != null && bigPlayer.transform.position.x > CheckPoint.position.x)))
            {
                AudioManager.instance.PauseSfxAudio(AudioManager.instance.Clouds);

            }
        }
    }
}
