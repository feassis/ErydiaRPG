using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health
{
    private float currentHP;
    private float maxHP;

    public event EventHandler OnDamage;
    public event EventHandler OnDeath;

    public Health(float maxHP)
    {
        this.maxHP = maxHP;
        currentHP = this.maxHP;
    }

    public (float currentHP, float maxHP) GetHealthInfo() => (currentHP, maxHP);

    public float GetNormalisedHP() => currentHP / maxHP;

    public void Damage(float dmg)
    {
        currentHP = Mathf.Clamp(currentHP - dmg, 0f, maxHP);
        OnDamage?.Invoke(this, EventArgs.Empty);

        if(currentHP <= 0)
        {
            OnDeath?.Invoke(this, EventArgs.Empty);
        }
    }
}
