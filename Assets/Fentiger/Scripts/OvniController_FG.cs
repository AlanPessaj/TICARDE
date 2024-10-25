using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OvniController_FG : MonoBehaviour
{
    public float orbitSpeed;
    public float orbitDuration;
    private float elapsedTime = 0f;
    private bool isOrbiting = true;
    private bool hasFinishedOrbiting = false;
    public float movingDelay;
    private float movingTime = 0f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log(transform.position.ToString());
            Debug.Log("Posicion de spawn: " + transform.parent.position);
        }
        if (isOrbiting)
        {
            transform.RotateAround(transform.parent.position, Vector3.up, orbitSpeed * Time.deltaTime);
            elapsedTime += Time.deltaTime;
            if (elapsedTime >= orbitDuration)
            {
                isOrbiting = false;
                hasFinishedOrbiting = true;
            }
        }
        
        if (!isOrbiting && hasFinishedOrbiting)
        {
            movingTime += Time.deltaTime;
            float t = Mathf.Clamp01(movingTime / movingDelay);
            transform.position = Vector3.Lerp(transform.position, transform.parent.position + new Vector3(2.4f, -6.6f, -10), t);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(new Vector3(-1.9f, 102.2f, 308f)), t);
            if (t >= 1f)
            {
                hasFinishedOrbiting = false;
            }
        }
    }
}
