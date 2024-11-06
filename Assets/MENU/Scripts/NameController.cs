using System.Collections;
using UnityEngine;
using TMPro;

public class NameController : MonoBehaviour
{
    public NameInputManager manager;
    public RectTransform[] chars = new RectTransform[4];
    public AudioClip moveSound;
    char[] characters = new char[36];
    int index = 0;
    Coroutine instance;
    int selectedChar = 0;
    public bool finished = false;
    bool isMoving = false;

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
        if (!isMoving)
        {
            RectTransform childRect = transform.GetChild(0).GetComponent<RectTransform>();

            if (gameObject.name == "Player1")
            {
                if (Input.GetButtonDown("A") && manager.instance == null)
                {
                    finished = !finished;
                    Color targetColor = finished ? Color.green : Color.white;
                    foreach (RectTransform caracter in chars)
                    {
                        caracter.GetComponent<TextMeshProUGUI>().color = targetColor;
                    }
                }

                if (!finished)
                {
                    transform.GetChild(0).gameObject.SetActive(true);

                    if (Input.GetKeyDown(KeyCode.D))
                    {
                        index = (index + 1) % chars.Length;
                        childRect.anchoredPosition = chars[index].anchoredPosition;
                        selectedChar = System.Array.IndexOf(characters, chars[index].GetComponent<TextMeshProUGUI>().text[0]);
                        StartCoroutine(ResetMovement());
                    }
                    else if (Input.GetKeyDown(KeyCode.A))
                    {
                        index = (index - 1 + chars.Length) % chars.Length;
                        childRect.anchoredPosition = chars[index].anchoredPosition;
                        selectedChar = System.Array.IndexOf(characters, chars[index].GetComponent<TextMeshProUGUI>().text[0]);
                        StartCoroutine(ResetMovement());
                    }
                    else if (Input.GetKeyDown(KeyCode.W))
                    {
                        selectedChar = (selectedChar - 1 + characters.Length) % characters.Length;
                        chars[index].GetComponent<TextMeshProUGUI>().text = characters[selectedChar].ToString();
                        if (instance == null)
                        {
                            instance = StartCoroutine(Move(0));
                        }
                        StartCoroutine(ResetMovement());
                    }
                    else if (Input.GetKeyDown(KeyCode.S))
                    {
                        selectedChar = (selectedChar + 1) % characters.Length;
                        chars[index].GetComponent<TextMeshProUGUI>().text = characters[selectedChar].ToString();
                        if (instance == null)
                        {
                            instance = StartCoroutine(Move(1));
                        }
                        StartCoroutine(ResetMovement());
                    }

                    if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S))
                    {
                        if ((manager.GetComponent<AudioSource>().time > 0.1f && manager.GetComponent<AudioSource>().isPlaying) || !manager.GetComponent<AudioSource>().isPlaying)
                        {
                            manager.GetComponent<AudioSource>().clip = moveSound;
                            manager.GetComponent<AudioSource>().Play();
                        }
                    }
                }
                else
                {
                    transform.GetChild(0).gameObject.SetActive(false);
                }
            }
            else if (gameObject.name == "Player2")
            {
                if (Input.GetButtonDown("A2") && manager.instance == null)
                {
                    finished = !finished;
                    Color targetColor = finished ? Color.green : Color.white;
                    foreach (RectTransform caracter in chars)
                    {
                        caracter.GetComponent<TextMeshProUGUI>().color = targetColor;
                    }
                }

                if (!finished)
                {
                    transform.GetChild(0).gameObject.SetActive(true);

                    if (Input.GetKeyDown(KeyCode.RightArrow))
                    {
                        index = (index + 1) % chars.Length;
                        childRect.anchoredPosition = chars[index].anchoredPosition;
                        selectedChar = System.Array.IndexOf(characters, chars[index].GetComponent<TextMeshProUGUI>().text[0]);
                        StartCoroutine(ResetMovement());
                    }
                    else if (Input.GetKeyDown(KeyCode.LeftArrow))
                    {
                        index = (index - 1 + chars.Length) % chars.Length;
                        childRect.anchoredPosition = chars[index].anchoredPosition;
                        selectedChar = System.Array.IndexOf(characters, chars[index].GetComponent<TextMeshProUGUI>().text[0]);
                        StartCoroutine(ResetMovement());
                    }
                    else if (Input.GetKeyDown(KeyCode.UpArrow))
                    {
                        selectedChar = (selectedChar - 1 + characters.Length) % characters.Length;
                        chars[index].GetComponent<TextMeshProUGUI>().text = characters[selectedChar].ToString();
                        if (instance == null)
                        {
                            instance = StartCoroutine(Move(0));
                        }
                        StartCoroutine(ResetMovement());
                    }
                    else if (Input.GetKeyDown(KeyCode.DownArrow))
                    {
                        selectedChar = (selectedChar + 1) % characters.Length;
                        chars[index].GetComponent<TextMeshProUGUI>().text = characters[selectedChar].ToString();
                        if (instance == null)
                        {
                            instance = StartCoroutine(Move(1));
                        }
                        StartCoroutine(ResetMovement());
                    }

                    if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow))
                    {
                        if ((manager.GetComponent<AudioSource>().time > 0.1f && manager.GetComponent<AudioSource>().isPlaying) || !manager.GetComponent<AudioSource>().isPlaying)
                        {
                            manager.GetComponent<AudioSource>().clip = moveSound;
                            manager.GetComponent<AudioSource>().Play();
                        }
                    }
                }
                else
                {
                    transform.GetChild(0).gameObject.SetActive(false);
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

    IEnumerator ResetMovement()
    {
        isMoving = true;
        yield return new WaitForSeconds(0.1f);
        isMoving = false;
    }
}
