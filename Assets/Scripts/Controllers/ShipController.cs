using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipController : MonoBehaviour
{
    public static ShipController singltone;
    public GameObject TargetLook;
    Ship ship;



    [HideInInspector]
    public Rigidbody rb;
    List<ShipBlockBase> Blocks = new List<ShipBlockBase>();
    public PlayMode playMode = PlayMode.Fly;
    
    //Временно
    public Text speedT;

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
        speedT.text = ship.speed.ToString();
    }

    void MoveForward()
    {
        if (ship.Power > 0 && playMode == PlayMode.Fly)
        {
            rb.AddForce(transform.forward * ship.TotalForce);
        }
    }
    void MoveLeft()
    {
        if (ship.Power > 0 && playMode == PlayMode.Fly)
        {
            rb.AddForce(-transform.right * ship.TotalForce);
        }
    }
    void MoveRight()
    {
        if (ship.Power > 0 && playMode == PlayMode.Fly)
        {
            rb.AddForce(transform.right * ship.TotalForce);
        }
    }
    void MoveBackward()
    {
        if (ship.Power > 0 && playMode == PlayMode.Fly)
        {
            rb.AddForce(-transform.forward * ship.TotalForce);
        }
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
        transform.LookAt(TargetLook.transform);
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
