using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingBat : MonoBehaviour
{
    public float speed;
    private GameObject player;
    public bool chase = false;
    public Transform startPoint;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }


    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {
            return;
        }

        if (chase == true)
        {
            Chase();
        }
        else
        {
            ReturnStartPoint();
        }
        Chase();
        Flip();
    }

    private void Chase()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
    }

    private void ReturnStartPoint()
    {
        transform.position = Vector2.MoveTowards(transform.position, startPoint.position, speed * Time.deltaTime);
    }
    private void Flip()
    {
        if (transform.position.x > player.transform.position.x)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }
}
