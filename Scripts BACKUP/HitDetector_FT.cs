﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitDetector_FT : MonoBehaviour
{
    public HitManager_FT hitManager;
    public int colNumber;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Hit"))
        {
            hitManager.hColliders[colNumber] = GetComponent<Collider>();
        }
    }
}
