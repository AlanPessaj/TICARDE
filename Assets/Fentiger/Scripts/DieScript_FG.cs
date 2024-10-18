using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieScript_FG : MonoBehaviour
{
    
    void Update()
    {
        transform.position += Vector3.up * 2.0f * Time.deltaTime;
        float newZ = transform.position.z + Mathf.Sin(Time.time) * 0.01f;
        transform.position = new Vector3(transform.position.x, transform.position.y, newZ);
        if (transform.position.y >= 15)
        {
            Destroy(gameObject);
        }
    }
}
