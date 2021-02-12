using UnityEngine;
using UnityEngine.Events;
using System;

public class HealthSystem : MonoBehaviour {

    [SerializeField] private float maxHealth;
    [SerializeField] private HealthBar healthBar;

    [Header("Events")]
    [SerializeField] private UnityEvent OnHeal;
    [SerializeField] private UnityEvent OnHit;
    [SerializeField] private UnityEvent OnDead;
    private float health;

    private void Start () {
        health = maxHealth;
    }
    public float GetHealth () {
        return health;
    }

    public void TakeDamage (float damage, Action onDamageTaken, Action onDead) {
        health -= damage;
        onDamageTaken?.Invoke();
        OnHit?.Invoke();
        if (health <= 0) {
            onDead?.Invoke();
            OnDead?.Invoke();
        }
        ClampHealth();
        UpdateHealthBar(true);
    }
    
    public void TakeDamage (float damage, Action onDamageTaken) {
        health -= damage;
        IsDead();
        onDamageTaken?.Invoke();
        OnHit?.Invoke();
        ClampHealth();
        UpdateHealthBar(true);
    }
    
    public void TakeDamage (float damage) {
        health -= damage;
        IsDead();
        OnHit?.Invoke();
        ClampHealth();
        UpdateHealthBar(true);
    }

    public void TakeHeal (float heal, Action onHeal) {
        health += heal;
        onHeal?.Invoke();
        OnHeal?.Invoke();
        ClampHealth();
        UpdateHealthBar(false);
    } 
    
    public void TakeHeal (float heal) {
        health += heal;
        OnHeal?.Invoke();
        ClampHealth();
        UpdateHealthBar(false);
    }

    public void IsDead () {
        if (health <= 0) {
            OnDead?.Invoke();
        }
    }

    public void UpdateHealthBar (bool decrease) {
        if (healthBar == null) return;
        healthBar.UpdateHealthBar(GetHealth(), maxHealth, decrease);
    }

    public void ClampHealth () {
        health = Mathf.Clamp(health, 0, maxHealth);
    }

    public float GetHealthNormalized () {
        return health / maxHealth;
    }

}
