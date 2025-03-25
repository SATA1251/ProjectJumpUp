using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public int currentStage; //  테스트용 1스테이지 고정

    private bool StageChange = false;

    private void Start()
    {
        currentStage = GameSceneManager.instance.currentStageNum;
    }

    public int GetStageNum()
    {
        return currentStage;
    }

}
