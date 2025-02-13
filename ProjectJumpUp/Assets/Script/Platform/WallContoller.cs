using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallContoller : MonoBehaviour
{

    float cameraSize;
    // Start is called before the first frame update
    void Start()
    {
        cameraSize = Camera.main.orthographicSize;

        if (gameObject.CompareTag("Wall_L"))
        {
            transform.position = new Vector3(-(cameraSize / 2 + 0.3f), 5, 0);
        }
        else if (gameObject.CompareTag("Wall_R"))
        {
            transform.position = new Vector3((cameraSize / 2 + 0.3f), 5, 0);
        }


    }

}
