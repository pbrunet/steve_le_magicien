using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollow : MonoBehaviour
{
    [SerializeField] private Camera camera;

    // Start is called before the first frame update
    void Start()
    {
        if(camera == null)
        {
            camera = Camera.main;
        }
    }

    // Update is called once per frame
    void Update()
    {
        camera.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, camera.transform.position.z);
    }
}
