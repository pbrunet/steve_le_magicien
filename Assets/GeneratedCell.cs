using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NeightborList
{
    public List<GeneratedCell> cells;
}

public class GeneratedCell : MonoBehaviour
{
    public enum NeightborSide
    {
        Left,
        Right,
        Top,
        Bottom
    }

    [EnumNamedArrayAttribute(typeof(NeightborSide))]
    [SerializeField] public NeightborList[] neighborCell = new NeightborList[4];

    // Start is called before the first frame update
    void Start()
    {
    } 

    // Update is called once per frame
    void Update()
    {
        
    }
}
