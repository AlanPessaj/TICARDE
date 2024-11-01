using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationScript : MonoBehaviour
{
    public Sprite[] sprites;
    public int index;
    public GameObject canvas;
    public GameSelector selector;

    // Update is called once per frame
    void Update()
    {
        if (index < 0)
        {
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
