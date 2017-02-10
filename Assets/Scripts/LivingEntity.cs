using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingEntity : MonoBehaviour, IDamageable {

    public float startingHealth;
    protected float health;
    protected bool dead;

    public virtual void Start()
    {
        health = startingHealth;
    }

    public void TakeHit(float damage, RaycastHit hit)
    {
        health -= damage;
        if (health <= 0 && !dead)
        {
            Die();
        }
    }
    public void Die ()
    {
        dead = true;
        GameObject.Destroy(gameObject);
    }
}
