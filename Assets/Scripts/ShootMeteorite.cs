using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootMeteorite : MonoBehaviour
{
    public GameObject meteor;

    public event System.Action OnShoot;
    public event System.Action OnDisarm;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            target.z = transform.position.z;
            Instantiate(meteor, target, Quaternion.identity);
            if (OnShoot != null)
            {
                OnShoot();
            }
            enabled = false;
        }
        if (Input.GetMouseButtonDown(1))
        {
            if(OnDisarm != null)
            {
                OnDisarm();
            }
            enabled = false;
        }

    }
}
