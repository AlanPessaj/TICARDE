using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinearMover_FG : MonoBehaviour
{
    public float speed;
    private float rotation;
    public float time = 3;
    private bool movingForward;
    public Generator_FG generator;
    public LinearSpawner_FG spawner;
    public bool debugTest;

    void Start()
    {
        speed = generator.Levels[generator.Level].speed;
        rotation = Random.Range(-1f, 1f);
        if (gameObject.name == "Seagull(Clone)")
        {
            movingForward = !transform.GetComponent<SeagullController_FG>().leftSide;
        }
        else
        {
            movingForward = !transform.parent.GetComponent<LinearSpawner_FG>().changedSide;

        }

        if (gameObject.name == "LillyPad(Clone)")
        {
            time = 5 / speed;
        }
        else if (gameObject.name == "BrokenLog(Clone)")
        {
            time = Random.Range((5 / speed) - 0.2f, (5 / speed) + 0.5f);
        }

    }
    void Update()
    {
        speed = generator.Levels[generator.Level].speed;
        if (movingForward)
        {
            if (transform.position.z < 18)
            {
                if (gameObject.name != "Seagull(Clone)")
                {
                    transform.Translate(0, 0, speed * Time.deltaTime, Space.World);
                }
                else
                {
                    transform.Translate(0, 0, 5 * Time.deltaTime, Space.World);
                }
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
                if (gameObject.name != "Seagull(Clone)")
                {
                    transform.Translate(0, 0, speed * -Time.deltaTime, Space.World);
                }
                else
                {
                    transform.Translate(0, 0, 5 * -Time.deltaTime, Space.World);
                }
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
        else if (gameObject.name == "BrokenLog(Clone)")
        {
            if (transform.childCount > 1)
            {
                time -= Time.deltaTime;
            }
        }
        if (time <= 0)
        {
            if (transform.childCount > 1)
            {
                transform.GetChild(1).parent = null;
            }
            Destroy(gameObject);
        }
    }

    private void Awake()
    {
        generator = GameObject.Find("GAMEMANAGER").GetComponent<Generator_FG>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (gameObject.name.Contains("Hippo") && (collision.gameObject.name.Contains("Log(Clone)") || collision.gameObject.name.Contains("LillyPad")))
        {
            Destroy(collision.gameObject);
        }
    }
}