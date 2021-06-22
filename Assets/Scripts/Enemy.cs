using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class Enemy : MonoBehaviour
{
    public int health;
    public BoxCollider2D enemy_box;
    public float dazeTime;
    public float startDazeTime;

    public Animator enemy_animator;

    public BossHP healthBar;

    public static Enemy Instance { get; set; }
    public bool bossDead = false;

    void Start()
    {
        Instance = this;

        if (SceneManager.GetActiveScene().name == "BossRoom")
        {
            healthBar.SetMaxHealth(health);
        }
    }

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

        if (SceneManager.GetActiveScene().name == "BossRoom" && health < 1)
        {
            bossDead = true;
        }

        if (SceneManager.GetActiveScene().name == "BossRoom")
        {
            healthBar.SetHealth(health);
        }
    }

    protected void Die()
    {
        Destroy(this.gameObject);
    }
}
