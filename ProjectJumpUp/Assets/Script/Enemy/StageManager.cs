using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public int currentStage; //  �׽�Ʈ�� 1�������� ����

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
