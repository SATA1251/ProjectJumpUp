using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    private int currentStage = 1; //  �׽�Ʈ�� 1�������� ����

    private bool StageChange = false;

    public int SetStageNum(int stageNum) // �������� ���� ���°� �ƴ϶�� 0�� ��ȯ�Ұ�
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
