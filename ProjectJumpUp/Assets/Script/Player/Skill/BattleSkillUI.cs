using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleSkillUI : MonoBehaviour
{
    public Button[] skillUseButtons; // 스킬 사용 버튼
    public Image[] skillIcons; // 스킬 아이콘
    public Sprite[] skillSprites; // 스킬별 아이콘
    private SkillLibrary skillLibrary; // 스킬을 가져올 라이브러리

    void Start()
    {
        skillLibrary = FindObjectOfType<SkillLibrary>(); // SkillLibrary 찾기
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
        ISkill skillInstance = skillLibrary.GetSkill(skillType); // 스킬 가져오기

        if (skillInstance != null)
        {
            Debug.Log($"사용한 스킬: {skillType}");
            skillInstance.Activate(); // 스킬 실행
        }
        else
        {
            Debug.LogWarning($"스킬 {skillType}이(가) SkillLibrary에 없음!");
        }
    }
}
