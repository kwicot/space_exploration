using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildController : MonoBehaviour
{
    public bool IsBuildMode;
    public LayerMask layerMask;
    public List<GameObject> BlockPrefabs = new List<GameObject>();
    public GameObject CurrentBlock;
    private Rigidbody rb;
    GameObject block;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (IsBuildMode)
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100, layerMask))
            {
                block.transform.position = hit.collider.transform.position + hit.normal;
                block.transform.rotation = hit.transform.rotation;
                if (Input.GetKeyDown(KeyCode.C)) BuildBlock();
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha1)) EnterBuildMode();
        if (Input.GetKeyDown(KeyCode.Alpha2)) ExitBuildMode();
        if (Input.GetKeyDown(KeyCode.Alpha3)) NextBlock();
    }

    void BuildBlock()
    {
        GameObject b = Instantiate(CurrentBlock, block.transform.position, block.transform.rotation, transform);
        b.layer = 6;
        var s = b.GetComponent<ShipBlockBase>();
        ShipController.singltone.AddBlock(s);
    }

    public void EnterBuildMode()
    {
        if (!IsBuildMode)
        {
            IsBuildMode = true;
            CurrentBlock = BlockPrefabs[0];
            block = Instantiate(CurrentBlock);
            rb.isKinematic = true;
            ShipController.singltone.playMode = PlayMode.Build;
        }
        
    }
    public void ExitBuildMode()
    {
        IsBuildMode = false;
        ShipController.singltone.playMode = PlayMode.Fly;
        rb.isKinematic = false;
        Destroy(block);
    }

    public void NextBlock()
    {
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
    }
    public void PreviousBlock()
    {

    }
}
