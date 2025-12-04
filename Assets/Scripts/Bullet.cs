using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private GameObject effect;
    [SerializeField]
    private float effectDuration = 1f; // Thời gian hiệu ứng chạy (giây)
    private GameManager gameManager;
    private AudioSource boom;
    [SerializeField]
    private float speed = 10;
    [SerializeField]
    private float lifetime = 3f; // Thời gian sống của đạn (giây)
    private Rigidbody2D rb;

    void Start()
    {
        boom = GetComponent<AudioSource>();
        gameManager = FindAnyObjectByType<GameManager>();
        rb = GetComponent<Rigidbody2D>();
        if (rb != null)
            rb.velocity = new Vector2(0, speed);
        
        // Tự động destroy sau một khoảng thời gian
        Destroy(gameObject, lifetime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Chỉ xử lý khi va chạm với meteor
        Meteor meteor = collision.gameObject.GetComponent<Meteor>();
        if (collision.gameObject.CompareTag("Meteor") || meteor != null)
        {
            // Tạo hiệu ứng nổ
            if (effect != null)
            {
                GameObject effectInstance = Instantiate(effect, transform.position, Quaternion.identity);
                // Tự động xóa hiệu ứng sau khi chạy xong
                Destroy(effectInstance, effectDuration);
            }
            
            if (boom != null)
                boom.Play();
            
            // Xóa meteor và đạn ngay lập tức
            Destroy(collision.gameObject);
            Destroy(gameObject);
            
            // Cộng điểm
            if (gameManager != null)
                gameManager.AddScore();
        }
    }
}
