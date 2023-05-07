using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayerScript : MonoBehaviour
{
    public GameObject MainCamera;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MainCamera.transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 1.365f , -10f);
    }
}
