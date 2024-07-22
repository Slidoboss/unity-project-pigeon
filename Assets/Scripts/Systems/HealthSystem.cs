using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem
{
    private int health;
    private int maxHealth;

    public event EventHandler OnHealthChanged;
    public int GetHealth
    {
        get { return health; }
    }
    private bool isAlive = true;
    public HealthSystem(int maxHealth)
    {
        this.maxHealth = maxHealth;
        health = maxHealth;
    }

    public void Damage(int damageAmount)
    {
        health -= damageAmount;
        if (health <= 0)
        {
            health = 0;
            isAlive = false;
        }
        OnHealthChanged?.Invoke(this, EventArgs.Empty);
    }
    public void Heal(int healAmount)
    {
        health += healAmount;
        if (health > maxHealth) health = maxHealth;
        OnHealthChanged?.Invoke(this, EventArgs.Empty);
    }
}
