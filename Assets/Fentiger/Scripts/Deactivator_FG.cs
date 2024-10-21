using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deactivator_FG : MonoBehaviour
{
    private void Update()
    {
        if (GameObject.Find("Player1") == null && GameObject.Find("Player2") == null)
        {
            MonoBehaviour[] allScripts = FindObjectsOfType<MonoBehaviour>();
            foreach (MonoBehaviour script in allScripts)
            {
                if (script != this && !(script is DieScript_FG) && !(script is Generator_FG))
                {
                    script.enabled = false;
                }
            }
        }
    }
}
