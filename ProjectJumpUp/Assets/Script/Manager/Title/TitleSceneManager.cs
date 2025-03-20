using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleSceneManager : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Ended)
            {
                Scene.instance.LoadStageSelectScene(); // �������� ����â�� ���⿡ �ϴ� �ٷ� �ΰ���
            }               
         }
    }
}
