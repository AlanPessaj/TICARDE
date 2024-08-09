using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitManager_FF : MonoBehaviour
{
    public Collider[] hColliders = new Collider[3];
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < hColliders.Length; i++)
        {
            if (hColliders[i] == null) {continue;}
            for (int o = i + 1; o < hColliders.Length; o++)
            {
                if (hColliders[i] == null) {continue;}
                if (i == 0)
                {
                    if (o == 1)
                    {
                        if (hColliders[o + 1] != null)
                        {
                            //pego en cabeza, pecho y piernas
                        }
                        else
                        {
                            //pego en cabeza y pecho
                        }
                    }
                    else
                    {
                        //pego en cabeza y piernas
                    }
                }
                else
                {
                    //pego en pecho y piernas
                }
            }
            switch (i)
            {
                case 0:
                    //pego en cabeza
                    break;
                case 1:
                    //pego en pecho
                    break;
                case 2:
                    //pego en piernas
                    break;
            }
        }
        hColliders = new Collider[3];
    }
}
