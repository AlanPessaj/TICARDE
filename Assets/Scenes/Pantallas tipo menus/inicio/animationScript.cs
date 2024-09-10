using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationScript : MonoBehaviour
{
    public Sprite[] sprites;
    public int index;
    public GameObject canvas;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (index == -1)
        {
            canvas.SetActive(true);
            Destroy(gameObject);
        }
        GetComponent<SpriteRenderer>().sprite = sprites[index];
    }
}
