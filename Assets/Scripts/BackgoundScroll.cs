using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgoundScroll : MonoBehaviour
{
    [SerializeField]
    private float speed = -2;
    [SerializeField]
    private float lower = -20;
    [SerializeField]
    private float upper = 40;

    void Update()
    {
        transform.Translate(0, speed * Time.deltaTime, 0);
        if (transform.position.y < lower)
        {
            transform.Translate(0, upper, 0);
        }
    }
}
