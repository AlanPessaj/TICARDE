using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectorScripts : MonoBehaviour
{
    public GameObject[] logos;
    int index;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (index > 0)
            {
                index--;
            }
            else
            {
                index = logos.Length - 1;
            }
            foreach (var item in logos)
            {
                item.SetActive(false);
            }
            logos[index].SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (index < logos.Length - 1)
            {
                index++;
            }
            else
            {
                index = 0;
            }
            foreach (var item in logos)
            {
                item.SetActive(false);
            }
            logos[index].SetActive(true);
        }
        if (Input.GetButtonDown("A"))
        {
            switch (index)
            {
                case 0:
                    SceneManager.LoadScene("Game(FF)");
                break;
                case 1:
                    SceneManager.LoadScene("Game(FT)");
                break;
                case 2:
                    SceneManager.LoadScene("Game(FG)");
                break;
            }
        }
    }
}
