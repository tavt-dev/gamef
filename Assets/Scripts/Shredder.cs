using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shredder : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Chỉ destroy các object đi ra ngoài màn hình (bullet, meteor)
        if (collision.gameObject != null)
            Destroy(collision.gameObject);
    }
}
