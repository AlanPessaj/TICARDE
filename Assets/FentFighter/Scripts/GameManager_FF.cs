using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager_FF : MonoBehaviour
{
    public GameObject[] characters;
    public GameObject[] maps;
    public GameObject map;
    public GameObject[] players;
    public GameObject[] UI;
    public CameraController_FF cameraController;

    private void Start()
    {
        Vector3[] playerPos = new Vector3[players.Length];
        Transform environment = map.transform.parent;
        for (int i = 0; i < players.Length; i++)
        {
            playerPos[i] = players[i].transform.position;
            Destroy(players[i]);
        }
        Destroy(map);
        int mapIndex;
        if (Random.value > 0.5f)
        {
            mapIndex = GameData.char1;
        }
        else
        {
            mapIndex = GameData.char2;
        }
        Instantiate(maps[mapIndex]).transform.parent = environment;
        players[0] = Instantiate(characters[GameData.char1], playerPos[0], Quaternion.Euler(0, 90, 0));
        players[1] = Instantiate(characters[GameData.char2], playerPos[1], Quaternion.Euler(0, -90, 0));
        SceneManager.MoveGameObjectToScene(players[0], gameObject.scene);
        SceneManager.MoveGameObjectToScene(players[1], gameObject.scene);
        players[0].GetComponent<PlayerController_FF>().otherPlayer = players[1];
        players[1].GetComponent<PlayerController_FF>().otherPlayer = players[0];
        players[0].name = "Player1";
        players[1].name = "Player2";
        players[0].GetComponent<UIManager_FF>().UI = UI[0];
        players[1].GetComponent<UIManager_FF>().UI = UI[1];
        cameraController.players = players;
    }
}
