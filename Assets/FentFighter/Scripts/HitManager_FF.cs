using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitManager_FF : MonoBehaviour
{
    public Collider[] hColliders = new Collider[3];
    public bool blocking;
    float immunityTimer;
    public float XPMultiplier;
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
                                TakeDamage(2.5f);
                            }
                            else
                            {
                                Debug.Log("pego en cabeza, pecho y piernas");
                                TakeDamage(2.5f);
                            }
                            collided = true;
                            break;
                        }
                        else
                        {
                            if (blocking)
                            {
                                Debug.Log("(block cabeza pecho)");
                            }
                            else
                            {
                                Debug.Log("pego en cabeza y pecho");
                                TakeDamage(2.5f);
                            }
                            collided = true;
                        }
                    }
                    else
                    {
                        if (blocking)
                        {
                            Debug.Log("pego en piernas (block cabeza)");
                            TakeDamage(2.5f);
                        }
                        else
                        {
                            Debug.Log("pego en cabeza y piernas");
                            TakeDamage(2.5f);
                        }
                        collided = true;
                    }
                }
                else
                {
                    if (blocking)
                    {
                        Debug.Log("pego en piernas (block pecho)");
                        TakeDamage(2.5f);
                    }
                    else
                    {
                        Debug.Log("pego en pecho y piernas");
                        TakeDamage(2.5f);
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
                    }
                    else
                    {
                        Debug.Log("pego en cabeza");
                        TakeDamage(2.5f);
                    }
                    break;
                case 1:
                    if (blocking)
                    {
                        Debug.Log("(block pecho)");
                    }
                    else
                    {
                        Debug.Log("pego en pecho");
                        TakeDamage(2.5f);
                    }
                    break;
                case 2:
                    if (blocking)
                    {
                        Debug.Log("(block piernas)");
                    }
                    else
                    {
                        Debug.Log("pego en piernas");
                        TakeDamage(2.5f);
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

    void TakeDamage(float damage)
    {
        if (immunityTimer <= 0)
        {
            GetComponent<UIManager_FF>().ChangeHealth(-damage);
            GetComponent<PlayerController_FF>().otherPlayer.GetComponent<UIManager_FF>().AddXP(damage * XPMultiplier);
            immunityTimer = 1f;
        }
    }
}
