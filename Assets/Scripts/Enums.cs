using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ShipState
{
    Idle,
    NonePower,
    Moving,
    Casting,
    Jumping,
    AutoCollecting
}
public enum BlockTypes
{
    Block,
    Engine,
    Giroscope,
    Accumulator
}

public enum PlayMode
{
    Fly,
    Build
}
