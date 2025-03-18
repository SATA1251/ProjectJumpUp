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
                Debug.Log($"[{this.GetType().Name}] ��ų ��Ÿ�� ������, �����ð� : {lastUsedTime + cooldownTime - Time.time:F1}��");
            }
        }

        protected abstract void UseSkill(); // �� ��ų���� �ٸ��� ����

    }

    private class CreatePlatform : SkillBase
    {
        public CreatePlatform() { cooldownTime = 5f; } // 5�� ��Ÿ��

        protected override void UseSkill()
        {
            Debug.Log("�÷��� ���� ��ų ���!");
            // playerPlatform.GetComponent<PlayerPlatform>().Respawn();
        }
    }

    private class Spider_p : SkillBase
    {
        public Spider_p() { cooldownTime = 3f; }

        protected override void UseSkill() { Debug.Log("�Ź� ��ų ���!"); }
    }
    private class Invincible : SkillBase
    {
        public Invincible() { cooldownTime = 3f; }

        protected override void UseSkill() { Debug.Log("���� ��ų ���!"); }
    }

    private class DoubleJump_p : SkillBase
    {
        public DoubleJump_p() { cooldownTime = 3f; }

        protected override void UseSkill() { Debug.Log("���� ���� ��ų ���!"); }
    }

    private class Headbutt : SkillBase
    {
        public Headbutt() { cooldownTime = 3f; }

        protected override void UseSkill() { Debug.Log("���� ��ų ���!"); }
    }

    private class Teleport : SkillBase
    {
        public Teleport() { cooldownTime = 3f; }

        protected override void UseSkill() { Debug.Log("�ڷ���Ʈ ��ų ���!"); }
    }

    private class Rest : SkillBase
    {
        public Rest() { cooldownTime = 3f; }

        protected override void UseSkill() { Debug.Log("�޽� ��ų ���!"); }
    }
    private class Unbreak_p : SkillBase
    {
        public Unbreak_p() { cooldownTime = 3f; }

        protected override void UseSkill() { Debug.Log("�μ����� �ʴ� ��ų ���!"); }
    }

    private class BottomJump_p : SkillBase
    {
        public BottomJump_p() { cooldownTime = 3f; }

        protected override void UseSkill() { Debug.Log("�ٴ� ���� ��ų ���!"); }
    }
}
