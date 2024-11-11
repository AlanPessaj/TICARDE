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
    public bool noDamage;
    public int score;
    bool died = false;

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.H))
        {
            UI.transform.GetChild(0).localPosition = new Vector3(Mathf.Lerp(385, 0, Mathf.InverseLerp(0, maxHealth, health)), 0, 0);
            UI.transform.GetChild(1).localPosition = new Vector3(Mathf.Lerp(385, 0, Mathf.InverseLerp(0, maxXP, XP)), -38.4f, 0);
        }*/
        if (health <= 0 && !died)
        {
            GetComponent<PlayerController_FF>().otherPlayer.GetComponent<UIManager_FF>().noDamage = true;
            GetComponent<Animator>().SetTrigger("die");
            died = true;
        }
    }

    public void LoadEnd()
    {
        GetComponent<Animator>().enabled = false;
        Time.timeScale = 1;
        GetComponent<PlayerController_FF>().otherPlayer.GetComponent<UIManager_FF>().score = Mathf.RoundToInt(GetComponent<PlayerController_FF>().otherPlayer.GetComponent<UIManager_FF>().score * 1.5f);
        GameData.score1 = GetComponent<PlayerController_FF>().isPlayer1 ? GameData.score1 = GetComponent<PlayerController_FF>().otherPlayer.GetComponent<UIManager_FF>().score : score;
        GameData.score2 = !GetComponent<PlayerController_FF>().isPlayer1 ? GameData.score1 = GetComponent<PlayerController_FF>().otherPlayer.GetComponent<UIManager_FF>().score : score;
        GameData.p1Winner = !GetComponent<PlayerController_FF>().isPlayer1;
        SceneManager.LoadScene("END(FF)");
    }

    public void ChangeHealth(float value)
    {
        if (value > 0 || !noDamage)
        {
            health = Mathf.Clamp(health + value, 0, maxHealth);
            UI.transform.GetChild(0).localPosition = new Vector3(Mathf.Lerp(385, 0, Mathf.InverseLerp(0, maxHealth, health)), 0, 0);
        }
    }

    public void AddXP(float value)
    {
        XP = Mathf.Clamp(XP + value, 0, maxXP);
        if (value > 0) score += (int)value;
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
