using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedPlatformManager : MonoBehaviour
{

    // 고정 채보를 싱글턴으로 미리 세팅
    public static FixedPlatformManager Instance { get; private set; }

    public List<Vector2> fixedPlatformPosition_Stage_1 = new List<Vector2>
    {
        new Vector2 (1,1)
    };

    public List<Vector2> monsterPlatformPosition_Stage_1 = new List<Vector2>
    {
        new Vector2(2,2)
    };




    public List<Vector2> platformPosition_Stage_2;
    public List<Vector2> platformPosition_Stage_3;
    public List<Vector2> platformPosition_Stage_4;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
