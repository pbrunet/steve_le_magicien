using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{

    [SerializeField] int NumCells = 10;

    private float xSize = 90f;
    private float ySize = 60f;

    private List<Tuple<int, int>> usedCells = new List<Tuple<int, int>>();

    // Start is called before the first frame update
    void Start()
    {
        GenNeighboor(GetComponent<GeneratedCell>(), new Tuple<int, int>( 0, 0 ));
    }

    void GenNeighboor(GeneratedCell currentCell, Tuple<int, int> pos)
    {
        usedCells.Add(pos);
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
            if(usedCells.Contains(newPos))
            {
                continue;
            }

            NumCells--;
            usedCells.Add(newPos);

            // FIXME: We should also consider "other neightbor" to find a common tile
            GeneratedCell sideCell = neighborCell[i].cells[UnityEngine.Random.Range(0, neighborCell[i].cells.Count)];

            Instantiate(sideCell, new Vector3(newPos.Item1 * xSize, newPos.Item2 * ySize, gameObject.transform.position.z), gameObject.transform.rotation, gameObject.transform);

            GenNeighboor(sideCell, newPos);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
