using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartController_FG : MonoBehaviour
{
    private void Update()
    {
        Physics.Raycast(transform.position + Vector3.up * 2f, Vector3.down, out RaycastHit hit, 3f, LayerMask.GetMask("Player"));
        if (hit.collider != null && !hit.transform.GetChild(0).gameObject.activeSelf)
        {
            hit.transform.GetChild(0).gameObject.SetActive(true);
            Destroy(gameObject);
        }
    }
}
