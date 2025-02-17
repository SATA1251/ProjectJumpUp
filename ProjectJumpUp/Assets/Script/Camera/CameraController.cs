using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    GameObject player;
    Vector3 cameraPosition;
   public float cameaAddPosition_y;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        cameaAddPosition_y = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(player != null)
        {
            // 2.5 , 5 , 7.5  // 5
            //if (player.transform.position.y > 7.5f)
            //{
            //    cameaAddPosition_y = 10.0f;
            //}
            if (player.transform.position.y > 5.0f)
            {
                //cameaAddPosition_y = 5.0f;
                cameaAddPosition_y = 10.0f;
            }
            else if (player.transform.position.y > 2.5f)
            {
                cameaAddPosition_y = 0.0f;
            }
            cameraPosition = new Vector3(transform.position.x, cameaAddPosition_y, -10);
        }

        transform.position = cameraPosition;
    }
}
