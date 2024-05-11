using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// fill with a given grid tile
public class FloorGenerator : MonoBehaviour
{
    private float startX;
    private float startY;
    private int row;
    private int col;
    private float tileSize = 1f;
    private GameObject grids;
    [SerializeField] private GameObject grid;

    // Start is called before the first frame update
    void Start()
    {
        startY = transform.parent.localScale.y;
        startX = transform.parent.localScale.x;
        row = (int)startY;
        col = (int)startX;

        startY = startY / -2 + 0.5f;
        startX = startX / -2 + 0.5f;

        grids = new GameObject("Grids");

        generateGrid();
    }

    private void generateGrid()
    {
        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < col; j++)
            {
                //Debug.Log(i);
                Instantiate(grid, new Vector3(startX, startY, 0), Quaternion.identity, grids.transform);
                startX += tileSize;
            }
            startX = (float)col / -2 + 0.5f;
            startY += tileSize;
        }
    }
}
