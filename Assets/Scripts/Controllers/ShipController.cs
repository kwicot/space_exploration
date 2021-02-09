using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    public Transform LookTarget;


    public Ship ship;



    [HideInInspector]
    public Rigidbody rb;

    void Start()
    {
        
        InputController.input.onMoveForwardKeyPressed += MoveForward;
        InputController.input.onMoveBackwardKeyPressed += MoveBackward;
        InputController.input.onMoveLeftKeyPressed += MoveLeft;
        InputController.input.onMoveRightKeyPressed += MoveRight;
        InputController.input.onRotateLeftKeyPressed += TurnLeftRight;
        InputController.input.OnMouseMove += Rotate;

        rb = GetComponent<Rigidbody>();
        ship = new Ship() { Power = 100 };
    }
    void FixedUpdate()
    {
        if (ship.Power > 0)
            RotationUpdate();

        ship.speed = rb.velocity.magnitude;



        Debug.Log(ship.speed);
    }

    void MoveForward()
    {
        if (ship.Power > 0)
            rb.AddForce(transform.forward * 70);
    }
    void MoveLeft()
    {
        if (ship.Power > 0)
            rb.AddForce(-transform.right * 70);
    }
    void MoveRight()
    {
        if (ship.Power > 0)
            rb.AddForce(transform.right * 70);
    }
    void MoveBackward()
    {
        if (ship.Power > 0)
            rb.AddForce(-transform.forward * 70);
    }

    float turnLeftRignt;
    float rotateUpDown;
    float rotateLeftRight;
    void TurnLeftRight(float turn)
    {
        turnLeftRignt += turn;
    }
    void Rotate(float h, float v)
    {
        rotateUpDown += v;
        rotateLeftRight += h;
    }
    void RotationUpdate()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(-transform.right.x + rotateUpDown,transform.localRotation.y + rotateLeftRight, turnLeftRignt), 0.1f);
        //transform.LookAt(LookTarget);
        //transform.rotation = Quaternion.Lerp(transform.rotation, Camera.main.transform.rotation, 0.1f);
    }
}
