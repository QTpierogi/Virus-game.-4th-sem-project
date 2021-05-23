using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public int health;
    private BoxCollider2D enemy_box;
    public float dazeTime;
    public float startDazeTime;

    private Animator enemy_animator;

    protected virtual void Awake()
    {
        enemy_box = GetComponent<BoxCollider2D>();
        enemy_animator = GetComponent<Animator>();
    }

    protected virtual void Update()
    {
        
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Health.Instance.GetDamage();
        }
    }

    public void GetDamage(int dealtDamage)
    {
        dazeTime = startDazeTime;
        enemy_animator.SetTrigger("EnemyDamaged");
        health -= dealtDamage;
        if (health < 1)
            Die();
    }

    protected void Die()
    {
        Destroy(this.gameObject);
    }
}
