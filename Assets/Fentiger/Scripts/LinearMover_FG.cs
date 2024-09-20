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

    void Start()
    {
        rotation = Random.Range(-1f, 1f);
    }

    void Update()
    {

        if (!transform.parent.GetComponent<LinearSpawner_FG>().changedSide)
        {
            if (transform.position.z < 18)
            {
                transform.Translate(0, 0, speed * Time.deltaTime, Space.World);
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
                transform.Translate(0, 0, -speed * Time.deltaTime, Space.World);
            }
            else
            {
                Destroy(gameObject);
            }
        }


        if (gameObject.name == "nenufar(Clone)")
        {
            transform.Rotate(0, rotation, 0);
            if (transform.childCount > 0)
            {
                if (time == 3f && timerCoroutine == null)
                {
                    timerCoroutine = StartCoroutine(Timer());
                }
                else if (time <= 0)
                {
                    transform.GetChild(0).parent = null;
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
}