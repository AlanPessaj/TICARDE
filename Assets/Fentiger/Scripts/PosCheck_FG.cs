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
        if (other.gameObject.layer == LayerMask.NameToLayer("Tree") || other.gameObject.layer == LayerMask.NameToLayer("Transport"))
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
        if (other.gameObject.layer == LayerMask.NameToLayer("Tree") || other.gameObject.layer == LayerMask.NameToLayer("Transport"))
        {
            inTreeOrTransport = false;
            UpdateCondition();
        }
        if (other.gameObject.layer == LayerMask.NameToLayer("Field"))
        {
            inField = false;
            UpdateCondition();
        }
    }

    void UpdateCondition()
    {
        bool isInValidZRange = transform.position.z >= -11 && transform.position.z <= 11;

        controller.takenSpot = inTreeOrTransport && !inField && isInValidZRange;
    }
}

