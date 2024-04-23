using UnityEngine;

public class CloudVsThunderArea : MonoBehaviour
{
    public float speed = 1.0f;
    public float amplitude = 1.0f;

    private Vector2 startPosition;

    void Start()
    {
        // Lưu vị trí ban đầu của đối tượng
        startPosition = transform.position;
    }

    void Update()
    {
        // Tính toán vị trí mới dựa trên thời gian và tốc độ
        float movementX = Mathf.Sin(Time.time * speed) * amplitude*2;
        float movementY = Mathf.Cos(Time.time * speed) * amplitude/2;

        // Cập nhật vị trí của đối tượng
        transform.position = startPosition + new Vector2(movementX, movementY);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("bigPlayer"))
        { 
            AudioManager.instance.PlaySfxAudio(AudioManager.instance.Clouds);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("bigPlayer"))
        { 
            AudioManager.instance.PauseSfxAudio(AudioManager.instance.Clouds);
        }
    }
}
