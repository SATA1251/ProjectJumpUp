using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleSkillUI : MonoBehaviour
{
    public Button[] skillUseButtons; // 스킬 사용 버튼
    public Image[] skillIcons; // 스킬 아이콘
    public Sprite[] skillSprites; // 스킬별 아이콘
    public float[] skillCooldowns; // 각 스킬의 쿨타임
    private SkillLibrary skillLibrary; // 스킬을 가져올 라이브러리

    private Dictionary<int, float> cooldownTimers = new Dictionary<int, float>(); // 스킬별 타이머

    private List<ISkill> passiveSkills = new List<ISkill>();

    void Start()
    {
        skillLibrary = FindObjectOfType<SkillLibrary>(); // SkillLibrary 찾기
        UpdateSkillUI();

        for (int i = 0; i < skillUseButtons.Length; i++)
        {
            int index = i;
            skillUseButtons[i].onClick.AddListener(() => UseSkill(index)); // 터치로 전환 필요
            cooldownTimers[index] = 0f;

            ISkill skill = skillLibrary.GetSkill(SkillManager.Instance.selectedSkills[i]);

            if (skill != null)
            {
                if (skill.IsPassive)
                {
                    skillUseButtons[i].interactable = false; // 패시브면 버튼 비활성화
                    passiveSkills.Add(skill); // 등록
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
        //    Debug.Log($"스킬 {SkillManager.Instance.selectedSkills[slotIndex]} 사용 불가! (쿨타임 진행 중)");
        //    return;
        //}

        SkillList skillType = SkillManager.Instance.selectedSkills[slotIndex];
        ISkill skillInstance = skillLibrary.GetSkill(skillType); // 스킬 가져오기

        if (skillInstance != null)
        {
            Debug.Log($"사용한 스킬: {skillType}");
            skillInstance.Activate(); // 스킬 실행

            //// 쿨타임 적용
            //cooldownTimers[slotIndex] = skillCooldowns[slotIndex];
            //skillUseButtons[slotIndex].interactable = false;
            //StartCoroutine(CooldownRoutine(slotIndex));
        }
        else
        {
            Debug.LogWarning($"스킬 {skillType}이(가) SkillLibrary에 없음!");
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
