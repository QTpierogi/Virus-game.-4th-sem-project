using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMonocite : Enemy
{
    private Rigidbody2D rbMonocite;

    public float verticalSpeed;
    public float undergroundSpeed;

    public float maxHeight;
    public float maxDiveDistance;

    private bool faceRight = false;

    private float timer;
    public float idleTime = 5f;

    Transform player;

    //bools for burying
    private bool buried;
    private bool isBurying;
    private bool isUnburying;

    //unburying time variables
    public float unburyTimeValue;
    private float timeBeforeUnburying;

    //shooting variables
    public Transform firePoint;
    public GameObject projectilePrefab;

    //statements bools
    private bool undergroundAttackInProgress = false;
    private bool isShooting = false;
    private bool inIdleMode = false;

    protected override void Awake()
    {
        base.Awake();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rbMonocite = GetComponent<Rigidbody2D>();
    }

    protected override void Update()
    {
        if (undergroundAttackInProgress)
            UndergroundAttack();
        else if (isShooting)
            Shoot();
        else if (inIdleMode)
        {
            Idle();
        }
        else GenerateStatement();
    }

    private void GenerateStatement()
    {
        var statement = Random.Range(0, 2);
        if (statement == 0)
            isShooting = true;
        else undergroundAttackInProgress = true;
    }

    private void UndergroundAttack()
    {
        if (!buried)
        {
            isBurying = true;
            Bury();
        }
        if(!isBurying && !isUnburying)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(player.position.x, transform.position.y), undergroundSpeed * Time.deltaTime);
            if (transform.position.x == player.position.x)
            {
                isUnburying = true;
                timeBeforeUnburying = unburyTimeValue;
            }
        }
        if (isUnburying)
            UnBury();
    }

    private void Bury()
    {
        //enemy_box.enabled = false;
        //enemy_animator.SetTrigger("MonociteBurying");
        rbMonocite.bodyType = RigidbodyType2D.Kinematic;
        transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, maxDiveDistance), verticalSpeed * Time.deltaTime);
        if(transform.position.y == maxDiveDistance)
        {
            rbMonocite.bodyType = RigidbodyType2D.Static;
            isBurying = false;
            buried = true;
            rbMonocite.bodyType = RigidbodyType2D.Kinematic;
        }
    }

    private void UnBury()
    {
        if (timeBeforeUnburying > 0)
            timeBeforeUnburying -= Time.deltaTime;
        else
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, maxHeight), verticalSpeed * Time.deltaTime);
        
        if (transform.position.y == maxHeight)
        {
            //enemy_box.enabled = true;
            //enemy_animator.SetTrigger("MonociteUnburying");
            rbMonocite.bodyType = RigidbodyType2D.Dynamic;
            buried = false;
            isUnburying = false;
            undergroundAttackInProgress = false;
            Flip();
            inIdleMode = true;
        }
    }

    private void Shoot()
    {
        Shoot();
        //enemy_animator.SetTrigger("MonociteShoot");
        Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        isShooting = false;
        inIdleMode = true;
    }

    private void Idle()
    {
        if (timer <= 0)
            timer = idleTime;
        timer -= Time.deltaTime;
        if (timer <= 0)
            inIdleMode = false;
        Flip();
    }

    void Flip()
    {
        if (player.position.x > transform.position.x)
            transform.eulerAngles = new Vector3(0, -180, 0);
        else
            transform.eulerAngles = new Vector3(0, 0, 0);
    }
}
