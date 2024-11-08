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
        if (other.gameObject.layer == LayerMask.NameToLayer("Hit") && colNumber != -1)
        {
            hitManager.hColliders[colNumber] = GetComponent<Collider>();
            hitManager.damageProperties = other.GetComponent<Damage_FF>();
        }
        if (other.gameObject.layer == LayerMask.NameToLayer("Floor") && colNumber == -1)
        {
            Debug.Log("auch");
            if (playerController.InState("hit_smash")) hitManager.TakeFallDamage(true);
            else if (playerController.InState("hit_slideKick")) hitManager.TakeFallDamage(false);
        }
    }
}
