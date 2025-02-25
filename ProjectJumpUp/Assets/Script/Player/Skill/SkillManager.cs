using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SkillManager : MonoBehaviour
{

    public SkillList[] skillSlots = new SkillList[3];
    public Button[] skillUseButtons;
    public Image[] skillIcons;
    public Sprite[] skillSprites;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < skillUseButtons.Length; i++)
        {
            int index = i; // 클로저 문제 방지
            skillUseButtons[i].onClick.AddListener(() => ActivateSkill(index));
            UpdateSkillUI();
        }
    }

    public void AssignSkill(int slotIndex, SkillList skill)
    {
        if (slotIndex < 0 || slotIndex >= skillSlots.Length)
        {
            return;
        }

        skillSlots[slotIndex] = skill;
        Debug.Log($"Slot {slotIndex} assigned to {skill}");
        UpdateSkillUI();
    }

    public void ActivateSkill (int slotIndex)
    {
        if (slotIndex < 0 || slotIndex >= skillSlots.Length)
            return;

        SkillList skill = skillSlots[slotIndex];
        Debug.Log($"Using skill: {skill}");

        switch (skill)
        {

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void UpdateSkillUI()
    {
        for (int i = 0; i < skillSlots.Length; i++)
        {
            skillIcons[i].sprite = skillSprites[(int)skillSlots[i]]; // 아이콘 변경
        }
    }
}
