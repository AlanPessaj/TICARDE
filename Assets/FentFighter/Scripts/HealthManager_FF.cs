using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthManager_FF : MonoBehaviour
{
    public GameObject UI;
    public float health;
    public float maxHealth;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            UpdateHealth();
        }
        if (health <= 0)
        {
            SceneManager.LoadScene(gameObject.scene.name);
        }
    }

    public void UpdateHealth()
    {
        UI.transform.GetChild(0).localPosition = new Vector3(Mathf.Lerp(385, 0, Mathf.InverseLerp(0, maxHealth, health)), 0, 0);
    }
}
