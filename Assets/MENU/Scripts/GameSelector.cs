using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSelector : MonoBehaviour
{
    public GameObject[] logos;
    int index;
    public bool animationFinished = false;
    
    // Update is called once per frame
    void Update()
    {
        if (animationFinished)
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
                        GameData.game = "FF";
                        break;
                    case 1:
                        GameData.game = "FT";
                        break;
                    case 2:
                        GameData.game = "FG";
                        break;
                }
                SceneManager.LoadScene("NameInput");
            }
        }
    }
}
