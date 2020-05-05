using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using TMPro;
public class SEREArea : MonoBehaviour
{
    public TextMeshPro cumulativeRewardText;
    [Header("Extraction Management")]
    public ExtractionPoint ExtractionGameObject;
    Vector3 ExtractionLocation;

    [Header("Spawn Management")]
    public float SpawnArea = 50;

    [Header("Enemy Management")]
    public GameObject EnemyPrefap;
    public int AmmountToSpawn;
    protected List<AIPlayer> aIPlayers = new List<AIPlayer>();
    Vector3 PlayerSpawnLocation;
    public virtual void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireCube(transform.position, new Vector3(SpawnArea *2, SpawnArea * 2, SpawnArea * 2));
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(PlayerSpawnLocation, new Vector3(5, 5, 5));
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(ExtractionLocation, new Vector3(5, 5, 5));
    }
    public virtual void ResetArea()
    {
        RemoveAllAI();
        ExtractionGameObject.ExtractionRadius = Academy.Instance.EnvironmentParameters.GetWithDefault("Extraction_Radius", 3f);

    }
    public void RemovePlayer(AIPlayer player)
    {
        aIPlayers.Remove(player);
        Destroy(player.gameObject);
    }
    public int PlayerRemaining
    {
        get { return aIPlayers.Count; }
    }
    protected void RemoveAllAI()
    {
        foreach (var item in aIPlayers)
            Destroy(item);
        aIPlayers = new List<AIPlayer>();
    }
    public static Vector3 ChooseRandomPosition(Vector3 center, float MinX, float MaxX, float MinZ, float MaxZ)
    {
        Vector3 ret = new Vector3
        {
            // Pick a random radius   
            x = UnityEngine.Random.Range(center.x - MinX, center.x + MaxX),
            z = UnityEngine.Random.Range(center.z - MinZ, center.z + MaxZ)
        };
        return ret;
    }
    protected void PlaceExtractionPoint()
    {
        ExtractionLocation = ChooseRandomPosition(transform.position, SpawnArea ,  SpawnArea ,  SpawnArea , SpawnArea);

        ExtractionLocation.y = 1;
        ExtractionGameObject.MyArea = this;
        //Spawn the extraction point prefab at location
        ExtractionGameObject.transform.position = ExtractionLocation;
    }
    public static GameObject Clone(GameObject gameObject, Transform transform)
    {
        GameObject GO = Instantiate(gameObject, transform.position, transform.rotation);
        return GO;
    }
}