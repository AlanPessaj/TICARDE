using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NameController : MonoBehaviour
{
    public RectTransform[] chars = new RectTransform[4];
    char[] characters = new char[36];
    int index = 0;
    Coroutine instance;
    int selectedChar = 0;

    private void Start()
    {
        for (int i = 0; i < 26; i++)
        {
            characters[i] = (char)('A' + i);
        }

        for (int i = 0; i < 10; i++)
        {
            characters[26 + i] = (char)('0' + i);
        }
        chars[index].GetComponent<TextMeshProUGUI>().text = characters[selectedChar].ToString();
    }

    void Update()
    {
        RectTransform childRect = transform.GetChild(0).GetComponent<RectTransform>();

        if (gameObject.name == "Player1")
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                index++;
                if (index >= chars.Length) index = 0;
                childRect.anchoredPosition = chars[index].anchoredPosition;
                selectedChar = System.Array.IndexOf(characters, chars[index].GetComponent<TextMeshProUGUI>().text[0]);
            }

            if (Input.GetKeyDown(KeyCode.A))
            {
                index--;
                if (index < 0) index = chars.Length - 1;
                childRect.anchoredPosition = chars[index].anchoredPosition;
                selectedChar = System.Array.IndexOf(characters, chars[index].GetComponent<TextMeshProUGUI>().text[0]);
            }

            if (Input.GetKeyDown(KeyCode.W))
            {
                selectedChar = (selectedChar - 1 + characters.Length) % characters.Length;
                chars[index].GetComponent<TextMeshProUGUI>().text = characters[selectedChar].ToString();
                if (instance == null)
                {
                    instance = StartCoroutine(Move(0));
                }
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                selectedChar = (selectedChar + 1) % characters.Length;
                chars[index].GetComponent<TextMeshProUGUI>().text = characters[selectedChar].ToString();
                if (instance == null)
                {
                    instance = StartCoroutine(Move(1));
                }
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                index++;
                if (index >= chars.Length) index = 0;
                childRect.anchoredPosition = chars[index].anchoredPosition;
                selectedChar = System.Array.IndexOf(characters, chars[index].GetComponent<TextMeshProUGUI>().text[0]);
            }

            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                index--;
                if (index < 0) index = chars.Length - 1;
                childRect.anchoredPosition = chars[index].anchoredPosition;
                selectedChar = System.Array.IndexOf(characters, chars[index].GetComponent<TextMeshProUGUI>().text[0]);
            }

            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                selectedChar = (selectedChar - 1 + characters.Length) % characters.Length;
                chars[index].GetComponent<TextMeshProUGUI>().text = characters[selectedChar].ToString();
                if (instance == null)
                {
                    instance = StartCoroutine(Move(0));
                }
            }

            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                selectedChar = (selectedChar + 1) % characters.Length;
                chars[index].GetComponent<TextMeshProUGUI>().text = characters[selectedChar].ToString();
                if (instance == null)
                {
                    instance = StartCoroutine(Move(1));
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
