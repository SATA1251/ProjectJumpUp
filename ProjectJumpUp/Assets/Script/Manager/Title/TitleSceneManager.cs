using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

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
                if (EventSystem.current.IsPointerOverGameObject())// touch.fingerId))
                {
                    Debug.Log("�ɼ�â ����");
                    return;
                }

                Scene.instance.LoadStageSelectScene();

            }
        }
        // �� ����� ����� ȯ�濡���� �۵��ϴ� �ڵ��̱⿡ �׽�Ʈ �߿��� �ּ�ó�� �ߴ�.

        // ���콺 Ŭ�� ó��
        //if (Input.GetMouseButtonDown(0))
        //{
        //    // UI ��� Ŭ�� �� ���� ó��
        //    if (EventSystem.current.IsPointerOverGameObject())
        //    {
        //        Debug.Log("���콺 UI Ŭ�� ���� - ���� ó����");
        //        return;
        //    }

        //    // ���콺 Ŭ���� UI ��Ұ� �ƴ� ������ �߻��ϸ� �� ��ȯ
        //    Scene.instance.LoadStageSelectScene();
        //}
    }
}
