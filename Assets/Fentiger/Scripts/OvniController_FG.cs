using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OvniController_FG : MonoBehaviour
{
    bool isOrbiting = true;
    bool hasFinishedOrbiting = false;
    bool oscillate = false;
    bool leave = false;
    bool gotInPos = false;
    public float orbitSpeed;
    public float orbitDuration;
    float elapsedTime = 0f;
    public float movingDelay;
    float movingTime = 0f;
    float t = 0f;
    public float oscillateSpeed = 2f;
    public Material yellow;
    public Material red;
    GameObject laser;
    bool firstTime = true;
    float leaveTime = 0f;
    Vector3 leaveRotation;
    Vector3 leavePos;

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
            if (firstTime)
            {
                StartCoroutine(Laser());
                firstTime = false;
            }
        }

        if (leave)
        {
            if (!gotInPos)
            {
                leaveTime += Time.deltaTime;
                transform.localPosition = Vector3.Lerp(leavePos, new Vector3(0, 3, 4), leaveTime);
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, 0), leaveTime);
                if (leaveTime >= 1)
                {
                    gotInPos = true;
                    leaveTime = 0;
                    leavePos = transform.localPosition;
                }
            }
            else
            {
                leaveTime += Time.deltaTime;
                transform.localPosition = Vector3.Lerp(leavePos, new Vector3(0, 1, -8), leaveTime);
                if (leaveTime >= 1)
                {
                    Destroy(gameObject);
                }
            }
            // 1 -8
        }
    }

    IEnumerator Laser()
    {
        yield return new WaitForSeconds(4f);
        laser = transform.GetChild(1).gameObject;
        laser.SetActive(true);
        yield return new WaitForSeconds(4f);
        laser.GetComponent<Renderer>().material = yellow;
        yield return new WaitForSeconds(4f);
        laser.GetComponent<Renderer>().material = red;
        laser.layer = LayerMask.NameToLayer("Seagull");
        yield return new WaitForSeconds(1f);
        laser.SetActive(false);
        leave = true;
        leavePos = transform.localPosition;
        leaveRotation = transform.rotation.eulerAngles;
    }
}
