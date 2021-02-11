using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public static InputController input;

    public KeyCode MoveForwardKey = KeyCode.W;
    public KeyCode MoveBackwardKey = KeyCode.S;
    public KeyCode MoveLeftKey = KeyCode.A;
    public KeyCode MoveRightKey = KeyCode.D;
    public KeyCode RotateLeftKey = KeyCode.Q;
    public KeyCode RotateRightKey = KeyCode.E;

    public KeyCode ForceMoveKey = KeyCode.LeftShift;

    public KeyCode AttackKey = KeyCode.Mouse0;
    public KeyCode AimKey = KeyCode.Mouse1;
    public KeyCode AimTypeChangeKey = KeyCode.V;

    public bool InvertCameraVertical;
    public bool InvertCameraHorizontal;
    public float CameraVerticalSpeed;
    public float CameraHorizontalSpeed;
    public bool LerpCamera;
    [Range(0.01f,1)]
    public float LerpSpeed;
    public bool RotateShipAtLook;




    public delegate void MoveEvent();
    public delegate void MoveEvent2(float direct);
    public delegate void MouseInputEvent(float h, float v);
    public event MoveEvent onMoveForwardKeyPressed;
    public event MoveEvent onMoveBackwardKeyPressed;
    public event MoveEvent onMoveLeftKeyPressed;
    public event MoveEvent onMoveRightKeyPressed;
    public event MoveEvent2 onRotateLeftKeyPressed;
    public event MouseInputEvent OnMouseMove;


    Camera cam;
    public float MouseScroll;

    void Awake()
    {
        if (!input)
            input = this;
        else
            Destroy(this);
    }
    private void Start()
    {
        cam = Camera.main;
    }

    void FixedUpdate()
    {
        if (Input.GetKey(MoveForwardKey)) onMoveForwardKeyPressed();
        if (Input.GetKey(MoveBackwardKey)) onMoveBackwardKeyPressed();
        if (Input.GetKey(MoveLeftKey)) onMoveLeftKeyPressed();
        if (Input.GetKey(MoveRightKey)) onMoveRightKeyPressed();
        if (Input.GetKey(RotateLeftKey)) onRotateLeftKeyPressed(1);
        if (Input.GetKey(RotateRightKey)) onRotateLeftKeyPressed(-1);

        Vector3 newCamPos = cam.transform.localPosition;
        newCamPos.z += Input.GetAxis("Mouse ScrollWheel") * 5;
        cam.transform.localPosition = Vector3.Lerp(cam.transform.localPosition, newCamPos, 0.1f);


        float h = Input.GetAxis("Mouse X");
        float v = Input.GetAxis("Mouse Y");
        if (InvertCameraHorizontal) h *= -1;
        if (InvertCameraVertical) v *= -1;
        OnMouseMove(h * CameraHorizontalSpeed, v * CameraVerticalSpeed);


    }
}
