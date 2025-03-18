using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : MonoBehaviour
{
    private float HP = 100f;
    public float attack = 5;

    public void PlayerDamage(float damage)
    {
        HP -= damage;
    }

    public void PlayerHeal(float heal)
    {
        HP += heal;
    }
}
