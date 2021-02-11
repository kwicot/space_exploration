using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockEngine : ShipBlockBase
{
    public float ForceMax;
    public float PowerConsumptionMax;
    ParticleSystem effect;

    private void Start()
    {
        effect = GetComponent<ParticleSystem>();
    }
    private void Update()
    {
    }
}
