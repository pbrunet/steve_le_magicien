using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

[System.Serializable]
public class ParallaxDef
{
    public GameObject background;
    public float factor = 10000;
};

public class BackgroundParallax : MonoBehaviour
{
    [SerializeField] private List<ParallaxDef> backgrounds;
    [SerializeField] private Camera camera;

    private List<Vector3> initPos = new List<Vector3>();

    // Start is called before the first frame update
    void Start()
    {
        if (camera == null)
        {
            camera = Camera.main;
        }

        for(int i = 0;i<backgrounds.Count;i++)
        {
            initPos.Add(backgrounds[i].background.transform.position);
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < backgrounds.Count; i++)
        {
            backgrounds[i].background.transform.position = initPos[i] + new Vector3(camera.transform.position.x / backgrounds[i].factor, camera.transform.position.y / backgrounds[i].factor, 0);
        }
    }
}
