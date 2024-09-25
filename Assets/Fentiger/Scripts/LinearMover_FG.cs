using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinearMover_FG : MonoBehaviour
{
    public float speed;
    public bool destroyable;
    private float rotation;
    private float time = 3f;
    private Coroutine timerCoroutine;
    private bool movingForward;
    public Generator_FG generator;
    public LinearSpawner_FG spawner;

    void Start()
    {
        rotation = Random.Range(-1f, 1f);
        movingForward = !transform.parent.GetComponent<LinearSpawner_FG>().changedSide;
    }

    void Update()
    {
        if (movingForward)
        {
            if (transform.position.z < 18)
            {
                transform.Translate(0, 0, (speed + spawner.difficulty/10) * Time.deltaTime, Space.World);
            }
            else
            {
                Destroy(gameObject);
            }
        }
        else
        {
            if (transform.position.z > -18)
            {
                transform.Translate(0, 0, (speed + generator.difficulty / 10) * -Time.deltaTime, Space.World);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        if (gameObject.name == "LillyPad(Clone)")
        {
            transform.Rotate(0, rotation, 0);
            if (transform.childCount > 1)
            {
                if (time == 3f && timerCoroutine == null)
                {
                    timerCoroutine = StartCoroutine(Timer());
                }
                else if (time <= 0)
                {
                    transform.GetChild(1).parent = null;
                    Destroy(gameObject);
                }
            }
        }
    }

    private IEnumerator Timer()
    {
        while (time > 0)
        {
            yield return new WaitForSecondsRealtime(1f);
            time--;
            //Animacion y sonido de irse rompiendo
        }
        timerCoroutine = null;
    }

    private void Awake()
    {
        generator = GameObject.Find("GAMEMANAGER").GetComponent<Generator_FG>();
    }
}
