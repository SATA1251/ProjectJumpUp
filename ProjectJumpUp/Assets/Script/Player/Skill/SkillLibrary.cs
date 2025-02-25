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
    
    private void Start()
    {
        player = GameObject.Find("Player");
        playerTransform = player.GetComponent<Transform>();
        playerPlatform = GameObject.Find("PlayerPlatform");
    }

    public SkillLibrary()
    {
        skillDictionary = new Dictionary<SkillList, ISkill>
        {

        };
    }

    public ISkill GetSkill(SkillList skill)
    {
        return skillDictionary.TryGetValue(skill, out ISkill skillInstance) ? skillInstance : null;
    }

    private class CreatePlatform : ISkill
    {
        public void Activate()
        {
            //playerPlatform.GetComponent<PlayerPlatform>().Respawn();
            //playerPlatform.transform = new Vector3(playerTransform.x, playerTransform.x - 5, playerTransform.z);
        }
    }

    private class Spider_p : ISkill
    {
        public void Activate()
        {

        }
    }

    private class Invincible : ISkill
    {
        public void Activate()
        {

        }
    }

    private class DoubleJump_p : ISkill
    {
        public void Activate()
        {

        }
    }

    private class Headbutt : ISkill
    {
        public void Activate()
        {

        }
    }

    private class Teleport : ISkill
    {
        public void Activate()
        {

        }
    }

    private class Rest : ISkill
    {
        public void Activate()
        {

        }
    }

    private class Unbreak_p : ISkill
    {
        public void Activate()
        {

        }
    }

    private class BottomJump_p : ISkill
    {
        public void Activate()
        {

        }
    }
}
