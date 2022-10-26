using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    public List<Vector2> waypoints = new List<Vector2>();
    public float distTolerance = 5;
    public float xSpeed = 10;
}
