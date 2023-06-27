using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public event EventHandler OnDead;
    public event EventHandler OnDamaged;
    public event EventHandler OnHealed;

    [SerializeField] private int healthAmountMax = 3;
    private int healthAmount;
    private bool dead = false;

    private void Awake()
    {
        healthAmount = healthAmountMax;
    }

    public void Damage(int damageAmount)
    {
        if (IsDead()) return;

        healthAmount -= damageAmount;

        if (healthAmount < 0)
        {
            healthAmount = 0;
        }

        OnDamaged?.Invoke(this, EventArgs.Empty);

        if (healthAmount == 0)
        {
            Die();
        }
    }

    public void Heal(int healAmount)
    {
        healthAmount += healAmount;

        if (healthAmount > healthAmountMax)
        {
            healthAmount = healthAmountMax;
        }

        OnHealed?.Invoke(this, EventArgs.Empty);
    }

    private void Die()
    {
        OnDead?.Invoke(this, EventArgs.Empty);

        dead = true;

        GameOverUI.Instance.Show();
    }

    public bool IsDead()
    {
        return dead;
    }

    public int GetHealthAmount()
    {
        return healthAmount;
    }
}
