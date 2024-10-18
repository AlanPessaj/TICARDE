using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShitDespawner_FG : MonoBehaviour
{
    float timer = 2f;
    bool timerOn;
    void Update()
    {
        if (transform.position.y < -15)
        {
            Destroy(gameObject);
        }
        if (timerOn)
        {
            if (timer <= 0)
            {
                Destroy(gameObject);
            }
            timer -= Time.deltaTime;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer != LayerMask.NameToLayer("Player"))
        {
            timerOn = true;
            transform.parent = collision.transform;
        }
    }
}
