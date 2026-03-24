using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class HoldBlockCode : MonoBehaviour
{
    BlocksArea area;
    public Transform putPos;
    public Transform fadeBlock;
    public Block holdableBlock, holdedBlock;
    public LayerMask blockLayer;
    public Transform holdPos;
    Animator animator;
    public bool putting;
    void Start()
    {
        area = FindAnyObjectByType<BlocksArea>();
        animator = GetComponent<Animator>();
    }

    
    void Update()
    {
        checkPos();
        hold();

    }
    void hold()
    {
        if (holdedBlock != null)
        {
            holdedBlock.checkPosition = false;
            holdedBlock.transform.position = holdPos.position;
            holdedBlock.holded = true;
            if (Input.GetKeyDown(KeyCode.Space) && holdableBlock == null)
            {
                animator.SetTrigger("put");
                putting = true;
            }
        }


        if (holdableBlock != null && holdedBlock == null)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                holdedBlock = holdableBlock;
            }
        }
        animator.SetBool("hold", holdedBlock != null);


        

    }
    public void put()
    {
        if (holdedBlock != null)
        {
            holdedBlock.holded = false;
            holdedBlock.checkPosition = true;
            putting = false;
            holdedBlock = null;
        }
    }
    void checkPos()
    {
        float posX = putPos.transform.position.x - area.transform.position.x;
        float posZ = putPos.transform.position.z - area.transform.position.z;

        float boxSize = area.blockLength;

        float nearestPosX = ((int)(posX / boxSize) * boxSize) + (boxSize / 2) + (posX < 0 ? -boxSize : 0);
        float nearestPosZ = ((int)(posZ / boxSize) * boxSize) + (boxSize / 2) + (posZ < 0 ? -boxSize : 0);

        fadeBlock.transform.position = new Vector3(nearestPosX, 0, nearestPosZ) + area.transform.position;

        Vector3 CheckPos = new Vector3(nearestPosX, 0, nearestPosZ) + area.transform.position;

        Collider[] colls = Physics.OverlapSphere(CheckPos, holdedBlock == null ? boxSize : 0.1f, blockLayer);

        Collider[] newColls = SortByDistance(colls, CheckPos);


        if (newColls.Length > 0)
        {
            holdableBlock = newColls[0].gameObject.GetComponent<Block>();
            if (holdedBlock == null)
            {
                fadeBlock.transform.position = newColls[0].gameObject.transform.position;
            }
            
        }
        else
        {
            holdableBlock = null;
        }
        fadeBlock.gameObject.SetActive(holdableBlock != null || holdedBlock != null);
    }

    Collider[] SortByDistance(Collider[] coll , Vector3 nearestPos)
    {
        List<Collider> colls = new List<Collider>(coll);
        List<Collider> c2 = new List<Collider>();
        foreach (Collider c in colls)
        {
            c2.Add(c);
        }
        c2 = c2.OrderBy(x => (Vector3.Distance(nearestPos, x.transform.position))).ToList();

        return c2.ToArray();
    }
}
