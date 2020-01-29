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
    public Color PlayerGizmoColour;
    public float PlayerSpawnArea;
    public Vector3 PlayerSpawnLocation;

    [Header("AI Spawn Management")]
    public List<Agent> AIToManage; 
    public Color AIGizmoColour;
    public float MiniumDistanceToPlayer;
    public float AISpawnArea;
    public int AIAmmount;
    public Vector3 AISpawnLocation;

   


    [Header("Extraction Spawn Management")]
    public GameObject ExtractionGameObject;
    public Color ExtractionGizmoColour;
    public float ExtractionPointSpawnArea;
    public float TimeNeededForExtraction;
    public Vector3 ExtractionLocation;
    float TimeOnExtractionPoint;

    [Header("New Game Management")]
    public List<GameObject> SpawnInObjects;


    private void OnDrawGizmos()
    {
        Gizmos.color = PlayerGizmoColour;
        Gizmos.DrawWireSphere(transform.position, PlayerSpawnArea);
        Gizmos.DrawWireCube(PlayerSpawnLocation, new Vector3(10, 10, 10));

        Gizmos.color = AIGizmoColour;
        Gizmos.DrawWireCube(AISpawnLocation, new Vector3(10, 10, 10));
        Gizmos.DrawWireSphere(transform.position, AISpawnArea);

        Gizmos.color = ExtractionGizmoColour;
        Gizmos.DrawWireCube(ExtractionLocation, new Vector3(10, 10, 10));
        Gizmos.DrawWireSphere(transform.position, ExtractionPointSpawnArea);
    }


    // Start is called before the first frame update
    void Start()
    {
        RestartLevel = true;
    }

    // Update is called once per frame
    void Update()
    {
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
        //Generate a vector from the map to spawn the extraction location
        ExtractionLocation = GenerateRandomPoint(transform.position, ExtractionPointSpawnArea);


        //Spawn the extraction point prefab at location
        GameObject go = Instantiate(ExtractionGameObject, ExtractionLocation, transform.rotation);
        SpawnInObjects.Add(go);
    }
    void SpawnPlayer()
    {
        //Generate a vector for the player to be created at
        PlayerSpawnLocation = GenerateRandomPoint(transform.position, PlayerSpawnArea);

        //Create player
        player.transform.position = PlayerSpawnLocation;
    }
    void SpawnAI()
    {
        //Generate a vector for the ai to be created at
        AISpawnLocation = GenerateRandomPoint(transform.position, AISpawnArea);

        //Create and Spawn AI
   foreach(var item in AIToManage)
        {
            item.transform.position = AISpawnLocation;
        }

    }
    public void NewGame()
    {
        foreach(var item in SpawnInObjects)
        {
            Destroy(item);
        }
        RestartLevel = true;
    }
}