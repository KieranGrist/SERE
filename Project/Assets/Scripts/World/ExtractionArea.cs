using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using TMPro;
[System.Serializable]
public class ExtractionArea : MonoBehaviour
{
    public TextMeshPro cumulativeRewardText;
    [Header("Learning AI Management")]
    public LearningSoldier SoldierAgent;
    Vector3 AISpawnLocation;

    [Header("Extraction Management")]
    public ExtractionPoint ExtractionGameObject;
    Vector3 ExtractionLocation;

    [Header("Enemy Management")]
    public GameObject EnemyPrefap;
    public int AmmountToSpawn;
    List<AIPlayer> aIPlayers = new List<AIPlayer>();
    Vector3 PlayerSpawnLocation;



    [Header("Spawn Management")]
    public float SpawnArea = 50;


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, SpawnArea);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(PlayerSpawnLocation, new Vector3(5, 5, 5));
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(AISpawnLocation, new Vector3(5, 5, 5));
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(ExtractionLocation, new Vector3(5, 5, 5));
    }
    // Start is called before the first frame update
    void Start()
    {
        ResetArea();
    }
    // Update is called once per frame
    void Update()
    {
        cumulativeRewardText.text = SoldierAgent.GetCumulativeReward().ToString("0.00");
        if (PlayerRemaining <= 0)
            ResetArea();
    }

    public static GameObject Clone(GameObject gameObject, Transform transform)
    {
        GameObject GO = Instantiate(gameObject, transform.position, transform.rotation);
        return GO;
    }

    // https://www.immersivelimit.com/tutorials/reinforcement-learning-penguins-part-2-unity-ml-agents
    static Vector3 ChooseRandomPosition(Vector3 center, float MinX, float MaxX, float MinZ, float MaxZ)
    {
        Vector3 ret = new Vector3();

    
            // Pick a random radius   
             ret.x = UnityEngine.Random.Range(MinX, MaxX);        

            ret.z  = UnityEngine.Random.Range(MinZ, MaxZ);
        return ret;
    }
    public void ResetArea()
    {
        RemoveAllAI();  
        PlaceSoldier();
        PlaceExtractionPoint();
        ExtractionGameObject.ExtractionRadius = Academy.Instance.EnvironmentParameters.GetWithDefault("Extraction_Radius", 3f);
        SpawnAI(AmmountToSpawn, Academy.Instance.EnvironmentParameters.GetWithDefault("Agent_Speed", 1f));

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
    void RemoveAllAI()
    {
        foreach(var item in aIPlayers)        
            Destroy(item);
        
        aIPlayers = new List<AIPlayer>();
    }
    void PlaceSoldier()
    {
        SoldierAgent.EndEpisode();
        Rigidbody rb = SoldierAgent.GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        AISpawnLocation = ChooseRandomPosition(transform.position, transform.position.x - SpawnArea / 2, transform.position.x + SpawnArea / 2, transform.position.z - SpawnArea / 2, transform.position.z + SpawnArea / 2) + Vector3.up * 5f; ;
        SoldierAgent.MyArea = this;            
        SoldierAgent.transform.position = AISpawnLocation;
    }
    void SpawnAI(int Ammount, float speed)
    {
        for (int i = 0; i < Ammount; i++)
        {
            GameObject player = Instantiate(EnemyPrefap);
            Vector3 PlayerSpawnLocation = ChooseRandomPosition(transform.position, transform.position.x - SpawnArea / 2, transform.position.x + SpawnArea / 2, transform.position.z - SpawnArea / 2, transform.position.z + SpawnArea / 2) + Vector3.up * 5f; ;

            player.transform.position = PlayerSpawnLocation;
            player.transform.parent = transform;
            player.GetComponent<AIPlayer>().AgentSpeed = speed;
            aIPlayers.Add(player.GetComponent<AIPlayer>());
        }     
        

    }
    void PlaceExtractionPoint()
    {
        ExtractionLocation = ChooseRandomPosition(transform.position, transform.position.x - SpawnArea / 2, transform.position.x + SpawnArea / 2, transform.position.z - SpawnArea / 2, transform.position.z + SpawnArea / 2);
 
        ExtractionLocation.y = 1;
        ExtractionGameObject.MyArea = this;
        //Spawn the extraction point prefab at location
        ExtractionGameObject.transform.position = ExtractionLocation;

    }
}