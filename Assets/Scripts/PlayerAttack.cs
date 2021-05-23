using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private float attackCooldown;
    public float attackCooldownValue = 0.1f;

    public Transform attackPos;
    public Animator player_animator;
    public LayerMask whatIsEnemy;
    public float attackRangeX = 0.4f;
    public float attackRangeY = 0.2f;
    public int damage = 1;

    void Start()
    {
        player_animator = GetComponent<Animator>();
        attackCooldown = 0f;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && attackCooldown <=0)
        {
            player_animator.SetTrigger("PlayerAttacked");
            Collider2D[] enemiesToDamage = Physics2D.OverlapBoxAll(attackPos.position, new Vector2(attackRangeX, attackRangeY), 0, whatIsEnemy);
            for (int i = 0; i < enemiesToDamage.Length; i++)
                enemiesToDamage[i].GetComponent<Enemy>().GetDamage(damage);
            attackCooldown = attackCooldownValue;
        }
        else attackCooldown -= Time.deltaTime;
    }
}
