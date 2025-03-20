using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillSelectUI : MonoBehaviour
{
    public Button[] skillSlotButtons;
    public GameObject skillListUI;
    public Button[] skillButtons; // 선택 가능 스킬 목록 버튼
    public SkillList[] availableSkills;

    private int selectedSlotIndex = -1;

    void Start()
    {
        // 슬롯 버튼 클릭 이벤트 추가
        for (int i = 0; i < skillSlotButtons.Length; i++)
        {
            int index = i;
            skillSlotButtons[i].onClick.AddListener(() => OpenSkillList(index));
        }

        // 스킬 리스트 내의 버튼 클릭 이벤트 추가
        for (int i = 0; i < skillButtons.Length; i++)
        {
            int index = i;
            skillButtons[i].onClick.AddListener(() => SelectSkill(index));
        }

        skillListUI.SetActive(false); // 처음에는 스킬 리스트 비활성화
    }

    // 특정 슬롯을 터치하면 스킬 리스트 UI 표시
    void OpenSkillList(int slotIndex)
    {
        selectedSlotIndex = slotIndex;
        skillListUI.SetActive(true);
    }

    // 스킬을 선택하면 해당 슬롯에 저장하고 UI 닫기
    void SelectSkill(int skillIndex)
    {
        if (selectedSlotIndex == -1)
            return;   

        SkillManager.Instance.AssignSkill(selectedSlotIndex, availableSkills[skillIndex]);
        Debug.Log($"Slot {selectedSlotIndex}에 {availableSkills[skillIndex]} 스킬 선택");

        skillListUI.SetActive(false); // 스킬 선택 후 UI 닫기
    }

    // 다음 씬으로 이동
    public void StartGame()
    {
        Scene.instance.LoadReadGameScene();
    }
}
