using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GameManager : MonoBehaviour
{
    public float TimeScale;

    public static GameManager GM;
    bool RestartLevel;
    [Header("Player Spawn Management")]
  public  Target player;
    public float PlayerSpawnArea;
    public Vector3 PlayerSpawnLocation;

    [Header("AI Spawn Management")]
    public List<Agent> AIToManage; 
    public float MiniumDistanceToPlayer;
    public float AISpawnArea;
    public Vector3 AISpawnLocation;

   


    [Header("Extraction Spawn Management")]
    public GameObject ExtractionGameObject;
    public float ExtractionPointSpawnArea;
    public float TimeNeededForExtraction;
    public Vector3 ExtractionLocation;
    float TimeOnExtractionPoint;


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, PlayerSpawnArea);
        Gizmos.DrawWireCube(PlayerSpawnLocation, new Vector3(50, 50, 50));

        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(AISpawnLocation, new Vector3(50, 50, 50));
        Gizmos.DrawWireSphere(transform.position, AISpawnArea);

        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(ExtractionLocation, new Vector3(50, 50, 50));
        Gizmos.DrawWireSphere(transform.position, ExtractionPointSpawnArea);
    }


    // Start is called before the first frame update
    void Start()
    {
        GM = this;
        RestartLevel = true;
    }

    // Update is called once per frame
    void Update()
    {
        ExtractionLocation = ExtractionGameObject.transform.position;
    Time.timeScale = TimeScale;
        GM = this;
        if (RestartLevel)
        {
     
   
   
            SpawnPlayer();

 

            SpawnAI();

   
            SpawnExtractionPoint();

            //FindObjectOfType<NavMeshSurface>().BuildNavMesh();
            RestartLevel = false;
        }

    }
    private static Vector3 GenerateRandomPoint(Vector3 Position, float Radius)
    {
        float MINX, MAXX, MINZ, MAXZ;
        MINX = Position.x - Radius;
        MINZ = Position.z - Radius;
        MAXX = Position.x + Radius;
        MAXZ = Position.z + Radius;
        float X = Random.Range(MINX, MAXX);
        float Z = Random.Range(MINZ, MAXZ);
        float Y = Terrain.activeTerrain.SampleHeight(new Vector3(X, 0, Z));
        return new Vector3(X, Y, Z);
    }


    void SpawnExtractionPoint()
    {
        ExtractionLocation = new Vector3();
        //Generate a vector from the map to spawn the extraction location
        ExtractionLocation = GenerateRandomPoint(transform.position, ExtractionPointSpawnArea);


        //Spawn the extraction point prefab at location
        ExtractionGameObject.transform.position = ExtractionLocation;
    }
    void SpawnPlayer()
    {
        PlayerSpawnLocation = new Vector3(); 
        //Generate a vector for the player to be created at
        PlayerSpawnLocation = GenerateRandomPoint(transform.position, PlayerSpawnArea);

        //Create player
        player.transform.position = PlayerSpawnLocation;
    }
    void SpawnAI()
    {
AISpawnLocation  = new Vector3();
        //Generate a vector for the ai to be created at
        AISpawnLocation = GenerateRandomPoint(transform.position, AISpawnArea);

        //Create and Spawn AI
   foreach(var item in AIToManage)
        {
            item.transform.position = new Vector3();
            item.enabled = true;
            item.Health = 100;
            item.transform.position = AISpawnLocation;
        }

    }
    public void NewGame()
    {
        RestartLevel = true;
    }
}