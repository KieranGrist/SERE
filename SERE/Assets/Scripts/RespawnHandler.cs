using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnHandler : MonoBehaviour
{
    public static RespawnHandler respawn;
    public List<GameObject> RespawnPoints = new List<GameObject>();
    public float RespawnRadius = 50;
    // Start is called before the first frame update
public Vector3 RandomRespawnPosition ()
    {
        var MaxX = transform.position.x + RespawnRadius;

        var MaxZ = transform.position.z + RespawnRadius;
        var MinX = transform.position.x - RespawnRadius;

        var MinZ = transform.position.z - RespawnRadius;

        var RandX = Random.Range(MinX, MaxX);
        var RandZ = Random.Range(MinZ, MaxZ);
        return new Vector3(RandX, 10, RandZ);
    }

    public void HandleRespawn(GameObject ObjectToRespawn)
    {
        if (RespawnPoints.Count > 0)
        {
            var RP = Random.Range(0, RespawnPoints.Count);
            ObjectToRespawn.transform.position = RespawnPoints[RP].transform.position;
        }
        else
        {
            ObjectToRespawn.transform.position = RandomRespawnPosition();
        }
    }
    // Update is called once per frame
    void Update()
    {
        respawn = this;
    }
}
