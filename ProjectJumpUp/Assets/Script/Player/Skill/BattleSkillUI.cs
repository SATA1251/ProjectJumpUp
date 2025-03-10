using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleSkillUI : MonoBehaviour
{
    public Button[] skillUseButtons; // ��ų ��� ��ư
    public Image[] skillIcons; // ��ų ������
    public Sprite[] skillSprites; // ��ų�� ������
    private SkillLibrary skillLibrary; // ��ų�� ������ ���̺귯��

    void Start()
    {
        skillLibrary = FindObjectOfType<SkillLibrary>(); // SkillLibrary ã��
        UpdateSkillUI();

        for (int i = 0; i < skillUseButtons.Length; i++)
        {
            int index = i;
            skillUseButtons[i].onClick.AddListener(() => UseSkill(index));
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
        SkillList skillType = SkillManager.Instance.selectedSkills[slotIndex];
        ISkill skillInstance = skillLibrary.GetSkill(skillType); // ��ų ��������

        if (skillInstance != null)
        {
            Debug.Log($"����� ��ų: {skillType}");
            skillInstance.Activate(); // ��ų ����
        }
        else
        {
            Debug.LogWarning($"��ų {skillType}��(��) SkillLibrary�� ����!");
        }
    }
}
