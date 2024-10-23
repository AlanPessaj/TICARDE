using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OvniController_FG : MonoBehaviour
{
    public float orbitSpeed = 50f;
    public float orbitDuration = 3f;
    private float elapsedTime = 0f;
    private bool isOrbiting = true;

    void Start()
    {
        
    }

    void Update()
    {
        if (isOrbiting)
        {
            transform.RotateAround(transform.parent.position, Vector3.up, orbitSpeed * Time.deltaTime);
            elapsedTime += Time.deltaTime;
            if (elapsedTime >= orbitDuration)
            {
                isOrbiting = false;
            }
        }
    }
}
