using UnityEngine;

public class Block : MonoBehaviour
{
    BlocksArea area;
    public bool checkPosition;
    public bool holded;
    BoxCollider bx;
    void Start()
    {
        bx = GetComponent<BoxCollider>();
        area = FindAnyObjectByType<BlocksArea>();
    }

    void Update()
    {
        bx.enabled = !holded;
        if (checkPosition)
        {
            PositionTurn();
        }
    }


    void PositionTurn()
    {
        float posX = transform.position.x - area.transform.position.x;
        float posZ = transform.position.z - area.transform.position.z;

        float boxSize = area.blockLength;

        float nearestPosX = ((int)(posX / boxSize) * boxSize) + (boxSize/2) + (posX < 0 ? -boxSize: 0);
        float nearestPosZ = ((int)(posZ / boxSize) * boxSize) + (boxSize / 2) + (posZ < 0 ? -boxSize : 0);

        transform.position = Vector3.Lerp(transform.position, new Vector3(nearestPosX,0, nearestPosZ) + area.transform.position, Time.deltaTime * 10f );
    }
}
