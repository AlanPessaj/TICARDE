using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeSpawner_FG : MonoBehaviour
{
    public GameObject tree;
    public Generator_FG generator;
    // Start is called before the first frame update
    void Start()
    {
        int treeRange = Random.Range(-12, 13);
        if (!(treeRange >= generator.treeSeparator - 2 && treeRange <= generator.treeSeparator + 2))
        {
            if (generator.treeSpawn)
            {
                Instantiate(tree, new Vector3(transform.position.x, transform.position.y - 2, generator.treeSeparator), Quaternion.identity);
                generator.treeSeparator = treeRange;
            }
            generator.treeSpawn = !generator.treeSpawn;
        }
        else
        {
            Start();
            return;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Awake()
    {
        generator = GameObject.Find("GAMEMANAGER").GetComponent<Generator_FG>();
    }
}
