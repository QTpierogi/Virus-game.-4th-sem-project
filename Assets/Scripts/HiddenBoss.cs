using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiddenBoss : MonoBehaviour
{
    private BossMonocite boss;
    private Transform bossPosition;
    private int speed = 10;

    void Start()
    {
        boss = GameObject.FindObjectOfType<BossMonocite>();
        bossPosition = boss.transform;
    }

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, new Vector2(bossPosition.position.x, transform.position.y), speed * Time.deltaTime);
        if (boss.unhidden)
            Destroy(this.gameObject);
    }
}
