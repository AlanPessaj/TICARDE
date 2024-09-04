using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitManager_FT : MonoBehaviour
{
    public Collider[] hColliders = new Collider[3];
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void Update()
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
                            //Debug.Log("pego en smash, drive y globo");
                            collided = true;
                            break;
                        }
                        else
                        {
                            //Debug.Log("pego en smash y drive");
                            collided = true;
                        }
                    }
                    else
                    {
                        //Debug.Log("pego en smash y globo");
                        collided = true;
                    }
                }
                else
                {
                    //Debug.Log("pego en drive y globo");
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
                    //Debug.Log("pego en smash");
                    break;
                case 1:
                    //Debug.Log("pego en drive");
                    break;
                case 2:
                    //Debug.Log("pego en globo");
                    break;
            }
        }
        hColliders = new Collider[3];
    }
}
