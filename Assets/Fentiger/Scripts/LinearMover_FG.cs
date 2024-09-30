using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinearMover_FG : MonoBehaviour
{
    public float speed;
    private float rotation;
    public float time;
    private bool movingForward;
    public Generator_FG generator;
    public LinearSpawner_FG spawner;
    float initialSpeed;

    void Start()
    {
        initialSpeed = speed;
        rotation = Random.Range(-1f, 1f);
        if (gameObject.name == "Seagull(Clone)")
        {
            movingForward = !transform.GetComponent<SeagullController>().leftSide;
        }
        else
        {
            movingForward = !transform.parent.GetComponent<LinearSpawner_FG>().changedSide;
        }
        if (gameObject.name == "LillyPad(Clone)")
        {
            time = 3f;
        }
        else
        {
            time = Random.Range(2f, 4f);
        }
    }

    void Update()
    {
        if (movingForward)
        {
            if (transform.position.z < 18 && gameObject.name != "Seagull(Clone)")
            {
                transform.Translate(0, 0, Mathf.Clamp((speed + spawner.difficulty / 10), 0f, initialSpeed * 3f) * Time.deltaTime, Space.World);
            }
            else if (gameObject.name == "Seagull(Clone)" && transform.position.z < 18)
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
                transform.Translate(0, 0, Mathf.Clamp((speed + spawner.difficulty / 10), 0f, initialSpeed*3f) * -Time.deltaTime, Space.World);
            }
            else if (gameObject.name == "Seagull(Clone)" && transform.position.z > -18)
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
