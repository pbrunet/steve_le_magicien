using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    [SerializeField] public List<Vector2> waypoints = new List<Vector2>();
    public float distTolerance = 5;
    public float xSpeed = 10;

    public void Start()
    {
    //    for(int i=0; i<waypoints.Count; i++) {
    //        waypoints[i] = transform.TransformPoint(waypoints[i]);
    //    }
    }
}
