using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    private int currentStage = 1; //  테스트용 1스테이지 고정

    private bool StageChange = false;

    public int SetStageNum(int stageNum) // 스테이지 진입 상태가 아니라면 0을 반환할것
    {
        currentStage = stageNum;
        return currentStage;
    }


    public int GetStageNum()
    {
        return currentStage;
    }
    //public void SetStage()
    //{
    //    switch (currentStage)
    //    {
    //        case 1:
              
    //            break;
    //    }
    //}
}
