using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinearSpawner_FG : MonoBehaviour
{
    public GameObject thing;
    float timer = 0;
    public float spawnRate;
    void Start()
    {
        //Instantiate(thing, transform.position + new Vector3(0, -2.5f, -13.5f), Quaternion.identity, transform);
    }

    // Update is called once per frame
    void Update()
    {
        if (timer <= 0)
        {
            Instantiate(thing, transform.position + new Vector3(0, -2.5f, -14f), Quaternion.identity, transform);
            timer = Random.Range(0.5f,spawnRate);
        }
        timer -= Time.deltaTime;
    }
}
