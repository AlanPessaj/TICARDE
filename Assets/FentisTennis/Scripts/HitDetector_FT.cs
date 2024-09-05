using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitDetector_FT : MonoBehaviour
{
    public HitManager_FT hitManager;
    public int colNumber;
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Hit"))
        {
            if (colNumber != 4)
            {
                hitManager.hColliders[colNumber] = GetComponent<Collider>();
            }
            else
            {
                hitManager.hColliders[0] = GetComponent<Collider>();
                hitManager.hColliders[1] = GetComponent<Collider>();
                hitManager.hColliders[2] = GetComponent<Collider>();
            }
        }
    }
}
