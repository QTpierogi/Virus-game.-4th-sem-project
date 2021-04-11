using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monsterbehavior : Entity
{
    private float waitTime;
    public float starwaitTime;
    public float speed;
    public Transform[] moveSpots;
    private int randomSpot;
    // Start is called before the first frame update
    void Start()
    {
        waitTime = starwaitTime;
        randomSpot = Random.Range(0, moveSpots.Length);

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, moveSpots[randomSpot].position, Time.deltaTime);
        if (Vector2.Distance(transform.position, moveSpots[randomSpot].position) < 0.2f)
        {
            if (waitTime <= 0)
            {
                randomSpot = Random.Range(0, moveSpots.Length);
                waitTime = starwaitTime;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == Hero.Instance.gameObject)
        {
            Hero.Instance.GetDamage();
        }
    }
}
