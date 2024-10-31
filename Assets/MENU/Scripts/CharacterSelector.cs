using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class CharacterSelector : MonoBehaviour
{
    public string name1;
    public string name2;
    public GameObject[] pSquares;
    GameObject[,] squares;
    public GameObject[] pMPSquares1;
    GameObject[,] MPSquares1;
    public GameObject[] pMPSquares2;
    GameObject[,] MPSquares2;
    public GameObject[] pP1txt;
    GameObject[,] p1txt;
    public GameObject[] pP2txt;
    GameObject[,] p2txt;
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
    bool loadingScene;
    // Start is called before the first frame update
    void Start()
    {
        name1 = GameData.name1;
        name2 = GameData.name2;
        multiplayer = GameData.multiplayer;
        Debug.Log($"Name1: {name1}, Name2: {name2}, Multiplayer: {multiplayer}");
        GameData.name1 = null;
        GameData.name2 = null;
        GameData.multiplayer = false;

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
        p1txt = new GameObject[pP1txt.Length / 2, pP1txt.Length / 2];
        p = 0;
        for (int i = 0; i < pP1txt.Length / 2; i++)
        {
            for (int o = 0; o < pP1txt.Length / 2; o++)
            {
                p1txt[i, o] = pP1txt[p];
                p++;
            }
        }
        p2txt = new GameObject[pP2txt.Length / 2, pP2txt.Length / 2];
        p = 0;
        for (int i = 0; i < pP2txt.Length / 2; i++)
        {
            for (int o = 0; o < pP2txt.Length / 2; o++)
            {
                p2txt[i, o] = pP2txt[p];
                p++;
            }
        }
        if (!multiplayer)
        {
            MPSquares1[0, 0].SetActive(false);
            MPSquares2[0, 0].SetActive(false);
            squares[0, 0].SetActive(true);
            squares[0, 0].GetComponent<Image>().color = new Color(1, 1, 0);
            p2txt[0, 0].SetActive(false);
            p1txt[0, 0].transform.position += new Vector3(50, 0, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!loadingScene)
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
                                MoveText(false);
                                goto breakLoop;
                            }
                        }
                    }
                breakLoop:
                    if (!(squares[hIndex, vIndex].activeSelf && squares[hIndex, vIndex].GetComponent<Image>().color != new Color(1, 1, 0)))
                    {
                        squares[hIndex, vIndex].SetActive(true);
                        squares[hIndex, vIndex].GetComponent<Image>().color = new Color(1, 1, 0);
                        MoveText(true);
                    }
                    else
                    {
                        foreach (var item in squares)
                        {
                            item.SetActive(false);
                        }
                        MPSquares1[hIndex, vIndex].SetActive(true);
                        MPSquares2[hIndex, vIndex].SetActive(true);
                        MoveText(null);
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
                                MoveText(false);
                                goto breakLoop;
                            }
                        }
                    }
                breakLoop:
                    if (!(squares[hIndex, vIndex].activeSelf && squares[hIndex, vIndex].GetComponent<Image>().color != new Color(1, 1, 0)))
                    {
                        squares[hIndex, vIndex].SetActive(true);
                        squares[hIndex, vIndex].GetComponent<Image>().color = new Color(1, 1, 0);
                        MoveText(true);
                    }
                    else
                    {
                        foreach (var item in squares)
                        {
                            item.SetActive(false);
                        }
                        MPSquares1[hIndex, vIndex].SetActive(true);
                        MPSquares2[hIndex, vIndex].SetActive(true);
                        MoveText(null);
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
                                MoveText(false);
                                goto breakLoop;
                            }
                        }
                    }
                breakLoop:
                    if (!(squares[hIndex, vIndex].activeSelf && squares[hIndex, vIndex].GetComponent<Image>().color != new Color(1, 1, 0)))
                    {
                        squares[hIndex, vIndex].SetActive(true);
                        squares[hIndex, vIndex].GetComponent<Image>().color = new Color(1, 1, 0);
                        MoveText(true);
                    }
                    else
                    {
                        foreach (var item in squares)
                        {
                            item.SetActive(false);
                        }
                        MPSquares1[hIndex, vIndex].SetActive(true);
                        MPSquares2[hIndex, vIndex].SetActive(true);
                        MoveText(null);
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
                                MoveText(false);
                                goto breakLoop;
                            }
                        }
                    }
                breakLoop:
                    if (!(squares[hIndex, vIndex].activeSelf && squares[hIndex, vIndex].GetComponent<Image>().color != new Color(1, 1, 0)))
                    {
                        squares[hIndex, vIndex].SetActive(true);
                        squares[hIndex, vIndex].GetComponent<Image>().color = new Color(1, 1, 0);
                        MoveText(true);
                    }
                    else
                    {
                        foreach (var item in squares)
                        {
                            item.SetActive(false);
                        }
                        MPSquares1[hIndex, vIndex].SetActive(true);
                        MPSquares2[hIndex, vIndex].SetActive(true);
                        MoveText(null);
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
                        squares[hIndex, vIndex].GetComponent<Image>().color = new Color(1, 1, 0);
                    }
                    else
                    {
                        MPSquares1[hIndex, vIndex].GetComponent<Image>().color = new Color(1, 1, 0);
                    }
                    confirmedTimer = 0;
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
                                    MoveText(true);
                                    goto breakLoop;
                                }
                            }
                        }
                    breakLoop:
                        if (!(squares[hIndex2, vIndex2].activeSelf && squares[hIndex2, vIndex2].GetComponent<Image>().color != Color.blue))
                        {
                            squares[hIndex2, vIndex2].SetActive(true);
                            squares[hIndex2, vIndex2].GetComponent<Image>().color = Color.blue;
                            MoveText(false);
                        }
                        else
                        {
                            foreach (var item in squares)
                            {
                                item.SetActive(false);
                            }
                            MPSquares1[hIndex2, vIndex2].SetActive(true);
                            MPSquares2[hIndex2, vIndex2].SetActive(true);
                            MoveText(null);
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
                                    MoveText(true);
                                    goto breakLoop;
                                }
                            }
                        }
                    breakLoop:
                        if (!(squares[hIndex2, vIndex2].activeSelf && squares[hIndex2, vIndex2].GetComponent<Image>().color != Color.blue))
                        {
                            squares[hIndex2, vIndex2].SetActive(true);
                            squares[hIndex2, vIndex2].GetComponent<Image>().color = Color.blue;
                            MoveText(false);
                        }
                        else
                        {
                            foreach (var item in squares)
                            {
                                item.SetActive(false);
                            }
                            MPSquares1[hIndex2, vIndex2].SetActive(true);
                            MPSquares2[hIndex2, vIndex2].SetActive(true);
                            MoveText(null);
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
                                    MoveText(true);
                                    goto breakLoop;
                                }
                            }
                        }
                    breakLoop:
                        if (!(squares[hIndex2, vIndex2].activeSelf && squares[hIndex2, vIndex2].GetComponent<Image>().color != Color.blue))
                        {
                            squares[hIndex2, vIndex2].SetActive(true);
                            squares[hIndex2, vIndex2].GetComponent<Image>().color = Color.blue;
                            MoveText(false);
                        }
                        else
                        {
                            foreach (var item in squares)
                            {
                                item.SetActive(false);
                            }
                            MPSquares1[hIndex2, vIndex2].SetActive(true);
                            MPSquares2[hIndex2, vIndex2].SetActive(true);
                            MoveText(null);
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
                                    MoveText(true);
                                    goto breakLoop;
                                }
                            }
                        }
                    breakLoop:
                        if (!(squares[hIndex2, vIndex2].activeSelf && squares[hIndex2, vIndex2].GetComponent<Image>().color != Color.blue))
                        {
                            squares[hIndex2, vIndex2].SetActive(true);
                            squares[hIndex2, vIndex2].GetComponent<Image>().color = Color.blue;
                            MoveText(false);
                        }
                        else
                        {
                            foreach (var item in squares)
                            {
                                item.SetActive(false);
                            }
                            MPSquares1[hIndex2, vIndex2].SetActive(true);
                            MPSquares2[hIndex2, vIndex2].SetActive(true);
                            MoveText(null);
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
                            squares[hIndex2, vIndex2].GetComponent<Image>().color = Color.blue;
                        }
                        else
                        {
                            MPSquares2[hIndex2, vIndex2].GetComponent<Image>().color = Color.blue;
                        }
                        confirmedTimer2 = 0;
                    }
                }
            }
            if ((confirmed[0] && confirmed[1] && multiplayer) || (confirmed[0] && !multiplayer))
            {
                StartCoroutine(NextScene());
            }
        }
        if (confirmed[0])
        {
            confirmedTimer += Time.deltaTime * blinkingSpeed;
            if (hIndex != hIndex2 || vIndex != vIndex2)
            {
                squares[hIndex, vIndex].GetComponent<Image>().color = Color.Lerp(new Color(1, 1, 0), Color.white, Mathf.PingPong(confirmedTimer, 1));
            }
            else
            {
                MPSquares1[hIndex, vIndex].GetComponent<Image>().color = Color.Lerp(new Color(1, 1, 0), Color.white, Mathf.PingPong(confirmedTimer, 1));
            }
        }
        if (confirmed[1])
        {
            confirmedTimer2 += Time.deltaTime * blinkingSpeed;
            if (hIndex != hIndex2 || vIndex != vIndex2)
            {
                squares[hIndex2, vIndex2].GetComponent<Image>().color = Color.Lerp(Color.blue, Color.white, Mathf.PingPong(confirmedTimer2, 1));
            }
            else
            {
                MPSquares2[hIndex2, vIndex2].GetComponent<Image>().color = Color.Lerp(Color.blue, Color.white, Mathf.PingPong(confirmedTimer2, 1));
            }
        }
    }

    void MoveText(bool? textToMove)
    {
        //true = P1, false = P2, null = both
        switch (textToMove)
        {
            case true:
                foreach (var item in p1txt)
                {
                    item.SetActive(false);
                }
                p1txt[hIndex, vIndex].SetActive(true);
                if (Mathf.Abs(p1txt[hIndex, vIndex].transform.localPosition.x) != 100)
                {
                    if (p1txt[hIndex, vIndex].transform.localPosition.x > 0)
                    {
                        p1txt[hIndex, vIndex].transform.localPosition = new Vector3(100, p1txt[hIndex, vIndex].transform.localPosition.y, p1txt[hIndex, vIndex].transform.localPosition.z);
                    }
                    else
                    {
                        p1txt[hIndex, vIndex].transform.localPosition = new Vector3(-100, p1txt[hIndex, vIndex].transform.localPosition.y, p1txt[hIndex, vIndex].transform.localPosition.z);
                    }
                }
            break;
            case false:
                foreach (var item in p2txt)
                {
                    item.SetActive(false);
                }
                p2txt[hIndex2, vIndex2].SetActive(true);
                if (Mathf.Abs(p2txt[hIndex2, vIndex2].transform.localPosition.x) != 100)
                {
                    if (p2txt[hIndex2, vIndex2].transform.localPosition.x > 0)
                    {
                        p2txt[hIndex2, vIndex2].transform.localPosition = new Vector3(100, p2txt[hIndex2, vIndex2].transform.localPosition.y, p2txt[hIndex2, vIndex2].transform.localPosition.z);
                    }
                    else
                    {
                        p2txt[hIndex2, vIndex2].transform.localPosition = new Vector3(-100, p2txt[hIndex2, vIndex2].transform.localPosition.y, p2txt[hIndex2, vIndex2].transform.localPosition.z);
                    }
                }
            break;
            case null:
                foreach (var item in p1txt)
                {
                    item.SetActive(false);
                }
                foreach (var item in p2txt)
                {
                    item.SetActive(false);
                }
                if (Mathf.Abs(p2txt[hIndex2, vIndex2].transform.localPosition.x) != 100)
                {
                    if (p2txt[hIndex2, vIndex2].transform.localPosition.x > 0)
                    {
                        p2txt[hIndex2, vIndex2].transform.localPosition = new Vector3(100, p2txt[hIndex2, vIndex2].transform.localPosition.y, p2txt[hIndex2, vIndex2].transform.localPosition.z);
                    }
                    else
                    {
                        p2txt[hIndex2, vIndex2].transform.localPosition = new Vector3(-100, p2txt[hIndex2, vIndex2].transform.localPosition.y, p2txt[hIndex2, vIndex2].transform.localPosition.z);
                    }
                }
                if (Mathf.Abs(p1txt[hIndex, vIndex].transform.localPosition.x) != 100)
                {
                    if (p1txt[hIndex, vIndex].transform.localPosition.x > 0)
                    {
                        p1txt[hIndex, vIndex].transform.localPosition = new Vector3(100, p1txt[hIndex, vIndex].transform.localPosition.y, p1txt[hIndex, vIndex].transform.localPosition.z);
                    }
                    else
                    {
                        p1txt[hIndex, vIndex].transform.localPosition = new Vector3(-100, p1txt[hIndex, vIndex].transform.localPosition.y, p1txt[hIndex, vIndex].transform.localPosition.z);
                    }
                }
                p1txt[hIndex, vIndex].transform.localPosition -= new Vector3(50, 0, 0);
                p2txt[hIndex2, vIndex2].transform.localPosition += new Vector3(50, 0, 0);
                p1txt[hIndex, vIndex].SetActive(true);
                p2txt[hIndex2, vIndex2].SetActive(true);
            break;
        }
    }

    IEnumerator NextScene()
    {
        loadingScene = true;
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene($"Game({game})", LoadSceneMode.Additive);
        yield return null;
        Scene nextScene = SceneManager.GetSceneByName($"Game({game})");
        int[,] translate = new int[pMPSquares2.Length / 2, pMPSquares2.Length / 2];
        int p = 0;
        for (int i = 0; i < pMPSquares2.Length / 2; i++)
        {
            for (int o = 0; o < pMPSquares2.Length / 2; o++)
            {
                translate[i, o] = p;
                p++;
            }
        }
        nextScene.GetRootGameObjects()[0].GetComponent<GameInfo>().char1 = translate[hIndex, vIndex];
        nextScene.GetRootGameObjects()[0].GetComponent<GameInfo>().char2 = translate[hIndex2, vIndex2];
        nextScene.GetRootGameObjects()[0].GetComponent<GameInfo>().name1 = name1;
        nextScene.GetRootGameObjects()[0].GetComponent<GameInfo>().name2 = name2;
        nextScene.GetRootGameObjects()[0].GetComponent<GameInfo>().loadAction();
        SceneManager.UnloadSceneAsync(gameObject.scene);
    }
}
