using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    GameObject player;
    Vector3 cameraPosition;
    public float cameaAddPosition_y;

    private float cameraTurnPoint_1 = 2.5f;
    private float cameraTurnPoint_2 = 5.0f;
    private float cameraTurnPoint_3 = 15.0f;

    private StageManager stageManager;

    public int currentNum = 0;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        stageManager = GameObject.Find("StageManager").GetComponent<StageManager>();

        currentNum = stageManager.GetStageNum();
        cameaAddPosition_y = 0;

    }

    // Update is called once per frame
    void Update()
    {
        SetStageCamera();
    }


    void SetStageCamera()
    {
        if (player != null)
        {
            if(player.transform.position.y > cameraTurnPoint_3 && currentNum != 1) // 스테이지 추가시 더 깔끔하게 수정
            {
                cameaAddPosition_y = 20.0f;
            }
            else if (player.transform.position.y > cameraTurnPoint_2)
            {
                cameaAddPosition_y = 10.0f;
            }
            else if (player.transform.position.y > cameraTurnPoint_1)
            {
                cameaAddPosition_y = 0.0f;
            }
            cameraPosition = new Vector3(transform.position.x, cameaAddPosition_y, -10);
        }

        transform.position = cameraPosition;
    }
}
