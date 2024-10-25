using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserScaler_FG : MonoBehaviour
{
    float toDivide;

    void Update()
    {
        Debug.DrawRay(transform.GetChild(0).position, transform.up * 48f, Color.red);
        Physics.Raycast(transform.GetChild(0).position, transform.up, out RaycastHit hit, 48f, LayerMask.GetMask("Tree"));
        if (hit.collider != null)
        {
            Debug.Log(hit.transform.position.ToString());
            toDivide = 24 / hit.distance * 2;
            transform.position = transform.GetChild(0).position + transform.up * (hit.distance / 2f);
            transform.localScale = new Vector3(0.2f, 24 / toDivide, 0.2f);
        }
        else
        {
            transform.localPosition = new Vector3(-14, -18, -5);
            transform.localScale = new Vector3(0.2f, 24, 0.2f);
        }
    }
}