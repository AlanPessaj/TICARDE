using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitDetector_FF : MonoBehaviour
{
    public HitManager_FF hitManager;
    public int colNumber;
    public PlayerController_FF playerController;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Hit") && other.gameObject != playerController.fist && other.gameObject != playerController.foot)
        {
            hitManager.hColliders[colNumber] = GetComponent<Collider>();
            hitManager.damageProperties = other.GetComponent<Properties_FF>();
        }
    }
}
