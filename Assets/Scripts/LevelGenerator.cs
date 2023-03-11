using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class LevelGenerator : MonoBehaviour
{

    [SerializeField] int NumCells = 10;
    int RemainingNumCells;
    [SerializeField] string nextLevel = "";
    [SerializeField] GoToLevel nextLevelPrefab;

    private float xSize = 90f;
    private float ySize = 60f;


    private GeneratedCell[][] grid;

    // Start is called before the first frame update
    void Start()
    {
        RemainingNumCells = NumCells;
        grid = new GeneratedCell[NumCells][];
        for (int i = 0; i < NumCells; i++)
        {
            grid[i] = new GeneratedCell[NumCells];
        }

        List<Tuple<int, int>> GeneratedPos = new List<Tuple<int, int>>();

        GenNeighboor(GetComponent<GeneratedCell>(), new Tuple<int, int>(NumCells / 2, NumCells / 2), GeneratedPos);


        for (int i = 0; i < grid.Length; i++)
        {
            for (int j = 0; j < grid[i].Length; j++)
            {
                if (i == NumCells / 2 && j == NumCells / 2) continue; // This is the seed!
                if (grid[i][j] == null) continue;
                Instantiate(grid[i][j], new Vector3((i - NumCells / 2) * xSize, (j - NumCells / 2) * ySize, gameObject.transform.position.z), gameObject.transform.rotation, gameObject.transform);
            }
        }

        SpawnNextLevel();
    }

    bool GenNeighboor(GeneratedCell currentCell, Tuple<int, int> pos, List<Tuple<int, int>> generatedPos)
    {
        Debug.Log("Generate " + currentCell.name + " at pos " + pos.Item1.ToString() + "/" + pos.Item2.ToString() + "\n");
        grid[pos.Item1][pos.Item2] = currentCell;
        RemainingNumCells--;

        List<Tuple<int, int>> localGeneratedPos = new List<Tuple<int, int>>();
        localGeneratedPos.Add(pos);

        var neighborCell = currentCell.neighborCell;

        bool failed = false;

        for (int i = 0; i < neighborCell.Length; i++)
        {
            Debug.Log("Try to generate in dir : " + i.ToString() + "\n");

            if (neighborCell[i].cells.Count == 0)
            {
                Debug.Log("Can't generate cell in dir " + i.ToString() + " at it " + currentCell.name + " doesn't have neighboor in this direction\n");
                continue;
            }

            Tuple<int, int> newPos;
            switch ((GeneratedCell.NeightborSide)i)
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

            if (newPos.Item1 < 0 || newPos.Item2 < 0 || newPos.Item1 >= grid.Length || newPos.Item2 >= grid[0].Length)
            {
                Debug.Log("Can't generate cell in dir " + i.ToString() + " at it " + currentCell.name + " is on the border of the grid\n");
                failed = true;
                break;
            }

            if (RemainingNumCells == 0)
            {
                Debug.Log("Stop generating cells, numCells reached\n");
                failed = true;
                break;
            }

            if (grid[newPos.Item1][newPos.Item2] != null)
            {
                Debug.Log("Can't generate cell in dir " + i.ToString() + " at from " + pos.Item1.ToString() + "/" + pos.Item2.ToString() + " as it is already spawned\n");
                continue;
            }

            List<GeneratedCell> possibleCells = new List<GeneratedCell>(neighborCell[i].cells);

            while (true)
            {

                GeneratedCell sideCell = PickCell(newPos, possibleCells);

                if (sideCell == null)
                {
                    Debug.Log("Can't generate cell in dir " + i.ToString() + " at from " + pos.Item1.ToString() + "/" + pos.Item2.ToString() + " as their are not such possible case\n");
                    failed = true;
                    break;
                }

                if (GenNeighboor(sideCell, newPos, localGeneratedPos))
                {
                    break;
                }
                else
                {
                    possibleCells.Remove(sideCell);
                }
            }
            if (failed) { break; }
        }

        if (failed)
        {
            foreach (Tuple<int, int> newPos in localGeneratedPos)
            {
                grid[newPos.Item1][newPos.Item2] = null;
                RemainingNumCells++;
            }
            return false;
        }
        else
        {

            generatedPos.AddRange(localGeneratedPos);
            return true;
        }
    }

    GeneratedCell.NeightborSide InvertSide(GeneratedCell.NeightborSide side)
    {
        switch (side)
        {
            case GeneratedCell.NeightborSide.Left:
                return GeneratedCell.NeightborSide.Right;
            case GeneratedCell.NeightborSide.Right:
                return GeneratedCell.NeightborSide.Left;
            case GeneratedCell.NeightborSide.Top:
                return GeneratedCell.NeightborSide.Bottom;
            case GeneratedCell.NeightborSide.Bottom:
                return GeneratedCell.NeightborSide.Top;
            default:
                return GeneratedCell.NeightborSide.Right;
        }
    }

    GeneratedCell PickCell(Tuple<int, int> newPos, List<GeneratedCell> possibleCells)
    {
        HashSet<GeneratedCell> cells = new HashSet<GeneratedCell>(possibleCells);

        for (int i = 0; i < 4; i++)
        {
            Tuple<int, int> neighborPos;
            switch ((GeneratedCell.NeightborSide)i)
            {
                case GeneratedCell.NeightborSide.Left:
                    neighborPos = new Tuple<int, int>(newPos.Item1 - 1, newPos.Item2); break;
                case GeneratedCell.NeightborSide.Right:
                    neighborPos = new Tuple<int, int>(newPos.Item1 + 1, newPos.Item2); break;
                case GeneratedCell.NeightborSide.Top:
                    neighborPos = new Tuple<int, int>(newPos.Item1, newPos.Item2 + 1); break;
                case GeneratedCell.NeightborSide.Bottom:
                    neighborPos = new Tuple<int, int>(newPos.Item1, newPos.Item2 - 1); break;
                default:
                    neighborPos = new Tuple<int, int>(newPos.Item1, newPos.Item2 - 1);
                    break;
            }

            if (neighborPos.Item1 < 0 || neighborPos.Item2 < 0 || neighborPos.Item1 >= grid.Length || neighborPos.Item2 >= grid[0].Length)
            {
                continue;
            }

            if (grid[neighborPos.Item1][neighborPos.Item2] != null)
            {
                var cellsFromNeighbor = grid[neighborPos.Item1][neighborPos.Item2].neighborCell[(int)InvertSide((GeneratedCell.NeightborSide)i)].cells;
                if (cellsFromNeighbor.Count == 0)
                {
                    cells.RemoveWhere(cell => cell.neighborCell[i].cells.Count > 0);
                }
                else
                {
                    cells.IntersectWith(cellsFromNeighbor);
                }
            }
        }
        if (cells.Count == 0)
        {
            Debug.Log("Can't pick a cell at " + newPos.Item1.ToString() + "/" + newPos.Item2.ToString() + " \n");
            return null;
        }

        return cells.ElementAt(UnityEngine.Random.Range(0, cells.Count));
    }

    void SpawnNextLevel()
    {
        int[][] distFromSeed = new int[NumCells][];
        for (int i = 0; i < NumCells; i++)
        {
            distFromSeed[i] = new int[NumCells];
            Array.Fill<int>(distFromSeed[i], NumCells * NumCells);
        }

        List<Tuple<int, int>> Candidate = new List<Tuple<int, int>>();
        HashSet<Tuple<int, int>> ToProcess = new HashSet<Tuple<int, int>>();
        HashSet<Tuple<int, int>> ToProcessNextDist = new HashSet<Tuple<int, int>>();

        int currentDist = 0;
        ToProcess.Add(new Tuple<int, int>(NumCells / 2, NumCells / 2));
        distFromSeed[NumCells / 2][NumCells / 2] = currentDist;
        currentDist++;

        do
        {
            Candidate.Clear();
            while (ToProcess.Count > 0)
            {
                Tuple<int, int> pos = ToProcess.Last();
                ToProcess.Remove(pos);
                Candidate.Add(pos);

                for (int i = 0; i < 4; i++)
                {

                    Tuple<int, int> newPos;
                    switch ((GeneratedCell.NeightborSide)i)
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

                    if (newPos.Item1 < 0 || newPos.Item2 < 0 || newPos.Item1 >= grid.Length || newPos.Item2 >= grid[0].Length || grid[newPos.Item1][newPos.Item2] == null)
                    {
                        continue;
                    }

                    if(!grid[pos.Item1][pos.Item2].neighborCell[i].cells.Contains(grid[newPos.Item1][newPos.Item2]))
                    {
                        // No path from this cell to the other one
                        continue;
                    }

                    if (distFromSeed[newPos.Item1][newPos.Item2] <= currentDist)
                    {
                        // This cell is already processed or empty
                        continue;
                    }

                    distFromSeed[newPos.Item1][newPos.Item2] = currentDist;
                    ToProcessNextDist.Add(newPos);
                }
            }

            ToProcess = ToProcessNextDist;
            ToProcessNextDist = new HashSet<Tuple<int, int>>();
            currentDist++;
        } while (ToProcess.Count > 0);


        Tuple<int, int> cellWithExit = Candidate[UnityEngine.Random.Range(0, Candidate.Count)];
        GeneratedCell genWithExit = grid[cellWithExit.Item1][cellWithExit.Item2];
        GoToLevel spawnedNextLevel = Instantiate<GoToLevel>(nextLevelPrefab, new Vector3((cellWithExit.Item1 - NumCells / 2) * xSize + genWithExit.possibleExitPos.x + nextLevelPrefab.transform.position.x, (cellWithExit.Item2 - NumCells / 2) * ySize + genWithExit.possibleExitPos.y + nextLevelPrefab.transform.position.y, gameObject.transform.position.z), gameObject.transform.rotation, gameObject.transform);
        spawnedNextLevel.SetNextLevel(nextLevel);

    }
}
