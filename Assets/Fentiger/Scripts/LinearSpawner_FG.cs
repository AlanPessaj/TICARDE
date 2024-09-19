using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinearSpawner_FG : MonoBehaviour
{
    public GameObject thing;
    float timer = 0;
    public float spawnRate;
    public bool changedSide = false;
    public bool randomSpawnSide;
    void Start()
    {
        timer = Random.Range(0f, 1f);
        if (randomSpawnSide)
        {
            changedSide = Random.Range(0, 2) == 1;
            Debug.Log(changedSide);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (timer <= 0)
        {
            if(randomSpawnSide && changedSide)
            {
                Instantiate(thing, transform.position + new Vector3(0, -2.7f, 16.5f), Quaternion.Euler(thing.transform.eulerAngles + new Vector3(0, 180, 0)), transform);
            }
            else
            {
                Instantiate(thing, transform.position + new Vector3(0, -2.7f, -16.5f), thing.transform.rotation, transform);
            }
            
            timer = Random.Range(spawnRate/4,spawnRate);
        }
        timer -= Time.deltaTime;
    }
}
