using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NameContoller : MonoBehaviour
{
    public RectTransform[] chars = new RectTransform[4];
    int index = 0;
    Coroutine instance;

    void Update()
    {
        if (gameObject.name == "Player1")
        {
            RectTransform childRect = transform.GetChild(0).GetComponent<RectTransform>();

            if (Input.GetKeyDown(KeyCode.D))
            {
                for (int i = 0; i < chars.Length; i++)
                {
                    if (Mathf.Approximately(childRect.anchoredPosition.x, chars[i].anchoredPosition.x))
                    {
                        index = i + 1;
                        break;
                    }
                }
                if (index < chars.Length)
                    childRect.anchoredPosition = chars[index].anchoredPosition;
            }

            if (Input.GetKeyDown(KeyCode.A))
            {
                for (int i = 0; i < chars.Length; i++)
                {
                    if (Mathf.Approximately(childRect.anchoredPosition.x, chars[i].anchoredPosition.x))
                    {
                        index = i - 1;
                        break;
                    }
                }
                if (index >= 0)
                    childRect.anchoredPosition = chars[index].anchoredPosition;
            }

            if (Input.GetKey(KeyCode.W))
            {
                if (instance == null)
                {
                    StartCoroutine(Move(0));
                }
            }

            if (Input.GetKey(KeyCode.S))
            {
                if (instance == null)
                {
                    StartCoroutine(Move(1));
                }
            }
        }
    }

    IEnumerator Move(int way)
    {
        transform.GetChild(0).GetChild(way).gameObject.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        transform.GetChild(0).GetChild(way).gameObject.SetActive(false);
        instance = null;
    }
}
