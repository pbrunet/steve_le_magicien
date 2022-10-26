//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class BuildingBehavior : MonoBehaviour
//{
//    public int cost = 50;
//    public GameObject range;

//    private void OnUnselect()
//    {
//        range.SetActive(false);
//    }

//    private void OnMouseDown()
//    {
//        float shotRange;
//        ShootAt shoot;
//        if(TryGetComponent<ShootAt>(out shoot))
//        {
//            shotRange = shoot.Range;
//        } else
//        {
//            shotRange = GetComponent<SpawnDefender>().range;
//        }

//        LevelManager.Instance.SelectObject(gameObject, OnUnselect);
//        range.SetActive(true);
//        float coef = range.GetComponent<SpriteRenderer>().bounds.size.x / range.transform.localScale.x * 2;
//        range.transform.localScale = new Vector3(shotRange * coef, shotRange * coef, 1);
//    }

//    // Start is called before the first frame update
//    void Start()
//    {
        
//    }

//    // Update is called once per frame
//    void Update()
//    {
        
//    }
//}
