using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    [SerializeField] private CombatUnit unit;
    [SerializeField] private HPBarGraphics hpBar;

    private Health myHealth;

    private void Awake()
    {
        myHealth = new Health(unit.GetMaxHP());

        myHealth.OnDamage += Health_OnDamage;
    }

    private void Health_OnDamage(object sender, EventArgs e)
    {
        UpdateHPBar();
    }

    private void UpdateHPBar()
    {
        hpBar.UpdateHPBar(myHealth.GetNormalisedHP());
    }
}
