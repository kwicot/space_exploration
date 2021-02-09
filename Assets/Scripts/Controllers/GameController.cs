using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController singltone;
    public ShipController shipController;
    public CameraController cameraController;
    void Start()
    {
        if (!singltone)
            singltone = this;
        else Destroy(gameObject);
    }

    void Update()
    {
        
    }
}
