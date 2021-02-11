using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildController : MonoBehaviour
{
    public bool IsBuildMode;
    public LayerMask layerMask;
    public List<GameObject> BlockPrefabs = new List<GameObject>();
    public GameObject selectedBlock;
    private Rigidbody rb;
    GameObject ghostBlock;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private float rotationOffsetZ = 0;
    private float rotationOffsetY = 0;
    void Update()
    {
        if (IsBuildMode)
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100, layerMask))
            {
                var hitRot = hit.transform.rotation;
                ghostBlock.transform.position = hit.collider.transform.position + hit.normal;
                ghostBlock.transform.rotation = Quaternion.Euler(hitRot.x,hitRot.y + rotationOffsetY,hitRot.z + rotationOffsetZ);
                if (Input.GetKey(KeyCode.LeftAlt) && Input.GetMouseButtonDown(0)) DestroyBlock(hit.collider.transform.gameObject);
                else if (Input.GetMouseButtonDown(0)) BuildBlock();
                
                if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.R))
                {
                    rotationOffsetZ += 90;
                }
                else if (Input.GetKeyDown(KeyCode.R))
                {
                    rotationOffsetY += 90;
                }
                
            }
        }

        if (Input.GetKeyDown(KeyCode.B)) EnterBuildMode();
        if (Input.GetKeyDown(KeyCode.Escape)) ExitBuildMode();
        if ((IsBuildMode))
        {
            if (Input.GetKeyDown(KeyCode.Alpha1)) SelectBlock(0);
            else if (Input.GetKeyDown(KeyCode.Alpha2)) SelectBlock(1);
            else if (Input.GetKeyDown(KeyCode.Alpha3)) SelectBlock(2);
            else if (Input.GetKeyDown(KeyCode.Alpha4)) SelectBlock(3);
            else if (Input.GetKeyDown(KeyCode.Alpha5)) SelectBlock(4);
            else if (Input.GetKeyDown(KeyCode.Alpha6)) SelectBlock(5);
            else if (Input.GetKeyDown(KeyCode.Alpha7)) SelectBlock(6);
            else if (Input.GetKeyDown(KeyCode.Alpha8)) SelectBlock(7);
            else if (Input.GetKeyDown(KeyCode.Alpha9)) SelectBlock(8);
        }
    }

    void BuildBlock()
    {
        GameObject b = Instantiate(selectedBlock, ghostBlock.transform.position, ghostBlock.transform.rotation, transform);
        b.layer = 6;
        ShipController.singltone.AddBlock(b.GetComponent<ShipBlockBase>());
    }
    void DestroyBlock(GameObject obj)
    {
        ShipController.singltone.RemoveBlock(obj.GetComponent<ShipBlockBase>());
        Destroy((obj));
    }

    public void EnterBuildMode()
    {
        if (!IsBuildMode)
        {
            IsBuildMode = true;
            SelectBlock(0);
            rb.isKinematic = true;
            ShipController.singltone.playMode = PlayMode.Build;
            ShipController.singltone.transform.rotation = Quaternion.Euler(0,0,0);
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            IsBuildMode = false;
            ShipController.singltone.playMode = PlayMode.Fly;
            rb.isKinematic = false;
            Cursor.lockState = CursorLockMode.Locked;
            Destroy(ghostBlock);
        }
        
    }
    public void ExitBuildMode()
    {
        IsBuildMode = false;
        ShipController.singltone.playMode = PlayMode.Fly;
        rb.isKinematic = false;
        Cursor.lockState = CursorLockMode.Locked;
        Destroy(ghostBlock);
    }

    public void SelectBlock(int index)
    {
        if (BlockPrefabs.Count > index)
        {
            selectedBlock = BlockPrefabs[index];
            
        }
        else
        {
            selectedBlock = BlockPrefabs[BlockPrefabs.Count-1];
        }
        Destroy(ghostBlock);
        ghostBlock = Instantiate(selectedBlock);
        
        /*
         for(int i = 0; i< BlockPrefabs.Count; i++)
        {
            if(CurrentBlock == BlockPrefabs[i])
            {
                if (i + 1 < BlockPrefabs.Count) CurrentBlock = BlockPrefabs[i + 1];
                else CurrentBlock = BlockPrefabs[0];
                break;
            }
        }
        if (IsBuildMode)
        {
            Destroy(block);
            block = Instantiate(CurrentBlock);
        }
        */
    }
}
