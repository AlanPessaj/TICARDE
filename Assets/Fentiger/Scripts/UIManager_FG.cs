using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager_FG : MonoBehaviour
{
    public GameObject UI;
    public float maxXP;
    public float XP;

    public void AddXP(float value)
    {
        XP = Mathf.Clamp(XP + value, 0, maxXP);
        UI.transform.GetChild(1).localPosition = new Vector3(Mathf.Lerp(385, 0, Mathf.InverseLerp(0, maxXP, XP)), -38.4f, 0);
    }
    public bool RemoveXP(float value)
    {
        if (value <= XP)
        {
            XP = Mathf.Clamp(XP - value, 0, maxXP);
            UI.transform.GetChild(1).localPosition = new Vector3(Mathf.Lerp(385, 0, Mathf.InverseLerp(0, maxXP, XP)), -38.4f, 0);
            return true;
        }
        return false;
    }
}
