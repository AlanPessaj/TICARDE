using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitDetector_FF : MonoBehaviour
{
    public HitManager_FF hitManager;
    public int colNumber;
    public PlayerController_FF playerController;
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Hit"))
        {
            hitManager.hColliders[colNumber] = GetComponent<Collider>();
            hitManager.damageProperties = other.GetComponent<Damage_FF>();
        }
    }
}
