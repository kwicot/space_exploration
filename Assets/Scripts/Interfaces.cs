using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEngine
{
    public float MaxThrust { get; set; }
    public float Fuel { get; set; }
    public void Thrust();
}
