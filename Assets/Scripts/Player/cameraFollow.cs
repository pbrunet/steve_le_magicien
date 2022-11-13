using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class cameraFollow : MonoBehaviour
{
    [SerializeField] private Camera camera;
    [SerializeField] private float smoothTime = 0.3F;
    private Vector3 velocity = Vector3.zero;
    [SerializeField] private float offsetSpeed = 5f;
    private float accumulatedVelocity = 0f;
    private bool dir = false;

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
        if(gameObject.GetComponent<Rigidbody2D>().velocity.x > 0)
        {
            if (!dir)
            {
                accumulatedVelocity = 0;
            }
            dir = true;
            accumulatedVelocity += Time.fixedDeltaTime;
        }
        else if (gameObject.GetComponent<Rigidbody2D>().velocity.x < 0)
        {
            if(dir)
            {
                accumulatedVelocity = 0;
            }
            dir = false;
            accumulatedVelocity += Time.fixedDeltaTime;
        }
        // Define a target position above and behind the target transform
        Vector3 targetPosition = gameObject.transform.TransformPoint(new Vector3(Mathf.Min(accumulatedVelocity * offsetSpeed, 30), 0, 0));

        // Smoothly move the camera towards that target position
        float z = camera.transform.position.z;
        Vector3 newPos = Vector3.SmoothDamp(camera.transform.position, targetPosition, ref velocity, smoothTime, Mathf.Infinity, Time.fixedDeltaTime);
        camera.transform.position = new Vector3(newPos.x, newPos.y, z);
    }
}
