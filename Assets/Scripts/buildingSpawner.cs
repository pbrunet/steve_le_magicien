using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class buildingSpawner : MonoBehaviour
{
    //[EnumNamedArray(typeof(BuildingKind))]
    //public BuildingBehavior[] buildings;// = new BuildingBehavior[COMPONENTS_COUNT];
    // private Rigidbody2D rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        // rigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnMouseDown()
    {
        //UIManager.Instance.ShowBuildingSpawner(this, OnBuildingSelected);
    }

    //// Update is called once per frame
    //private void OnBuildingSelected(BuildingKind kind)
    //{
    //    if (LevelManager.Instance.Pay(buildings[(int)kind].cost))
    //    {
    //        Instantiate(buildings[(int)kind].gameObject, transform.position, Quaternion.identity);
    //        Destroy(gameObject);
    //    }
    //}
}
