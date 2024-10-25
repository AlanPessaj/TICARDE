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
    private bool oscillate = false;
    private float t = 0f;
    public float oscillateSpeed = 2f;

    private void Update()
    {
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
            transform.localPosition = Vector3.Lerp(transform.localPosition, new Vector3(11.4f, 6.15f, 9f), t);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(new Vector3(-1.9f, 102.2f, 308f)), t);
            if (t >= 0.1f)
            {
                hasFinishedOrbiting = false;
                oscillate = true;
            }
        }

        if (oscillate)
        {
            t += Time.deltaTime * oscillateSpeed;
            transform.localPosition = Vector3.Lerp(new Vector3(11.4f, 6.15f, 9f), new Vector3(8.5f, -6.61f, 4.84f), Mathf.PingPong(t, 1f));
        }
    }
}
