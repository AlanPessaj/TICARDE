using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterSelector : MonoBehaviour
{
    public GameObject[] pSquares;
    GameObject[,] squares;
    public GameObject[] pMPSquares1;
    GameObject[,] MPSquares1;
    public GameObject[] pMPSquares2;
    GameObject[,] MPSquares2;
    public bool multiplayer;
    bool[] confirmed = new bool[2];
    int vIndex;
    int hIndex;
    int vIndex2;
    int hIndex2;
    float confirmedTimer;
    float confirmedTimer2;
    public float blinkingSpeed;
    public string game;
    // Start is called before the first frame update
    void Start()
    {
        squares = new GameObject[pSquares.Length / 2, pSquares.Length / 2];
        int p = 0;
        for (int i = 0; i < pSquares.Length / 2; i++)
        {
            for (int o = 0; o < pSquares.Length / 2; o++)
            {
                squares[i, o] = pSquares[p];
                p++;
            }
        }
        MPSquares1 = new GameObject[pMPSquares1.Length / 2, pMPSquares1.Length / 2];
        p = 0;
        for (int i = 0; i < pMPSquares1.Length / 2; i++)
        {
            for (int o = 0; o < pMPSquares1.Length / 2; o++)
            {
                MPSquares1[i, o] = pMPSquares1[p];
                p++;
            }
        }
        MPSquares2 = new GameObject[pMPSquares2.Length / 2, pMPSquares2.Length / 2];
        p = 0;
        for (int i = 0; i < pMPSquares2.Length / 2; i++)
        {
            for (int o = 0; o < pMPSquares2.Length / 2; o++)
            {
                MPSquares2[i, o] = pMPSquares2[p];
                p++;
            }
        }
        if (!multiplayer)
        {
            MPSquares1[0, 0].SetActive(false);
            MPSquares2[0, 0].SetActive(false);
            squares[0, 0].SetActive(true);
            squares[0, 0].GetComponent<Image>().color = new Color(1, 1, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!confirmed[0])
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                if (hIndex > 0)
                {
                    hIndex--;
                }
                foreach (var item in squares)
                {
                    if (item.GetComponent<Image>().color == new Color(1, 1, 0))
                    {
                        item.SetActive(false);
                    }
                }
                for (int i = 0; i < pMPSquares1.Length / 2; i++)
                {
                    for (int o = 0; o < pMPSquares1.Length / 2; o++)
                    {
                        if (MPSquares1[i, o].activeInHierarchy)
                        {
                            MPSquares1[i, o].SetActive(false);
                            MPSquares2[i, o].SetActive(false);
                            squares[i, o].SetActive(true);
                            squares[i, o].GetComponent<Image>().color = Color.blue;
                            goto breakLoop;
                        }
                    }
                }
                breakLoop:
                if (!(squares[hIndex, vIndex].activeSelf && squares[hIndex, vIndex].GetComponent<Image>().color != new Color(1, 1, 0)))
                {
                    squares[hIndex, vIndex].SetActive(true);
                    squares[hIndex, vIndex].GetComponent<Image>().color = new Color(1, 1, 0);
                }
                else
                {
                    foreach (var item in squares)
                    {
                        item.SetActive(false);
                    }
                    MPSquares1[hIndex, vIndex].SetActive(true);
                    MPSquares2[hIndex, vIndex].SetActive(true);
                }
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                if (hIndex < (pSquares.Length / 2) - 1)
                {
                    hIndex++;
                }
                foreach (var item in squares)
                {
                    if (item.GetComponent<Image>().color == new Color(1, 1, 0))
                    {
                        item.SetActive(false);
                    }
                }
                for (int i = 0; i < pMPSquares1.Length / 2; i++)
                {
                    for (int o = 0; o < pMPSquares1.Length / 2; o++)
                    {
                        if (MPSquares1[i, o].activeInHierarchy)
                        {
                            MPSquares1[i, o].SetActive(false);
                            MPSquares2[i, o].SetActive(false);
                            squares[i, o].SetActive(true);
                            squares[i, o].GetComponent<Image>().color = Color.blue;
                            goto breakLoop;
                        }
                    }
                }
                breakLoop:
                if (!(squares[hIndex, vIndex].activeSelf && squares[hIndex, vIndex].GetComponent<Image>().color != new Color(1, 1, 0)))
                {
                    squares[hIndex, vIndex].SetActive(true);
                    squares[hIndex, vIndex].GetComponent<Image>().color = new Color(1, 1, 0);
                }
                else
                {
                    foreach (var item in squares)
                    {
                        item.SetActive(false);
                    }
                    MPSquares1[hIndex, vIndex].SetActive(true);
                    MPSquares2[hIndex, vIndex].SetActive(true);
                }
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                if (vIndex > 0)
                {
                    vIndex--;
                }
                foreach (var item in squares)
                {
                    if (item.GetComponent<Image>().color == new Color(1, 1, 0))
                    {
                        item.SetActive(false);
                    }
                }
                for (int i = 0; i < pMPSquares1.Length / 2; i++)
                {
                    for (int o = 0; o < pMPSquares1.Length / 2; o++)
                    {
                        if (MPSquares1[i, o].activeInHierarchy)
                        {
                            MPSquares1[i, o].SetActive(false);
                            MPSquares2[i, o].SetActive(false);
                            squares[i, o].SetActive(true);
                            squares[i, o].GetComponent<Image>().color = Color.blue;
                            goto breakLoop;
                        }
                    }
                }
                breakLoop:
                if (!(squares[hIndex, vIndex].activeSelf && squares[hIndex, vIndex].GetComponent<Image>().color != new Color(1, 1, 0)))
                {
                    squares[hIndex, vIndex].SetActive(true);
                    squares[hIndex, vIndex].GetComponent<Image>().color = new Color(1, 1, 0);
                }
                else
                {
                    foreach (var item in squares)
                    {
                        item.SetActive(false);
                    }
                    MPSquares1[hIndex, vIndex].SetActive(true);
                    MPSquares2[hIndex, vIndex].SetActive(true);
                }
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                if (vIndex < (pSquares.Length / 2) - 1)
                {
                    vIndex++;
                }
                foreach (var item in squares)
                {
                    if (item.GetComponent<Image>().color == new Color(1, 1, 0))
                    {
                        item.SetActive(false);
                    }
                }
                for (int i = 0; i < pMPSquares1.Length / 2; i++)
                {
                    for (int o = 0; o < pMPSquares1.Length / 2; o++)
                    {
                        if (MPSquares1[i, o].activeInHierarchy)
                        {
                            MPSquares1[i, o].SetActive(false);
                            MPSquares2[i, o].SetActive(false);
                            squares[i, o].SetActive(true);
                            squares[i, o].GetComponent<Image>().color = Color.blue;
                            goto breakLoop;
                        }
                    }
                }
                breakLoop:
                if (!(squares[hIndex, vIndex].activeSelf && squares[hIndex, vIndex].GetComponent<Image>().color != new Color(1, 1, 0)))
                {
                    squares[hIndex, vIndex].SetActive(true);
                    squares[hIndex, vIndex].GetComponent<Image>().color = new Color(1, 1, 0);
                }
                else
                {
                    foreach (var item in squares)
                    {
                        item.SetActive(false);
                    }
                    MPSquares1[hIndex, vIndex].SetActive(true);
                    MPSquares2[hIndex, vIndex].SetActive(true);
                }
            }
        }
        if (Input.GetButtonDown("A"))
        {
            confirmed[0] = !confirmed[0];
            if (!confirmed[0])
            {
                if (hIndex != hIndex2 || vIndex != vIndex2)
                {
                    confirmedTimer = 0;
                    squares[hIndex, vIndex].GetComponent<Image>().color = new Color(1, 1, 0);
                }
                else
                {
                    MPSquares1[hIndex, vIndex].GetComponent<Image>().color = new Color(1, 1, 0);
                }
            }
        }
        if (confirmed[0])
        {
            if (hIndex != hIndex2 || vIndex != vIndex2)
            {
                squares[hIndex, vIndex].GetComponent<Image>().color = Color.Lerp(new Color(1, 1, 0), Color.white, Mathf.PingPong(confirmedTimer, 1));
                confirmedTimer += Time.deltaTime * blinkingSpeed;
            }
            else
            {
                MPSquares1[hIndex, vIndex].GetComponent<Image>().color = Color.Lerp(new Color(1, 1, 0), Color.white, Mathf.PingPong(confirmedTimer, 1));
                confirmedTimer += Time.deltaTime * blinkingSpeed;
            }
        }
        if (multiplayer)
        {
            if (!confirmed[1])
            {
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    if (hIndex2 > 0)
                    {
                        hIndex2--;
                    }
                    foreach (var item in squares)
                    {
                        if (item.GetComponent<Image>().color == Color.blue)
                        {
                            item.SetActive(false);
                        }
                    }
                    for (int i = 0; i < pMPSquares1.Length / 2; i++)
                    {
                        for (int o = 0; o < pMPSquares1.Length / 2; o++)
                        {
                            if (MPSquares1[i, o].activeInHierarchy)
                            {
                                MPSquares1[i, o].SetActive(false);
                                MPSquares2[i, o].SetActive(false);
                                squares[i, o].SetActive(true);
                                squares[i, o].GetComponent<Image>().color = new Color(1, 1, 0);
                                goto breakLoop;
                            }
                        }
                    }
                    breakLoop:
                    if (!(squares[hIndex2, vIndex2].activeSelf && squares[hIndex2, vIndex2].GetComponent<Image>().color != Color.blue))
                    {
                        squares[hIndex2, vIndex2].SetActive(true);
                        squares[hIndex2, vIndex2].GetComponent<Image>().color = Color.blue;
                    }
                    else
                    {
                        foreach (var item in squares)
                        {
                            item.SetActive(false);
                        }
                        MPSquares1[hIndex2, vIndex2].SetActive(true);
                        MPSquares2[hIndex2, vIndex2].SetActive(true);
                    }
                }
                if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    if (hIndex2 < (pSquares.Length / 2) - 1)
                    {
                        hIndex2++;
                    }
                    foreach (var item in squares)
                    {
                        if (item.GetComponent<Image>().color == Color.blue)
                        {
                            item.SetActive(false);
                        }
                    }
                    for (int i = 0; i < pMPSquares1.Length / 2; i++)
                    {
                        for (int o = 0; o < pMPSquares1.Length / 2; o++)
                        {
                            if (MPSquares1[i, o].activeInHierarchy)
                            {
                                MPSquares1[i, o].SetActive(false);
                                MPSquares2[i, o].SetActive(false);
                                squares[i, o].SetActive(true);
                                squares[i, o].GetComponent<Image>().color = new Color(1, 1, 0);
                                goto breakLoop;
                            }
                        }
                    }
                    breakLoop:
                    if (!(squares[hIndex2, vIndex2].activeSelf && squares[hIndex2, vIndex2].GetComponent<Image>().color != Color.blue))
                    {
                        squares[hIndex2, vIndex2].SetActive(true);
                        squares[hIndex2, vIndex2].GetComponent<Image>().color = Color.blue;
                    }
                    else
                    {
                        foreach (var item in squares)
                        {
                            item.SetActive(false);
                        }
                        MPSquares1[hIndex2, vIndex2].SetActive(true);
                        MPSquares2[hIndex2, vIndex2].SetActive(true);
                    }
                }
                if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    if (vIndex2 > 0)
                    {
                        vIndex2--;
                    }
                    foreach (var item in squares)
                    {
                        if (item.GetComponent<Image>().color == Color.blue)
                        {
                            item.SetActive(false);
                        }
                    }
                    for (int i = 0; i < pMPSquares1.Length / 2; i++)
                    {
                        for (int o = 0; o < pMPSquares1.Length / 2; o++)
                        {
                            if (MPSquares1[i, o].activeInHierarchy)
                            {
                                MPSquares1[i, o].SetActive(false);
                                MPSquares2[i, o].SetActive(false);
                                squares[i, o].SetActive(true);
                                squares[i, o].GetComponent<Image>().color = new Color(1, 1, 0);
                                goto breakLoop;
                            }
                        }
                    }
                    breakLoop:
                    if (!(squares[hIndex2, vIndex2].activeSelf && squares[hIndex2, vIndex2].GetComponent<Image>().color != Color.blue))
                    {
                        squares[hIndex2, vIndex2].SetActive(true);
                        squares[hIndex2, vIndex2].GetComponent<Image>().color = Color.blue;
                    }
                    else
                    {
                        foreach (var item in squares)
                        {
                            item.SetActive(false);
                        }
                        MPSquares1[hIndex2, vIndex2].SetActive(true);
                        MPSquares2[hIndex2, vIndex2].SetActive(true);
                    }
                }
                if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    if (vIndex2 < (pSquares.Length / 2) - 1)
                    {
                        vIndex2++;
                    }
                    foreach (var item in squares)
                    {
                        if (item.GetComponent<Image>().color == Color.blue)
                        {
                            item.SetActive(false);
                        }
                    }
                    for (int i = 0; i < pMPSquares1.Length / 2; i++)
                    {
                        for (int o = 0; o < pMPSquares1.Length / 2; o++)
                        {
                            if (MPSquares1[i, o].activeInHierarchy)
                            {
                                MPSquares1[i, o].SetActive(false);
                                MPSquares2[i, o].SetActive(false);
                                squares[i, o].SetActive(true);
                                squares[i, o].GetComponent<Image>().color = new Color(1, 1, 0);
                                goto breakLoop;
                            }
                        }
                    }
                    breakLoop:
                    if (!(squares[hIndex2, vIndex2].activeSelf && squares[hIndex2, vIndex2].GetComponent<Image>().color != Color.blue))
                    {
                        squares[hIndex2, vIndex2].SetActive(true);
                        squares[hIndex2, vIndex2].GetComponent<Image>().color = Color.blue;
                    }
                    else
                    {
                        foreach (var item in squares)
                        {
                            item.SetActive(false);
                        }
                        MPSquares1[hIndex2, vIndex2].SetActive(true);
                        MPSquares2[hIndex2, vIndex2].SetActive(true);
                    }
                }
            }
            if (Input.GetButtonDown("A2"))
            {
                confirmed[1] = !confirmed[1];
                if (!confirmed[1])
                {
                    if (hIndex != hIndex2 || vIndex != vIndex2)
                    {
                        confirmedTimer2 = 0;
                        squares[hIndex2, vIndex2].GetComponent<Image>().color = Color.blue;
                    }
                    else
                    {
                        MPSquares2[hIndex2, vIndex2].GetComponent<Image>().color = Color.blue;
                    }
                }
            }
            if (confirmed[1])
            {
                if (hIndex != hIndex2 || vIndex != vIndex2)
                {
                    squares[hIndex2, vIndex2].GetComponent<Image>().color = Color.Lerp(Color.blue, Color.white, Mathf.PingPong(confirmedTimer2, 1));
                    confirmedTimer2 += Time.deltaTime * blinkingSpeed;
                }
                else
                {
                    MPSquares2[hIndex2, vIndex2].GetComponent<Image>().color = Color.Lerp(Color.blue, Color.white, Mathf.PingPong(confirmedTimer2, 1));
                    confirmedTimer2 += Time.deltaTime * blinkingSpeed;
                }
            }
        }
        if ((confirmed[0] && confirmed[1] && multiplayer) || (confirmed[0] && !multiplayer))
        {
            SceneManager.LoadScene($"Game({game})");
        }
    }
}
