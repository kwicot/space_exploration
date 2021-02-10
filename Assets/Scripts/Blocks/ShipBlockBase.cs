using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipBlockBase : MonoBehaviour
{
    public string Name;
    public string Description;
    public float MaxHealth;
    public float Health;
    public float weight;
    public void Damage(float dmg)
    {
        Health -= dmg;
        if (Health <= 0) Destroy(gameObject);
    }
    public void Repair(float count)
    {
        Health += count;
    } 
    public float GetNeedHealthToRepair()
    {
        return MaxHealth - Health;
    }

}
