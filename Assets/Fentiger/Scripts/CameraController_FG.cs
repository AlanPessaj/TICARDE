﻿using System.Collections;
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
    Vector3 previousPosition;
    float step;

    void Update()
    {
        if (generator.multiplayer)
        {
            if (Vector3.Distance(previousTarget, transform.position) <= snapThreshold && Vector3.Distance(previousTarget, transform.position) != 0)
            {
                transform.position = previousTarget;
            }
            if (!((player1.position.x + player2.position.x) / 2 < (generator.distance - generator.despawnRadius) + 7.6f || player1.position.x == 0))
            {
                targetPosition = new Vector3((player1.position.x + player2.position.x) / 2 - 5.4f, transform.position.y, transform.position.z);
                previousPosition = transform.position;
                step = 0;
                step += stepSize * Time.deltaTime;
                MoveCamera(step);
            }
        }
        else
        {
            if (generator.player1Alive)
            {
                FollowPlayer(player1);
            }
            else if (!generator.player1Alive)
            {
                FollowPlayer(player2);
            }
        }
    }

    void FollowPlayer(Transform player)
    {
        if (Vector3.Distance(player.position, transform.position) <= snapThreshold && Vector3.Distance(player.position, transform.position) != 0)
        {
            transform.position = player.position;
        }
        else
        {
            targetPosition = new Vector3(player.position.x - 5.4f, transform.position.y, transform.position.z);
            if (targetPosition.x < (generator.distance - generator.despawnRadius) + 2.6f)
            {
                targetPosition.x = (generator.distance - generator.despawnRadius) + 2.6f;
            }

            previousPosition = transform.position;
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
            previousPosition = transform.position;
        }
        transform.position = new Vector3(Mathf.Lerp(previousPosition.x, previousTarget.x, step), Mathf.Lerp(previousPosition.y, previousTarget.y, step), Mathf.Lerp(previousPosition.z, previousTarget.z, step));
    }
}
