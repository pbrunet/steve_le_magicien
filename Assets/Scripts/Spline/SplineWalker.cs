using UnityEngine;

public delegate void EndSplineReachedHandler(GameObject obj);
public class SplineWalker : MonoBehaviour
{
    public enum SplineWalkerMode
    {
        Once,
        Loop,
        PingPong
    }

    public BezierSpline spline;
    [SerializeField] private float speed;
    [SerializeField] private bool lookForward;

    [SerializeField] private SplineWalkerMode mode;
    public event EndSplineReachedHandler OnEndSplineReached;

    private Vector3 offset;
    private float progress;
    private bool goingForward = true;

    private void Start()
    {
        offset = spline.GetPoint(0) - transform.position;
    }

    private void Update()
    {
        if (goingForward)
        {
            progress += speed * Time.deltaTime / spline.GetVelocity(progress).magnitude;
            if (progress > 1f)
            {
                if (mode == SplineWalkerMode.Once)
                {
                    progress = 1f;
                }
                else if (mode == SplineWalkerMode.Loop)
                {
                    progress -= 1f;
                }
                else
                {
                    progress = 2f - progress;
                    goingForward = false;
                }
                if (OnEndSplineReached != null)
                {
                    OnEndSplineReached(gameObject);
                }
            }
        }
        else
        {
            progress -= speed * Time.deltaTime / spline.GetVelocity(progress).magnitude;
            if (progress < 0f)
            {
                progress = -progress;
                goingForward = true;

                if (OnEndSplineReached != null)
                {
                    OnEndSplineReached(gameObject);
                }
            }
        }

        Vector3 position = spline.GetPoint(progress);
        transform.position = position + offset;
        if (lookForward)
        {
            transform.LookAt(position + spline.GetDirection(progress));
        }
    }
}