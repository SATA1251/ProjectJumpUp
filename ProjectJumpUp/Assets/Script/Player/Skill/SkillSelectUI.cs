using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillSelectUI : MonoBehaviour
{
    public Button[] skillSlotButtons;
    public GameObject skillListUI;
    public Button[] skillButtons; // ���� ���� ��ų ��� ��ư
    public SkillList[] availableSkills;

    private int selectedSlotIndex = -1;

    void Start()
    {
        // ���� ��ư Ŭ�� �̺�Ʈ �߰�
        for (int i = 0; i < skillSlotButtons.Length; i++)
        {
            int index = i;
            skillSlotButtons[i].onClick.AddListener(() => OpenSkillList(index));
        }

        // ��ų ����Ʈ ���� ��ư Ŭ�� �̺�Ʈ �߰�
        for (int i = 0; i < skillButtons.Length; i++)
        {
            int index = i;
            skillButtons[i].onClick.AddListener(() => SelectSkill(index));
        }

        skillListUI.SetActive(false); // ó������ ��ų ����Ʈ ��Ȱ��ȭ
    }

    // Ư�� ������ ��ġ�ϸ� ��ų ����Ʈ UI ǥ��
    void OpenSkillList(int slotIndex)
    {
        selectedSlotIndex = slotIndex;
        skillListUI.SetActive(true);
    }

    // ��ų�� �����ϸ� �ش� ���Կ� �����ϰ� UI �ݱ�
    void SelectSkill(int skillIndex)
    {
        if (selectedSlotIndex == -1)
            return;   

        SkillManager.Instance.AssignSkill(selectedSlotIndex, availableSkills[skillIndex]);
        Debug.Log($"Slot {selectedSlotIndex}�� {availableSkills[skillIndex]} ��ų ����");

        skillListUI.SetActive(false); // ��ų ���� �� UI �ݱ�
    }

    // ���� ������ �̵�
    public void StartGame()
    {
        Scene.instance.LoadReadGameScene();
    }
}
