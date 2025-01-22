using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CeilingManager : MonoBehaviour
{
    private float stage1_MaxHp = 100;
    private float stage2_MaxHp = 200;
    private float stage3_MaxHp = 300;

    [SerializeField]
    private float ceil_Hp;

    private int currentStage;

    private float collisionCooldown = 1.0f;

    private bool canTakeDamage = true;

    public StageManager stageManager;

    public GameObject player;
    public PlayerStat playerStat;


    // Start is called before the first frame update
    void Start()
    {
        stageManager = GameObject.Find("StageManager").GetComponent<StageManager>();
        player = GameObject.FindWithTag("Player");
        playerStat = GameObject.FindWithTag("Player").GetComponent<PlayerStat>();
        SetCeilingHP(); // 스테이지 개시시 천장 체력 세팅해주는 함수, 원래 여기 있으면 안된다.
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetCeilingHP()
    {
        switch (stageManager.GetStageNum())
        {
            case 1:
                ceil_Hp = stage1_MaxHp;
                break;
            case 2:
                ceil_Hp = stage2_MaxHp;
                break;
            case 3:
                ceil_Hp = stage3_MaxHp;
                break;
            default:
                break;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player") && canTakeDamage)
        {
            ceil_Hp -= playerStat.attack;
            Debug.Log("천장 공격 성공");
            StartCoroutine(CollisionCooldown());
        }
    }

    private IEnumerator CollisionCooldown()
    {
        canTakeDamage = false;
        yield return new WaitForSeconds(collisionCooldown);
        canTakeDamage = true;
    }
}
