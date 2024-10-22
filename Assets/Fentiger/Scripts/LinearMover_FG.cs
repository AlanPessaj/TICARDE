using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinearMover_FG : MonoBehaviour
{
    public float speed;
    private float rotation;
    public float time = 100;
    private bool movingForward;
    public Generator_FG generator;
    public LinearSpawner_FG spawner;

    void Start()
    {
        rotation = Random.Range(-1f, 1f);
        if (gameObject.name == "Seagull(Clone)")
        {
            movingForward = !transform.GetComponent<SeagullController_FG>().leftSide;
        }
        else
        {
            movingForward = !transform.parent.GetComponent<LinearSpawner_FG>().changedSide;
            speed = spawner.spawnRate / spawner.time;

        }
        if (gameObject.name == "LillyPad(Clone)")
        {
            time = Mathf.LerpUnclamped(18f, 9f, Mathf.InverseLerp(1.2f, 3.6f, speed)) / 6;
        }
        else if (gameObject.name == "BrokenLog(Clone)")
        {
            time = Random.Range(Mathf.LerpUnclamped(18f, 9f, Mathf.InverseLerp(1.2f, 3.6f, speed)) / 9, Mathf.LerpUnclamped(18f, 9f, Mathf.InverseLerp(1.2f, 3.6f, speed)) / 4.5f);
        }
    }
    void Update()
    {
        if (movingForward)
        {
            if (transform.position.z < 18)
            {
                transform.Translate(0, 0, speed * Time.deltaTime, Space.World);
            }
            else
            {
                if (transform.childCount > 2)
                {
                    transform.GetChild(1).parent = null;
                }
                if (transform.childCount > 1)
                {
                    transform.GetChild(1).parent = null;
                }
                Destroy(gameObject);
            }
        }
        else
        {
            if (transform.position.z > -18)
            {
                transform.Translate(0, 0, speed * -Time.deltaTime, Space.World);
            }
            else
            {
                if (transform.childCount > 2)
                {
                    transform.GetChild(1).parent = null;
                }
                if (transform.childCount > 1)
                {
                    transform.GetChild(1).parent = null;
                }
                Destroy(gameObject);
            }
        }

        if (gameObject.name == "LillyPad(Clone)")
        {
            transform.Rotate(0, rotation, 0);
            if (transform.childCount > 1)
            {
                time -= Time.deltaTime;
            }
        }
        else if(gameObject.name == "BrokenLog(Clone)")
        {
            if (transform.childCount > 1)
            {
                time -= Time.deltaTime;
            }
        }
        if (time <= 0)
        {
            if (transform.childCount > 2)
            {
                transform.GetChild(1).parent = null;
            }
            transform.GetChild(1).parent = null;
            Destroy(gameObject);
        }
    }

    private void Awake()
    {
        generator = GameObject.Find("GAMEMANAGER").GetComponent<Generator_FG>();
    }
}
