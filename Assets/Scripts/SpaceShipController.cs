using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShipController : MonoBehaviour
{
    [SerializeField]
    private GameManager gameManager;
    [SerializeField]
    private GameObject bullet;
    private AudioSource shoot;

    [SerializeField]
    private float speed = 10;
    [SerializeField]
    private float xLimit = 7;
    [SerializeField]
    private float yLimit = 4;

    [SerializeField]
    private float reload = 0.5f;
    private float elapseTime = 0;
    private Vector3 startPosition;

    void Start()
    {
        shoot = GetComponent<AudioSource>();
        if (gameManager == null)
            gameManager = FindAnyObjectByType<GameManager>();
        startPosition = transform.position; // Lưu vị trí ban đầu
    }

    public void ResetPosition()
    {
        transform.position = startPosition;
        elapseTime = 0;
    }

    void Update()
    {
        if (Time.timeScale == 0) return; // Không xử lý input khi game đã pause

        elapseTime += Time.deltaTime;
        float xInput = Input.GetAxis("Horizontal");
        float yInput = Input.GetAxis("Vertical");
        transform.Translate(xInput * speed * Time.deltaTime, yInput * speed * Time.deltaTime, 0);

        Vector3 position = transform.position;
        position.x = Mathf.Clamp(position.x, -xLimit, xLimit);
        position.y = Mathf.Clamp(position.y, -yLimit, yLimit);

        transform.position = position;

        if (Input.GetButtonDown("Jump") && elapseTime > reload && bullet != null)
        {
            if (shoot != null)
                shoot.Play();
            Instantiate(bullet, transform.position + new Vector3(0, 1.2f, 0), Quaternion.identity);
            elapseTime = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Chỉ game over khi va chạm với meteor
        if (collision.gameObject.CompareTag("Meteor") || collision.gameObject.GetComponent<Meteor>() != null)
        {
            if (gameManager != null)
                gameManager.GameOver();
        }
    }
}
