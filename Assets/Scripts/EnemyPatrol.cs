using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public float speed;
    public int positionOfPatrol;
    public Transform point;
    bool movingRight;

    Transform player;
    public float stoppingDistance;

    bool chill = false;
    bool angry = false;
    bool goBack = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (Vector2.Distance(transform.position, point.position) < positionOfPatrol && (angry == false))
        {
            chill = true;
        }

        if (Vector2.Distance(transform.position, player.position) < stoppingDistance)
        {
            angry = true;
            chill = false;
            goBack = false;
        }

        if (Vector2.Distance(transform.position, player.position) > stoppingDistance)
        {
            goBack = true;
            angry = false;
        }

        if (chill == true)
        {
            Chill();
        }
        else if (angry == true)
        {
            GetAngry();
        }
        else if (goBack == true)
        {
            GoBack();
        }
    }

    void Flip()
    {
        //faceRight = !faceRight;
        //transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        //transform.localRotation = Quaternion.Euler(transform.localEulerAngles.x, transform.localEulerAngles.y, -transform.localEulerAngles.z);

        if (movingRight == true)
            transform.eulerAngles = new Vector3(0, -180, 0);
        else
            transform.eulerAngles = new Vector3(0, 0, 0);
    }



    void Chill()
    {
        if(transform.position.x > point.position.x + positionOfPatrol)
        {
            movingRight = false;
            Flip();
        }
        else if (transform.position.x < point.position.x - positionOfPatrol)
        {
            movingRight = true;
            Flip();
        }

        if (movingRight)
        {
            transform.position = new Vector2(transform.position.x + speed * Time.deltaTime, transform.position.y);
        }
        else
        {
            transform.position = new Vector2(transform.position.x - speed * Time.deltaTime, transform.position.y);
        }
    }

    void GetAngry()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        speed = 3;
        if (transform.position.x > player.position.x)
        {
            movingRight = false;
            Flip();
        }
        else if (transform.position.x < player.position.x)
        {
            movingRight = true;
            Flip();
        }
    }

    void GoBack()
    {
        transform.position = Vector2.MoveTowards(transform.position, point.position, speed * Time.deltaTime);
        Flip();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == PlayerMovement.Instance.gameObject)
        {
            Health.Instance.GetDamage();
        }
    }

}
