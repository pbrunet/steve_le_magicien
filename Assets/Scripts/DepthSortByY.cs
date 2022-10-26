using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;


[RequireComponent(typeof(SortingGroup))]
public class DepthSortByY : MonoBehaviour
{

    private const int occuracy = 100;

    void Update()
    {
        SortingGroup sort = GetComponent<SortingGroup>();
        sort.sortingOrder = -(int)(transform.position.y * occuracy);
    }
}