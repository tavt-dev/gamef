using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    [SerializeField]
    private float lifetime = 5f; // Thời gian sống của meteor (giây)
    [SerializeField]
    private float minScale = 0.5f; // Kích thước nhỏ nhất
    [SerializeField]
    private float maxScale = 1.5f; // Kích thước lớn nhất
    [SerializeField]
    private GameObject spawnEffect; // Hiệu ứng khi meteor spawn
    [SerializeField]
    private float spawnEffectChance = 0.3f; // 30% cơ hội có hiệu ứng
    [SerializeField]
    private float effectDuration = 5f; // Thời gian hiệu ứng chạy
    private float speed;
    private Rigidbody2D rb;

    void Start()
    {
        // Random kích thước
        float randomScale = Random.Range(minScale, maxScale);
        transform.localScale = new Vector3(randomScale, randomScale, 1f);
        
        speed = Random.Range(-5f, -2f);
        rb = GetComponent<Rigidbody2D>();
        if (rb != null)
            rb.velocity = new Vector2(0, speed);
        
        // Đôi khi có hiệu ứng khi spawn
        if (spawnEffect != null && Random.Range(0f, 1f) < spawnEffectChance)
        {
            GameObject effectInstance = Instantiate(spawnEffect, transform.position, Quaternion.identity);
            Destroy(effectInstance, effectDuration);
        }
        
        // Tự động destroy sau một khoảng thời gian
        Destroy(gameObject, lifetime);
    }
}
