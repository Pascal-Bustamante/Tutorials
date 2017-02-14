using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingEntity : MonoBehaviour, IDamageable {

    public float startingHealth;
    protected float health;
    protected bool dead;

    public event System.Action OnDeath;

    protected virtual void Start()
    {
        health = startingHealth;
    }

    public void TakeHit(float damage, RaycastHit hit)
    {
        //this will be used to implement particle effects
        TakeDamage(damage);
    }

    public void TakeDamage (float damage)
    {
        health -= damage;
        if (health <= 0 && !dead)
        {
            Die();
        }
    }

    protected void Die ()
    {
        dead = true;
        if (OnDeath != null)
        {
            OnDeath();
        }
        GameObject.Destroy(gameObject);
    }
}
