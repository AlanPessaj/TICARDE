using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager_FF : MonoBehaviour
{
    public GameObject UI;
    public float health;
    public float maxHealth;
    public float maxXP;
    public float XP; 
    // Start is called before the first frame update
    void Start()
    {
        UI.transform.GetChild(1).localPosition = new Vector3(Mathf.Lerp(385, 0, Mathf.InverseLerp(0, maxXP, XP)), -38.4f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            UI.transform.GetChild(0).localPosition = new Vector3(Mathf.Lerp(385, 0, Mathf.InverseLerp(0, maxHealth, health)), 0, 0);
            UI.transform.GetChild(1).localPosition = new Vector3(Mathf.Lerp(385, 0, Mathf.InverseLerp(0, maxXP, XP)), -38.4f, 0);
        }
        if (health <= 0)
        {
            SceneManager.LoadScene(gameObject.scene.name);
        }
    }

    public void ChangeHealth(float value)
    {
        health = Mathf.Clamp(health + value, 0, maxHealth);
        UI.transform.GetChild(0).localPosition = new Vector3(Mathf.Lerp(385, 0, Mathf.InverseLerp(0, maxHealth, health)), 0, 0);
    }

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
        }
        UI.transform.GetChild(1).localPosition = new Vector3(Mathf.Lerp(385, 0, Mathf.InverseLerp(0, maxXP, XP)), -38.4f, 0);
        return value <= XP;
    }
}
