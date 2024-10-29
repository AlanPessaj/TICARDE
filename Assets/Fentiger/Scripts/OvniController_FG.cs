using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OvniController_FG : MonoBehaviour
{
    bool isOrbiting = true;
    bool hasFinishedOrbiting = false;
    bool oscillate = false;
    bool leave = false;
    public float orbitSpeed;
    public float orbitDuration;
    float elapsedTime = 0f;
    public float movingTime = 0f;
    float oscillateTime = 0f;
    public float oscillateSpeed;
    public Material yellow;
    public Material red;
    GameObject laser;
    bool firstTime = true;
    float leaveTime = 0f;
    float leaveTargetTime;
    Vector3 leavePos;
    Quaternion leaveRot;
    Vector3 transitionPos;
    Quaternion transitionRot;

    private void Update()
    {
        if (isOrbiting)
        {
            elapsedTime += Time.deltaTime;
            transform.RotateAround(transform.parent.position, Vector3.up, orbitSpeed * Time.deltaTime);
            if (elapsedTime >= orbitDuration)
            {
                isOrbiting = false;
                hasFinishedOrbiting = true;
                transitionPos = transform.localPosition;
                transitionRot = transform.rotation;
            }
        }

        if (!isOrbiting && hasFinishedOrbiting)
        {
            movingTime += Time.deltaTime;
            transform.localPosition = Vector3.Lerp(transitionPos, new Vector3(11.4f, 6.15f, 9f), movingTime);
            transform.rotation = Quaternion.Lerp(transitionRot, Quaternion.Euler(new Vector3(-1.9f, 102.2f, 308f)), movingTime);
            if (movingTime >= 1f)
            {
                hasFinishedOrbiting = false;
                oscillate = true;
            }
        }

        if (oscillate)
        {
            oscillateTime += Time.deltaTime * oscillateSpeed;
            transform.localPosition = Vector3.Lerp(new Vector3(11.4f, 6.15f, 9f), new Vector3(8.5f, -6.61f, 4.84f), Mathf.PingPong(oscillateTime, 1f));
            if (firstTime)
            {
                StartCoroutine(Laser());
                firstTime = false;
            }
        }

        if (leave)
        {
            leaveTargetTime += Time.deltaTime;
            leaveTime += Time.deltaTime / 5;
            Vector3 leaveTarget = Vector3.Lerp(new Vector3(0, 3, 4), new Vector3(0, 1, -8), leaveTargetTime);
            transform.rotation = Quaternion.Lerp(leaveRot, Quaternion.identity, leaveTargetTime);
            transform.localPosition = Vector3.Lerp(leavePos, leaveTarget, leaveTime);
            leavePos = transform.localPosition;
            if (leaveTargetTime >= 1)
            {
                Destroy(gameObject);
            }
        }
    }

    IEnumerator Laser()
    {
        bool style = Random.Range(0, 2) == 1;
        if (style)
        {
            yield return new WaitForSeconds(4f);
        }
        else
        {
            yield return new WaitForSeconds(3f);
        }
        laser = transform.GetChild(1).gameObject;
        laser.SetActive(true);
        GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(Random.Range(2f,4f));
        laser.GetComponent<Renderer>().material = yellow;
        yield return new WaitForSeconds(Random.Range(2f, 4f));
        laser.GetComponent<Renderer>().material = red;
        laser.layer = LayerMask.NameToLayer("Seagull");
        yield return new WaitForSeconds(Random.Range(1f,1.5f));
        laser.SetActive(false);
        GetComponent<AudioSource>().Stop();
        leave = true;
        leavePos = transform.localPosition;
        leaveRot = transform.rotation;
    }
}
