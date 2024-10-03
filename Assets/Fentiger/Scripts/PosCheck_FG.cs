using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PosCheck_FG : MonoBehaviour
{
    public FrogController_FG controller;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != LayerMask.NameToLayer("Field") && (other.gameObject.layer == LayerMask.NameToLayer("Tree") || other.gameObject.layer == LayerMask.NameToLayer("Transport")))
        {
            controller.takenSpot = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Field") || other.gameObject.layer == LayerMask.NameToLayer("Tree") || other.gameObject.layer == LayerMask.NameToLayer("Transport"))
        {
            controller.takenSpot = false;
        }
    }
}
