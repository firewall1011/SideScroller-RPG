using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalMove : MonoBehaviour
{
    public float speed = 0f;
    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(transform.localScale.x, 0, 0) * speed * Time.deltaTime;
    }
}
