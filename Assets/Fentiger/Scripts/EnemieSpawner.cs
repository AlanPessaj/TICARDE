using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemieSpawner : MonoBehaviour
{
    public Generator_FG generator;
    public GameObject enemie;

    void Start()
    {
        if (Random.Range(0,2) == 1)
        {
            Instantiate(enemie, new Vector3(transform.position.x, transform.position.y, Random.Range(-12, 13)), Quaternion.identity, transform);
        }
    }

    void Update()
    {
        
    }

    private void Awake()
    {
        generator = GameObject.Find("GAMEMANAGER").GetComponent<Generator_FG>();
    }
}
