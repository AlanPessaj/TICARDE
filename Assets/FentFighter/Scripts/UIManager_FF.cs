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
    bool calledCorutine = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            UI.transform.GetChild(0).localPosition = new Vector3(Mathf.Lerp(385, 0, Mathf.InverseLerp(0, maxHealth, health)), 0, 0);
            UI.transform.GetChild(1).localPosition = new Vector3(Mathf.Lerp(385, 0, Mathf.InverseLerp(0, maxXP, XP)), -38.4f, 0);
        }
        if (health <= 0 && !calledCorutine)
        {
            calledCorutine = true;
            SceneManager.LoadScene("END(FF)", LoadSceneMode.Additive);
            StartCoroutine(SetScore());
        }
    }

    IEnumerator SetScore()
    {
        yield return null;
        Scene end = SceneManager.GetSceneByName("END(FF)");
        if (GetComponent<PlayerController_FF>().isPlayer1)
        {
            end.GetRootGameObjects()[0].GetComponent<Points_FF>().UpdateScore(100 - GetComponent<PlayerController_FF>().otherPlayer.GetComponent<UIManager_FF>().health, 100 - health);
        }
        else
        {
            end.GetRootGameObjects()[0].GetComponent<Points_FF>().UpdateScore(100 - health, 100 - GetComponent<PlayerController_FF>().otherPlayer.GetComponent<UIManager_FF>().health);
        }
        SceneManager.UnloadSceneAsync(gameObject.scene);
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
            UI.transform.GetChild(1).localPosition = new Vector3(Mathf.Lerp(385, 0, Mathf.InverseLerp(0, maxXP, XP)), -38.4f, 0);
            return true;
        }
        return false;
    }
}
