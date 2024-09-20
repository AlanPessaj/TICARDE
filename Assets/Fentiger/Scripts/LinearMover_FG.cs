using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinearMover_FG : MonoBehaviour
{
    public float speed;
    public bool destroyable;
    float rotatioN;
    float time = 3f;
    // Start is called before the first frame update
    void Start()
    {
         rotatioN = Random.Range(-1f, 1f);
    }

    // Update is called once per frame
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
            transform.Rotate(0, rotatioN, 0);
        }

        if (transform.childCount > 0 && gameObject.name == "nenufar(Clone)")
        {
            StartCoroutine(timer());
        }
        else
        {
            StopCoroutine(timer());
        }
    }

    IEnumerator timer()
    {
        while(time > 0)
        {
            yield return new WaitForSeconds(1f);
            time--;
            Debug.Log("-1s " + time);
        }
        transform.GetChild(0).parent = null;
        Destroy(gameObject);
    }
}
