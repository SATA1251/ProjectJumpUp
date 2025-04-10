using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleSkillUI : MonoBehaviour
{
    public Button[] skillUseButtons; // ��ų ��� ��ư
    public Image[] skillIcons; // ��ų ������
    public Sprite[] skillSprites; // ��ų�� ������
    public float[] skillCooldowns; // �� ��ų�� ��Ÿ��
    private SkillLibrary skillLibrary; // ��ų�� ������ ���̺귯��

    private Dictionary<int, float> cooldownTimers = new Dictionary<int, float>(); // ��ų�� Ÿ�̸�

    private List<ISkill> passiveSkills = new List<ISkill>();

    void Start()
    {
        skillLibrary = FindObjectOfType<SkillLibrary>(); // SkillLibrary ã��
        UpdateSkillUI();

        for (int i = 0; i < skillUseButtons.Length; i++)
        {
            int index = i;
            skillUseButtons[i].onClick.AddListener(() => UseSkill(index)); // ��ġ�� ��ȯ �ʿ�
            cooldownTimers[index] = 0f;

            ISkill skill = skillLibrary.GetSkill(SkillManager.Instance.selectedSkills[i]);

            if (skill != null)
            {
                if (skill.IsPassive)
                {
                    skillUseButtons[i].interactable = false; // �нú�� ��ư ��Ȱ��ȭ
                    passiveSkills.Add(skill); // ���
                }
                else
                {
                    skillUseButtons[i].interactable = true;
                }
            }
        }
    }

    void Update()
    {
        foreach (var skill in passiveSkills)
        {
            skill.UsingPassive();
        }
    }

    void UpdateSkillUI()
    {
        for (int i = 0; i < SkillManager.Instance.selectedSkills.Length; i++)
        {
            skillIcons[i].sprite = skillSprites[(int)SkillManager.Instance.selectedSkills[i]];
        }
    }

    void UseSkill(int slotIndex)
    {
        //if (cooldownTimers[slotIndex] > 0)
        //{
        //    Debug.Log($"��ų {SkillManager.Instance.selectedSkills[slotIndex]} ��� �Ұ�! (��Ÿ�� ���� ��)");
        //    return;
        //}

        SkillList skillType = SkillManager.Instance.selectedSkills[slotIndex];
        ISkill skillInstance = skillLibrary.GetSkill(skillType); // ��ų ��������

        if (skillInstance != null)
        {
            Debug.Log($"����� ��ų: {skillType}");
            skillInstance.Activate(); // ��ų ����

            //// ��Ÿ�� ����
            //cooldownTimers[slotIndex] = skillCooldowns[slotIndex];
            //skillUseButtons[slotIndex].interactable = false;
            //StartCoroutine(CooldownRoutine(slotIndex));
        }
        else
        {
            Debug.LogWarning($"��ų {skillType}��(��) SkillLibrary�� ����!");
        }
    }

    //void UpdateSkillCooldowns()
    //{
    //    for(int i = 0; i < skillUseButtons.Length; i++)
    //    {
    //        if(cooldownTimers[i] > 0)
    //        {
    //            cooldownTimers[i] -= Time.deltaTime;
    //            if(cooldownTimers[i] <= 0)
    //            {
    //                cooldownTimers[i] = 0;
    //                skillUseButtons[i].interactable = true;
    //            }
    //        }
    //    }
            
    //}

    //IEnumerator CooldownRoutine(int slotIndex)
    //{
    //    float cooldown = skillCooldowns[slotIndex];
    //    float elapsed = 0f;
    //    Image buttonImage = skillUseButtons[slotIndex].image;

    //    while (elapsed < cooldown)
    //    {
    //        elapsed += Time.deltaTime;
    //        float alpha = Mathf.Lerp(0.5f, 1f, elapsed / cooldown);
    //        buttonImage.color = new Color(1f, 1f, 1f, alpha);
    //        yield return null;
    //    }

    //    skillUseButtons[slotIndex].interactable = true;
    //    buttonImage.color = Color.white;
    //}
}
