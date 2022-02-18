using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingTarget : MonoBehaviour
{
    [SerializeField]
    private float movingTime;
    [SerializeField]
    private float moveVelocity;
    private float currentMovingTime;
    private int moveDirection;

    Rigidbody rigid;

    private void Start()
    {
        rigid = GetComponent<Rigidbody>();

        moveDirection = -1;
    }

    void Update()
    {
        CalcMove();
        ChangeMoveDirection();
        Move();
    }

    private void CalcMove()
    {
        if(currentMovingTime > 0)
        {
            currentMovingTime -= Time.deltaTime;
        }
    }

    private void ChangeMoveDirection()
    {
        if(currentMovingTime <= 0)
        {
            moveDirection *= -1;
            currentMovingTime = movingTime;
        }
    }

    private void Move()
    {
        rigid.velocity = new Vector3(moveDirection * moveVelocity, 0, 0);
    }

}
