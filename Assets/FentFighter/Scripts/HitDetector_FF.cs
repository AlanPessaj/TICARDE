using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitDetector_FF : MonoBehaviour
{
    public HitManager_FF hitManager;
    public int colNumber;
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Fist_FF"))
        {
            hitManager.hColliders[colNumber] = GetComponent<Collider>();
        }
    }
}
