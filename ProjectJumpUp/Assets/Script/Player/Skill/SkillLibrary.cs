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

    // ��ų ���� ������Ʈ üũ
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
                Debug.Log($"[{this.GetType().Name}] ��ų ��Ÿ�� ������, �����ð� : {lastUsedTime + cooldownTime - Time.time:F1}��");
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
                Debug.Log($"[{this.GetType().Name}] �нú� ��Ÿ�� ������, �����ð� : {lastUsedTime + cooldownTime - Time.time:F1}��");
            }
        }

        protected virtual void UseSkill()
        {

        }
        // �� ��ų���� �ٸ��� ����
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
        } // 5�� ��Ÿ��

        protected override void UseSkill()
        {
            Debug.Log("�÷��� ���� ��ų ���!");
            library.playerPlatform.transform.position = library.player.transform.position + new Vector3(0, -1f, 0);
            library.playerPlatform.GetComponent<PlayerPlatform>().Respawn();
        }
    }

    private class Invincible : SkillBase
    {
        public Invincible() { cooldownTime = 3f; }

        protected override void UseSkill() { Debug.Log("���� ��ų ���!"); }
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

    private class Spider_p : SkillBase
    {
        public override bool IsPassive => true; // �нú� ��ų �Ǵ�
        private SkillLibrary library;

        public Spider_p(SkillLibrary lib)
        {
            library = lib;
            cooldownTime = 0.0f;
        }

        protected override void PassiveSkill()
        {
            Debug.Log("�Ź� ��ų �����!");

            library.player.GetComponent<PlayerController>().passiveSpiderAct = true;

        }

        protected override void UnPassiveSkill()
        {
            Debug.Log("�Ź� ��ų ��Ÿ����");

            library.player.GetComponent<PlayerController>().passiveSpiderAct = false;

        }
    }

    private class DoubleJump_p : SkillBase
    {
        public DoubleJump_p() { cooldownTime = 3f; }

        protected override void UseSkill() { Debug.Log("���� ���� ��ų ���!"); }
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
