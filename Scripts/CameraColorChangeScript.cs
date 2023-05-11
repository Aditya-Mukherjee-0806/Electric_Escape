using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraColorChangeScript : MonoBehaviour
{
    public GameObject MainCamera;
    public Color skyColor;
    public Color groundColor;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(MainCamera.transform.position.y > 0)
        {
            MainCamera.GetComponent<Camera>().backgroundColor = skyColor;
        }
        else
        {
            MainCamera.GetComponent<Camera>().backgroundColor = groundColor;
        }
    }
}
