using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    public static ShipController singltone;

    Ship ship;



    [HideInInspector]
    public Rigidbody rb;
    HashSet<ShipBlockBase> Blocks = new HashSet<ShipBlockBase>();
    public PlayMode playMode = PlayMode.Fly;

    void Start()
    {
        Init();
    }
    void Init()
    {
        if (!singltone)
            singltone = this;
        else
             Destroy(this);
        
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
        if (ship.Power > 0 && playMode == PlayMode.Fly)
        {
            RotationUpdate();
            ship.Power -= ship.PowerConsumption;
        }



        ship.speed = rb.velocity.magnitude;
        //Debug.Log(ship.speed);
    }

    void MoveForward()
    {
        if (ship.Power > 0 && playMode == PlayMode.Fly)
            foreach (var b in Blocks)
            {
                if(b is BlockEngine)
                {

                }
            }
    }
    void MoveLeft()
    {
        if (ship.Power > 0&& playMode == PlayMode.Fly)
            rb.AddForce(-transform.right * 70);
    }
    void MoveRight()
    {
        if (ship.Power > 0&& playMode == PlayMode.Fly)
            rb.AddForce(transform.right * 70);
    }
    void MoveBackward()
    {
        if (ship.Power > 0&& playMode == PlayMode.Fly)
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
        transform.localRotation = Quaternion.Euler(rotateUpDown, rotateLeftRight, turnLeftRignt);
    }
    void RecalcParams()
    {
        ship.Weight = 0;
        ship.TotalForce = 0;
        ship.TotalHealth = 0;
        ship.TotalPower = 0;
        ship.TotalShield = 0;
        foreach(var b in Blocks)
        {
            ship.Weight += b.weight;
            ship.TotalHealth += b.Health;
            if (b is BlockAccumulator) ship.TotalPower += b.gameObject.GetComponent<BlockAccumulator>().PowerMax;
            if (b is BlockEngine) ship.TotalForce += b.gameObject.GetComponent<BlockEngine>().ForceMax;


        }
        rb.mass = ship.Weight;
        Debug.Log(ship.TotalPower);
    }
    public void AddBlock(ShipBlockBase block)
    {
        Blocks.Add(block);
        RecalcParams();
    }
    public void RemoveBlock(ShipBlockBase block)
    {
        Blocks.Remove(block);
        RecalcParams();
    }
}
