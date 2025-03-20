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
                    Debug.Log("옵션창 진입");
                    return;
                }

                Scene.instance.LoadStageSelectScene();

            }
        }
        // 위 방법은 모바일 환경에서만 작동하는 코드이기에 테스트 중에는 주석처리 했다.

        // 마우스 클릭 처리
        //if (Input.GetMouseButtonDown(0))
        //{
        //    // UI 요소 클릭 시 예외 처리
        //    if (EventSystem.current.IsPointerOverGameObject())
        //    {
        //        Debug.Log("마우스 UI 클릭 감지 - 예외 처리됨");
        //        return;
        //    }

        //    // 마우스 클릭이 UI 요소가 아닌 곳에서 발생하면 씬 전환
        //    Scene.instance.LoadStageSelectScene();
        //}
    }
}
