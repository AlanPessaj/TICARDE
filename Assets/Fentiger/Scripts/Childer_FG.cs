using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Childer_FG : MonoBehaviour
{
    Generator_FG generator;
    void Start()
    {
        transform.parent = generator.lastTileCreated.transform;
    }

    private void Awake()
    {
        generator = GameObject.Find("GAMEMANAGER").GetComponent<Generator_FG>();
    }
}
