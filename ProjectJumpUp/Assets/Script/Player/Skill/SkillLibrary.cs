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

    private class CreatePlatform : ISkill
    {
        public void Activate()
        {
            Debug.Log("�÷��� ���� ��ų ���!");
            // playerPlatform.GetComponent<PlayerPlatform>().Respawn();
        }
    }

    private class Spider_p : ISkill { public void Activate() { Debug.Log("�Ź� ��ų ���!"); } }
    private class Invincible : ISkill { public void Activate() { Debug.Log("���� ��ų ���!"); } }
    private class DoubleJump_p : ISkill { public void Activate() { Debug.Log("���� ���� ��ų ���!"); } }
    private class Headbutt : ISkill { public void Activate() { Debug.Log("���� ��ų ���!"); } }
    private class Teleport : ISkill { public void Activate() { Debug.Log("�ڷ���Ʈ ��ų ���!"); } }
    private class Rest : ISkill { public void Activate() { Debug.Log("�޽� ��ų ���!"); } }
    private class Unbreak_p : ISkill { public void Activate() { Debug.Log("�μ����� �ʴ� ��ų ���!"); } }
    private class BottomJump_p : ISkill { public void Activate() { Debug.Log("�ٴ� ���� ��ų ���!"); } }
}
