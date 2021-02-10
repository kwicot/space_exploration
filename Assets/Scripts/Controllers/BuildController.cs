using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildController : MonoBehaviour
{
    public bool IsBuildMode;
    public List<GameObject> BlockPrefabs = new List<GameObject>();
    public GameObject CurrentBlock;
    GameObject block;
    void Start()
    {
        
    }

    void Update()
    {
        if (IsBuildMode)
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition),out hit, 1000))
            {
                Transform pos = hit.transform;
                block.transform.position = pos.position + Vector3.right;
                block.transform.rotation = pos.rotation;
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha1)) EnterBuildMode();
        if (Input.GetKeyDown(KeyCode.Alpha2)) ExitBuildMode();
        if (Input.GetKeyDown(KeyCode.Alpha3)) NextBlock();
    }

    public void EnterBuildMode()
    {
        if (!IsBuildMode)
        {
            IsBuildMode = true;
            CurrentBlock = BlockPrefabs[0];
            block = Instantiate(CurrentBlock);
        }
    }
    public void ExitBuildMode()
    {
        IsBuildMode = false;
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
