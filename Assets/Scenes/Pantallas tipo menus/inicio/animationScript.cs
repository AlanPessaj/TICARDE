using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationScript : MonoBehaviour
{
    public Sprite[] sprites;
    public int index;
    public GameObject canvas;
    public GameSelector selector;
    public GAMEMANAGER ticardemanager;

    void Start()
    {
        ticardemanager = GameObject.Find("TICARDEMANAGER").GetComponent<GAMEMANAGER>();
    }
    void Update()
    {
        if (index < 0)
        {
            ticardemanager.menuActive = true;
            canvas.SetActive(true);
            selector.animationFinished = true;
            Destroy(gameObject);
        }
        else
        {
            GetComponent<SpriteRenderer>().sprite = sprites[index];
        }
    }
}
