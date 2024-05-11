using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeSpawner : MonoBehaviour
{

    private int treeNum = 15;
    [SerializeField] private GameObject tree;
    [SerializeField] private Transform background;
    private bool[,] positionInfo;    // at each square, whether or not the square has tree. no tree = false
    private int maxX;
    private int maxY;
    private GameObject trees;    // empty object to hold the trees

    // Start is called before the first frame update
    void Start()
    {
        maxX = (int)background.localScale.x / 2;
        maxY = (int)background.localScale.y / 2;

        positionInfo = new bool[maxX * 2, maxY * 2];

        trees = new GameObject("Trees");

        for (int i = 0; i < treeNum; i++)
        {
            spawnTree();
        }
    }

    private void spawnTree()
    {

        int x = Random.Range(0, maxX * 2);
        int y = Random.Range(0, maxY * 2);
        while (positionInfo[x,y])    // if selected random position has tree, choose another one
        {
            x = Random.Range(0, maxX * 2);
            y = Random.Range(0, maxY * 2);
        }
        positionInfo[x, y] = true;

        x -= maxX;
        y -= maxY;
        Vector3 randomPos = new Vector3(x + 0.5f, y + 0.5f, 0);

        // make trees can't be spawned at same position

        Instantiate(tree, randomPos, Quaternion.identity, trees.transform);
    }
}
