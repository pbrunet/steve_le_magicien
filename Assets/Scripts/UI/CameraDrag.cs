using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CameraDrag : MonoBehaviour
{
    public float maxZoom;
    public float minZoom;
    public float panSpeed = -1;
    public GameObject fullBackground;

    Vector3 bottomLeft;
    Vector3 topRight;

    float cameraMaxY;
    float cameraMinY;
    float cameraMaxX;
    float cameraMinX;

    void Start()
    {
        maxZoom = Math.Min(maxZoom, fullBackground.GetComponent<SpriteRenderer>().bounds.max.y);
        maxZoom = Math.Min(maxZoom, fullBackground.GetComponent<SpriteRenderer>().bounds.max.x / Camera.main.aspect);
        //set max camera bounds (assumes camera is max zoom and centered on Start)
        topRight = fullBackground.GetComponent<SpriteRenderer>().bounds.max;
        bottomLeft = fullBackground.GetComponent<SpriteRenderer>().bounds.min;
        cameraMaxX = topRight.x;
        cameraMaxY = topRight.y;
        cameraMinX = bottomLeft.x;
        cameraMinY = bottomLeft.y;
    }

    void Update()
    {
        //click and drag
        if (Input.GetMouseButton(0))
        {
            float x = Input.GetAxis("Mouse X") * panSpeed;
            float y = Input.GetAxis("Mouse Y") * panSpeed;
            transform.Translate(x, y, 0);
        }

        //zoom
        if (Input.GetAxis("Mouse ScrollWheel") > 0 && Camera.main.orthographicSize > minZoom) // forward
        {
            Camera.main.orthographicSize = Math.Max(Camera.main.orthographicSize - 0.5f, minZoom);
        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0 && Camera.main.orthographicSize < maxZoom) // back            
        {
            Camera.main.orthographicSize = Math.Min(Camera.main.orthographicSize + 0.5f, maxZoom);
        }


        //check if camera is out-of-bounds, if so, move back in-bounds
        topRight = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth, Camera.main.pixelHeight, -transform.position.z));
        bottomLeft = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, -transform.position.z));

        if (topRight.x > cameraMaxX)
        {
            transform.position = new Vector3(transform.position.x - (topRight.x - cameraMaxX), transform.position.y, transform.position.z);
        }

        if (topRight.y > cameraMaxY)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - (topRight.y - cameraMaxY), transform.position.z);
        }

        if (bottomLeft.x < cameraMinX)
        {
            transform.position = new Vector3(transform.position.x + (cameraMinX - bottomLeft.x), transform.position.y, transform.position.z);
        }

        if (bottomLeft.y < cameraMinY)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + (cameraMinY - bottomLeft.y), transform.position.z);
        }
    }
}