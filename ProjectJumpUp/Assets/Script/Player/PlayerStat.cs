using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : MonoBehaviour
{
    private float maxHP = 100f;
    private float currentHP;
    public float attack = 5;

    void Start()
    {
        currentHP = maxHP;
    }

    public void PlayerDamage(float damage)
    {
        currentHP -= damage;

        if (currentHP <= 0)
        {
            Die();
        }
    }

    public void PlayerHeal(float heal)
    {
        currentHP += heal;

        if (currentHP >= 100.0f)
        {
            currentHP = 100.0f;
        }
    }

    void Die()
    {
        Debug.Log("�÷��̾� ���!");
        GameOver();
    }

    void GameOver()
    {
        Time.timeScale = 0f; // ���� ����
       // if (gameOverUI != null)
       //     gameOverUI.SetActive(true); // ���ӿ��� UI ǥ��
    }
}
