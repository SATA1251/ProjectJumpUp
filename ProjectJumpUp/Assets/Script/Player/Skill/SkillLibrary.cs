using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISkill
{
    bool IsPassive { get; }
    void Activate();
    void UsingPassive();
}

public class SkillLibrary : MonoBehaviour
{
    private GameObject player;
    private Transform playerTransform;

    // 스킬 관련 오브젝트 체크
    private GameObject playerPlatform;
    private GameObject wallObject_R;
    private GameObject wallObject_L;

    private Dictionary<SkillList, ISkill> skillDictionary;

    private void Awake()
    {
        skillDictionary = new Dictionary<SkillList, ISkill>
        {
            { SkillList.CreatePlatform, new CreatePlatform(this) },
            { SkillList.Invincible, new Invincible() },
            { SkillList.Headbutt, new Headbutt() },
            { SkillList.Teleport, new Teleport() },
            { SkillList.Rest, new Rest() },
            { SkillList.Spider_p, new Spider_p(this) },
            { SkillList.DoubleJump_p, new DoubleJump_p() },
            { SkillList.Unbreak_p, new Unbreak_p() },
            { SkillList.BottomJump_p, new BottomJump_p() }
        };
    }

    private void Start()
    {
        player = GameObject.Find("Player");
        playerTransform = player.GetComponent<Transform>();
        playerPlatform = GameObject.Find("PlayerPlatform");
        wallObject_R = GameObject.Find("Wall_R");
        wallObject_L = GameObject.Find("Wall_L");

    }

    public ISkill GetSkill(SkillList skill)
    {
        return skillDictionary.TryGetValue(skill, out ISkill skillInstance) ? skillInstance : null;
    }

    private abstract class SkillBase : ISkill
    {
        public virtual bool IsPassive => false;

        protected float cooldownTime;
        private float lastUsedTime = -9999f;

        public bool IsAvailable()
        {
            return Time.time >= lastUsedTime + cooldownTime;
        }

        public void Activate()
        {
            if(IsAvailable())
            {
                lastUsedTime = Time.time;
                UseSkill();
            }
            else
            {
                Debug.Log($"[{this.GetType().Name}] 스킬 쿨타임 진행중, 남은시간 : {lastUsedTime + cooldownTime - Time.time:F1}초");
            }
        }

        public void UsingPassive()
        {
            if (IsAvailable())
            {
                lastUsedTime = Time.time;
                PassiveSkill();
            }
            else
            {
                UnPassiveSkill();
                Debug.Log($"[{this.GetType().Name}] 패시브 쿨타임 진행중, 남은시간 : {lastUsedTime + cooldownTime - Time.time:F1}초");
            }
        }

        protected virtual void UseSkill()
        {

        }
        // 각 스킬마다 다르게 구현
        protected virtual void PassiveSkill()
        {

        }

        protected virtual void UnPassiveSkill()
        {

        }

    }

    private class CreatePlatform : SkillBase
    {
        private SkillLibrary library;

        public CreatePlatform(SkillLibrary lib)
        { 
            cooldownTime = 5f;
            library = lib;
        } // 5초 쿨타임

        protected override void UseSkill()
        {
            Debug.Log("플랫폼 생성 스킬 사용!");
            library.playerPlatform.transform.position = library.player.transform.position + new Vector3(0, -1f, 0);
            library.playerPlatform.GetComponent<PlayerPlatform>().Respawn();
        }
    }

    private class Invincible : SkillBase
    {
        public Invincible() { cooldownTime = 3f; }

        protected override void UseSkill() { Debug.Log("무적 스킬 사용!"); }
    }

    private class Headbutt : SkillBase
    {
        public Headbutt() { cooldownTime = 3f; }

        protected override void UseSkill() { Debug.Log("헤드벗 스킬 사용!"); }
    }

    private class Teleport : SkillBase
    {
        public Teleport() { cooldownTime = 3f; }

        protected override void UseSkill() { Debug.Log("텔레포트 스킬 사용!"); }
    }

    private class Rest : SkillBase
    {
        public Rest() { cooldownTime = 3f; }

        protected override void UseSkill() { Debug.Log("휴식 스킬 사용!"); }
    }

    private class Spider_p : SkillBase
    {
        public override bool IsPassive => true; // 패시브 스킬 판단
        private SkillLibrary library;

        public Spider_p(SkillLibrary lib)
        {
            library = lib;
            cooldownTime = 0.0f;
        }

        protected override void PassiveSkill()
        {
            Debug.Log("거미 스킬 사용중!");

            library.player.GetComponent<PlayerController>().passiveSpiderAct = true;

        }

        protected override void UnPassiveSkill()
        {
            Debug.Log("거미 스킬 쿨타임중");

            library.player.GetComponent<PlayerController>().passiveSpiderAct = false;

        }
    }

    private class DoubleJump_p : SkillBase
    {
        public DoubleJump_p() { cooldownTime = 3f; }

        protected override void UseSkill() { Debug.Log("더블 점프 스킬 사용!"); }
    }

    private class Unbreak_p : SkillBase
    {
        public Unbreak_p() { cooldownTime = 3f; }

        protected override void UseSkill() { Debug.Log("부서지지 않는 스킬 사용!"); }
    }

    private class BottomJump_p : SkillBase
    {
        public BottomJump_p() { cooldownTime = 3f; }

        protected override void UseSkill() { Debug.Log("바닥 점프 스킬 사용!"); }
    }
}
