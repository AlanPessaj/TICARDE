using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Deactivator_FG : MonoBehaviour
{
    private void Update()
    {
        if (GameObject.Find("Player1") == null && GameObject.Find("Player2") == null)
        {
            MonoBehaviour[] allScripts = FindObjectsOfType<MonoBehaviour>();
            NavMeshAgent[] allAgents = FindObjectsOfType<NavMeshAgent>();
            
            foreach (MonoBehaviour script in allScripts)
            {
                if (script != this && !(script is DieScript_FG) && !(script is Generator_FG) && !(script is SoundManager_FG)&& !(script is LedsController) && !(script is GAMEMANAGER) && !(script is conexion))
                {
                    script.enabled = false;
                }
            }

            foreach (NavMeshAgent agent in allAgents)
            {
                agent.enabled = false;
            }
        }
    }
}
