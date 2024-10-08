using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PosCheck_FG : MonoBehaviour
{
    public FrogController_FG controller;
    private bool inTreeOrTransport = false;
    private bool inField = false;
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Tree") || (other.gameObject.layer == LayerMask.NameToLayer("Transport") && other.gameObject != controller.gameObject))
        {
            inTreeOrTransport = true;
            UpdateCondition();
        }
        if (other.gameObject.layer == LayerMask.NameToLayer("Field"))
        {
            inField = true;
            UpdateCondition();
        }
    }

    void OnTriggerExit(Collider other)
    {
        controller.takenSpot = null;
    }

    void UpdateCondition()
    {
        bool isInValidZRange = transform.position.z > -12.1f && transform.position.z < 12.1f;
        controller.takenSpot = inTreeOrTransport || !inField || !isInValidZRange;
        if (controller.takenSpot == true)
        {
            Debug.Log($"updated condition, tree: {inTreeOrTransport}, field: {inField}, range: {isInValidZRange}, POS: {transform.position}");
        }
    }
}

