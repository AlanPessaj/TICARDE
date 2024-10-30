using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Childer_FG : MonoBehaviour
{
    Generator_FG generator;

    private void Update()
    {
        if (generator.camara.position.x - 50 > transform.position.x)
        {
            Destroy(gameObject);
        }
    }

    private void Awake()
    {
        generator = GameObject.Find("GAMEMANAGER").GetComponent<Generator_FG>();
    }
}
