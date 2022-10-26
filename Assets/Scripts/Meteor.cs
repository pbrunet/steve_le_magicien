using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Meteor : MonoBehaviour
{
    [SerializeField] private float duration;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("Block", 0.5f);
        Invoke("End", duration);
    }

    void End()
    {
        Destroy(gameObject);
    }

    void Block()
    {
        GetComponent<NavMeshObstacle>().enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
