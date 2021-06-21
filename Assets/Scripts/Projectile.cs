using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10f;
    public Rigidbody2D projectile_rb;

    void Start()
    {
        projectile_rb.velocity = -transform.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Health.Instance.GetDamage();
        }
        if (collision.GetComponent<BossMonocite>() == null)
            Destroy(gameObject);
    }
}
