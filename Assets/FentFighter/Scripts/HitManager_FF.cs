using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitManager_FF : MonoBehaviour
{
    public Collider[] hColliders = new Collider[3];
    public bool blocking;
    float immunityTimer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < hColliders.Length; i++)
        {
            bool collided = false;
            if (hColliders[i] == null) {continue;}
            for (int o = i + 1; o < hColliders.Length; o++)
            {
                if (hColliders[o] == null) {continue;}
                if (i == 0)
                {
                    if (o == 1)
                    {
                        if (hColliders[o + 1] != null)
                        {
                            if (blocking)
                            {
                                Debug.Log("pego en piernas (block pecho cabeza)");
                                damage();
                            }
                            else
                            {
                                Debug.Log("pego en cabeza, pecho y piernas");
                                damage();
                            }
                            collided = true;
                            break;
                        }
                        else
                        {
                            if (blocking)
                            {
                                Debug.Log("(block cabeza pecho)");
                                damage();
                            }
                            else
                            {
                                Debug.Log("pego en cabeza y pecho");
                                damage();
                            }
                            collided = true;
                        }
                    }
                    else
                    {
                        if (blocking)
                        {
                            Debug.Log("pego en piernas (block cabeza)");
                            damage();
                        }
                        else
                        {
                            Debug.Log("pego en cabeza y piernas");
                            damage();
                        }
                        collided = true;
                    }
                }
                else
                {
                    if (blocking)
                    {
                        Debug.Log("pego en piernas (block pecho)");
                        damage();
                    }
                    else
                    {
                        Debug.Log("pego en pecho y piernas");
                        damage();
                    }
                    collided = true;
                }
            }
            if (collided)
            {
                break;
            }
            switch (i)
            {
                case 0:
                    if (blocking)
                    {
                        Debug.Log("(block cabeza)");
                        damage();
                    }
                    else
                    {
                        Debug.Log("pego en cabeza");
                        damage();
                    }
                    break;
                case 1:
                    if (blocking)
                    {
                        Debug.Log("(block pecho)");
                        damage();
                    }
                    else
                    {
                        Debug.Log("pego en pecho");
                        damage();
                    }
                    break;
                case 2:
                    if (blocking)
                    {
                        Debug.Log("(block pecho)");
                        damage();
                    }
                    else
                    {
                        Debug.Log("pego en pecho");
                        damage();
                    }
                    break;
            }
        }
        hColliders = new Collider[3];
        if (immunityTimer > 0)
        {
            immunityTimer -= Time.deltaTime;
        }
        else
        {
            immunityTimer = 0;
        }
    }

    void damage()
    {
        if (immunityTimer <= 0)
        {
            gameObject.GetComponent<HealthManager_FF>().health -= 20;
            gameObject.GetComponent<HealthManager_FF>().UpdateHealth();
            immunityTimer = 1f;
        }
    }
}
