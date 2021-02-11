using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject CameraPivot;
    public ShipController ship;
    public Vector3 Offset;




    bool lerp;
    float lerpSpeed;
    void Start()
    {
        Init();
    }

    void Init()
    {
        //GameController.singltone.cameraController = this;
        InputController.input.OnMouseMove += RotateUpdate;
        lerp = InputController.input.LerpCamera;
        lerpSpeed = InputController.input.LerpSpeed;
    }

    void FixedUpdate()
    {
        MoveUpdate();
    }

    float horizont;
    float vertical;
    void RotateUpdate(float h, float v)
    {
        if (ShipController.singltone.playMode == PlayMode.Fly)
        {

            horizont += h;
            vertical += v;
            /*CameraPivot.transform.eulerAngles = new Vector3(
                        CameraPivot.transform.eulerAngles.x + v,
                        CameraPivot.transform.eulerAngles.y + h,
                        CameraPivot.transform.eulerAngles.z); */
            CameraPivot.transform.rotation =
                Quaternion.Lerp(transform.rotation, Quaternion.Euler(vertical, horizont, 0), 0.1f);
        }
        else
        {
            if (Input.GetMouseButton(1))
            {
                horizont += Input.GetAxis("Mouse X");
                vertical -= Input.GetAxis("Mouse Y");
                CameraPivot.transform.rotation = Quaternion.Euler(vertical * 4,horizont * 4,0);
            }
        }
    }
    void MoveUpdate()
    {
        if (lerp)
            CameraPivot.transform.position = Vector3.Lerp(CameraPivot.transform.position, ship.transform.localPosition, lerpSpeed);
        else
            CameraPivot.transform.position = ship.transform.position;
        Offset.z = transform.localPosition.z;
        transform.localPosition = Offset;
    }
}
