using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField]
    private Text gameOverText;
    [SerializeField]
    private Text scoreText;
    [SerializeField]
    private SpaceShipController spaceShip;
    private float score = 0;
    private bool isGameOver = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (spaceShip == null)
            spaceShip = FindAnyObjectByType<SpaceShipController>();
        if (scoreText != null)
            scoreText.text = "0";
        if (gameOverText != null)
            gameOverText.gameObject.SetActive(false);
    }

    public void AddScore()
    {
        if (isGameOver) return; // Không cộng điểm khi game đã over
        
        score++;
        if (scoreText != null)
            scoreText.text = score.ToString();
    }

    public float GetScore()
    {
        return score;
    }

    public void GameOver()
    {
        if (isGameOver) return; // Tránh gọi nhiều lần
        
        isGameOver = true;
        if (audioSource != null)
            audioSource.Stop();
        if (gameOverText != null)
            gameOverText.gameObject.SetActive(true);
        Time.timeScale = 0;
    }

    public void PlayAgain()
    {
        isGameOver = false;
        
        // Xóa hết meteor trên màn hình
        ClearAllMeteors();
        
        // Reset vị trí tàu về ban đầu
        if (spaceShip != null)
            spaceShip.ResetPosition();
        
        if (audioSource != null)
            audioSource.Play();
        score = 0;
        if (scoreText != null)
            scoreText.text = score.ToString();
        if (gameOverText != null)
            gameOverText.gameObject.SetActive(false);
        Time.timeScale = 1;
    }

    private void ClearAllMeteors()
    {
        // Tìm và destroy tất cả meteor
        Meteor[] meteors = FindObjectsByType<Meteor>(FindObjectsSortMode.None);
        foreach (Meteor meteor in meteors)
        {
            if (meteor != null && meteor.gameObject != null)
                Destroy(meteor.gameObject);
        }
        
        // Xóa cả bullet còn lại
        Bullet[] bullets = FindObjectsByType<Bullet>(FindObjectsSortMode.None);
        foreach (Bullet bullet in bullets)
        {
            if (bullet != null && bullet.gameObject != null)
                Destroy(bullet.gameObject);
        }
    }
}
