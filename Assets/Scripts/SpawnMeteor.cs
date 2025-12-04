using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMeteor : MonoBehaviour
{
    [SerializeField]
    private GameObject meteor;
    [SerializeField]
    private float xLimit = 7;
    [SerializeField]
    private float minDelay = 1;
    [SerializeField]
    private float maxDelay = 3;
    [SerializeField]
    private int scoreThreshold1 = 10; // Điểm để spawn 2 meteor
    [SerializeField]
    private int scoreThreshold2 = 20; // Điểm để spawn 3 meteor
    [SerializeField]
    private int scoreThreshold3 = 30; // Điểm để spawn 4 meteor
    
    private GameManager gameManager;

    void Start()
    {
        gameManager = FindAnyObjectByType<GameManager>();
        if (meteor != null)
            Spawn();
    }

    void Spawn()
    {
        if (meteor == null || Time.timeScale == 0) return; // Không spawn khi game pause
        
        // Tính số lượng meteor cần spawn dựa trên điểm
        int meteorCount = GetMeteorCount();
        
        // Spawn nhiều meteor với vị trí random
        for (int i = 0; i < meteorCount; i++)
        {
            float xOffset = Random.Range(-xLimit, xLimit);
            Instantiate(meteor, transform.position + new Vector3(xOffset, 0, 0), Quaternion.identity);
        }
        
        Invoke(nameof(Spawn), Random.Range(minDelay, maxDelay));
    }

    private int GetMeteorCount()
    {
        if (gameManager == null) return 1;
        
        float score = gameManager.GetScore();
        
        if (score >= scoreThreshold3)
            return 4;
        else if (score >= scoreThreshold2)
            return 3;
        else if (score >= scoreThreshold1)
            return 2;
        else
            return 1;
    }
}
