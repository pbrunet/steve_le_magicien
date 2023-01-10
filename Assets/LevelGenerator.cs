using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class LevelGenerator : MonoBehaviour
{

    [SerializeField] int NumCells = 10;

    private float xSize = 90f;
    private float ySize = 60f;


    private GeneratedCell[][] grid;

    private List<Tuple<int, int>> usedCells = new List<Tuple<int, int>>();

    // Start is called before the first frame update
    void Start()
    {
        grid = new GeneratedCell[NumCells][];
        for (int i = 0; i < NumCells; i++)
        {
            grid[i] = new GeneratedCell[NumCells];
        }

        GenNeighboor(GetComponent<GeneratedCell>(), new Tuple<int, int>(5, 5));


        for (int i = 0; i < grid.Length; i++)
        {
            for (int j = 0; j < grid[i].Length; j++)
            {
                if (i == 5 && j == 5) continue; // This is the seed!
                if(grid[i][j] == null) continue;
                Instantiate(grid[i][j], new Vector3((i - 5) * xSize, (j - 5) * ySize, gameObject.transform.position.z), gameObject.transform.rotation, gameObject.transform);
            }
        }
    }

    void GenNeighboor(GeneratedCell currentCell, Tuple<int, int> pos)
    {
        grid[pos.Item1][pos.Item2] = currentCell;

        var neighborCell = currentCell.neighborCell;

        for (int i = 0; i < neighborCell.Length; i++)
        {

            if (NumCells == 0)
            {
                break;
            }

            if (neighborCell[i].cells.Count == 0)
            {
                continue;
            }

            Tuple<int, int> newPos;
            switch((GeneratedCell.NeightborSide)i)
            {
                case GeneratedCell.NeightborSide.Left:
                    newPos = new Tuple<int, int>(pos.Item1 - 1, pos.Item2); break;
                case GeneratedCell.NeightborSide.Right:
                    newPos = new Tuple<int, int>(pos.Item1 + 1, pos.Item2); break;
                case GeneratedCell.NeightborSide.Top:
                    newPos = new Tuple<int, int>(pos.Item1, pos.Item2 + 1); break;
                case GeneratedCell.NeightborSide.Bottom:
                    newPos = new Tuple<int, int>(pos.Item1, pos.Item2 - 1); break;
                default:
                    newPos = new Tuple<int, int>(pos.Item1, pos.Item2 - 1); 
                    break;
            }

            if(newPos.Item1 < 0 || newPos.Item2 < 0 || newPos.Item1 >= grid.Length || newPos.Item2 >= grid[0].Length) {
                continue;
            }

            if (grid[newPos.Item1][newPos.Item2] != null)
            {
                continue;
            }

            NumCells--;

            // FIXME: We should also consider "other neightbor" to find a common tile
            GeneratedCell sideCell = neighborCell[i].cells[UnityEngine.Random.Range(0, neighborCell[i].cells.Count)];

            GenNeighboor(sideCell, newPos);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
