using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NameContoller : MonoBehaviour
{
    public RectTransform[] chars = new RectTransform[4];
    char[] characters = new char[36];
    int index = 0;
    Coroutine instance;
    int selectedChar = 0;
    int selected = 0;

    private void Start()
    {
        for (int i = 0; i < 26; i++)
        {
            characters[i] = (char)('a' + i);
        }

        for (int i = 0; i < 10; i++)
        {
            characters[26 + i] = (char)('0' + i);
        }
    }

    void Update()
    {
        selectedChar = System.Array.IndexOf(chars, chars[selected].GetComponent<TextMeshPro>().text);
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
                        selected = i + 1;
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
                        selected = i - 1;
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
                    instance = StartCoroutine(Move(0));
                }
            }

            if (Input.GetKey(KeyCode.S))
            {
                if (instance == null)
                {
                    instance = StartCoroutine(Move(1));
                }
            }
        }
    }

    IEnumerator Move(int way)
    {
        if (way == 0) selectedChar++;
        else selectedChar--;
        if (selectedChar < 0) selectedChar = 35;
        if (selectedChar > 35) selectedChar = 0;
        yield return new WaitForSeconds(0.1f);
        chars[selected].GetComponent<TextMeshPro>().text = characters[selectedChar].ToString();
        transform.GetChild(0).GetChild(way).gameObject.SetActive(true);
        transform.GetChild(0).GetChild(way).gameObject.SetActive(false);
        instance = null;
    }
}
