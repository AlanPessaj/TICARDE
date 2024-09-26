using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController_FG : MonoBehaviour
{
    public Transform player1;
    public Transform player2;
    public Generator_FG generator;
    public float snapThreshold;
    public float stepSize;
    Vector3 targetPosition;
    Vector3 previousTarget;
    Vector3 previusPosition;
    float step;

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(previousTarget, transform.position) <= snapThreshold && Vector3.Distance(previousTarget, transform.position) != 0)
        {
            transform.position = previousTarget;
        }
        if (!((player1.position.x + player2.position.x) / 2 < (generator.distance - generator.despawnRadius) + 7.6f || player1.position.x == 0))
        {
            targetPosition = new Vector3((player1.position.x + player2.position.x) / 2 - 5.4f, transform.position.y, transform.position.z);
            previusPosition = transform.position;
            step = 0;
            step += stepSize * Time.deltaTime;
            MoveCamera(step);
        }
    }

    void MoveCamera(float step)
    {
        if (targetPosition != previousTarget)
        {
            previousTarget = targetPosition;
            this.step = 0;
            step = 0;
            previusPosition = transform.position;
        }
        transform.position = new Vector3(Mathf.Lerp(previusPosition.x, previousTarget.x, step), Mathf.Lerp(previusPosition.y, previousTarget.y, step), Mathf.Lerp(previusPosition.z, previousTarget.z, step));
    }
}
