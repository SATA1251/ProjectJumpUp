using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISkill
{
    void Activate();
}

public class SkillLibrary : MonoBehaviour
{
    private GameObject player;
    private GameObject playerPlatform;
    private Transform playerTransform;

    private Dictionary<SkillList, ISkill> skillDictionary;

    private void Awake()
    {
        skillDictionary = new Dictionary<SkillList, ISkill>
        {
            { SkillList.CreatePlatform, new CreatePlatform() },
            { SkillList.Spider_p, new Spider_p() },
            { SkillList.Invincible, new Invincible() },
            { SkillList.DoubleJump_p, new DoubleJump_p() },
            { SkillList.Headbutt, new Headbutt() },
            { SkillList.Teleport, new Teleport() },
            { SkillList.Rest, new Rest() },
            { SkillList.Unbreak_p, new Unbreak_p() },
            { SkillList.BottomJump_p, new BottomJump_p() }
        };
    }

    private void Start()
    {
        player = GameObject.Find("Player");
        playerTransform = player.GetComponent<Transform>();
        playerPlatform = GameObject.Find("PlayerPlatform");
    }

    public ISkill GetSkill(SkillList skill)
    {
        return skillDictionary.TryGetValue(skill, out ISkill skillInstance) ? skillInstance : null;
    }

    private abstract class SkillBase : ISkill
    {
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

        protected abstract void UseSkill(); // 각 스킬마다 다르게 구현

    }

    private class CreatePlatform : SkillBase
    {
        public CreatePlatform() { cooldownTime = 5f; } // 5초 쿨타임

        protected override void UseSkill()
        {
            Debug.Log("플랫폼 생성 스킬 사용!");
            // playerPlatform.GetComponent<PlayerPlatform>().Respawn();
        }
    }

    private class Spider_p : SkillBase
    {
        public Spider_p() { cooldownTime = 3f; }

        protected override void UseSkill() { Debug.Log("거미 스킬 사용!"); }
    }
    private class Invincible : SkillBase
    {
        public Invincible() { cooldownTime = 3f; }

        protected override void UseSkill() { Debug.Log("무적 스킬 사용!"); }
    }

    private class DoubleJump_p : SkillBase
    {
        public DoubleJump_p() { cooldownTime = 3f; }

        protected override void UseSkill() { Debug.Log("더블 점프 스킬 사용!"); }
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
