using UnityEngine;

public class BlocksArea : MonoBehaviour
{
    public Vector2Int mapSize;
    public float blockLength = 0.5f;
    public bool GizmosVisible;
    void Start()
    {
        
    }


    void Update()
    {
        
    }

    private void OnDrawGizmos()
    {
        if (GizmosVisible)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(transform.position + (new Vector3(mapSize.x * blockLength, blockLength, mapSize.y * blockLength) / 2), new Vector3(mapSize.x * blockLength, blockLength, mapSize.y * blockLength));
            Gizmos.color = (Color.green + Color.clear) / 2;
            Gizmos.DrawCube(transform.position + (new Vector3(mapSize.x * blockLength, blockLength, mapSize.y * blockLength) / 2), new Vector3(mapSize.x * blockLength, blockLength, mapSize.y * blockLength));

        }
    }
}
