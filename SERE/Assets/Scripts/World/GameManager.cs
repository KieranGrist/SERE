using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
[System.Serializable]
public struct DebugRand

{
    [Header("X Debug")]
    public float Minx;
    public float MaxX;
    public float RandX;
    [Header("Z Debug")]
    public float MinZ;
    public float MaxZ;
    public float RandZ;
}

[System.Serializable]

public class GameManager : MonoBehaviour
{

    [Header("Team Management")]
    public Team OneZero;
    public Team OneOne;
    public Team OneTwo;
    public Team OneThree;
    public Team UglyOne;

    public GameManager()
    {
        OneOne = new Team();
        OneOne.TeamName = "1 - 1";
        OneOne.TeamID = 1;

        OneTwo = new Team();
        OneTwo.TeamName = "1 - 2";
        OneTwo.TeamID = 2;

        OneThree = new Team();
        OneThree.TeamName = "1 - 1";
        OneThree.TeamID = 3;

        OneZero = new Team();
        OneZero.TeamName = "1 - 0";
        OneZero.TeamID = 4;

        UglyOne = new Team();
        UglyOne.TeamName = "Ugly One";
        UglyOne.TeamID = 5;
    }
    [Header("Game Management")]
    public float TimeScale = 1;
    public bool Reset;
    public static GameManager GM;
    bool RestartLevel;
    [Header("Player Spawn Management")]
  public  Player player;
    public float PlayerSpawnArea = 1000;
    public Vector3 PlayerSpawnLocation;
    public DebugRand DebugPlayerRand;


    [Header("AI Spawn Management")]
    public List<Agent> AIToManage; 
    public float MiniumDistanceToPlayer;
    public float AISpawnArea = 500;
    public Vector3 AISpawnLocation;
    public DebugRand DebugAIRand;




    [Header("Extraction Spawn Management")]
    public GameObject ExtractionGameObject;
    public float ExtractionPointSpawnArea = 1500;
    public float TimeNeededForExtraction;
    public Vector3 ExtractionLocation;
    float TimeOnExtractionPoint;
    public DebugRand DebugExtractionRand;
    public static GameObject Clone(GameObject gameObject, Transform transform)
    {
        GameObject GO = Instantiate(gameObject, transform.position, transform.rotation);
        return GO;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position, new Vector3(PlayerSpawnArea * 2, PlayerSpawnArea * 2, PlayerSpawnArea * 2));
        Gizmos.DrawWireCube(PlayerSpawnLocation, new Vector3(50, 50, 50));

        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(AISpawnLocation, new Vector3(50, 50, 50));
        Gizmos.DrawWireCube(transform.position, new Vector3(AISpawnArea * 2, AISpawnArea * 2, AISpawnArea * 2));

        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(ExtractionLocation, new Vector3(50, 50, 50));
        Gizmos.DrawWireCube(transform.position, new Vector3(ExtractionPointSpawnArea * 2, ExtractionPointSpawnArea * 2, ExtractionPointSpawnArea * 2));
    }


    // Start is called before the first frame update
    void Start()
    {
        OneOne.ReferenceMembersInTeam();
        OneTwo.ReferenceMembersInTeam();
        OneThree.ReferenceMembersInTeam();
        OneZero.ReferenceMembersInTeam();
        UglyOne.ReferenceMembersInTeam();

        OneOne.SetUpTeam();
        OneTwo.SetUpTeam();
        OneThree.SetUpTeam();
        OneZero.SetUpTeam();
        UglyOne.SetUpTeam();

        GM = this;
        RestartLevel = true;
    }

    // Update is called once per frame
    void Update()
    {
        GM = this;
        ExtractionLocation = ExtractionGameObject.transform.position;
        Time.timeScale = TimeScale;
        GM = this;
        foreach(var item in FindObjectsOfType<Agent>())
        {
            if (item.agentStats.Health < 0)
            {
                item.transform.position = AISpawnLocation;
                item.Restart();
            }
        }
        if (Reset)
        {
            NewGame();
            Reset = false;
        }
        if (RestartLevel)
        {
            SpawnPlayer();
            SpawnAI();
            SpawnExtractionPoint();

            RestartLevel = false;
        }

    }
    private static Vector3 GenerateRandomPoint(Vector3 Position, float Radius,out DebugRand debugRand)
    {
        float MINX, MAXX, MINZ, MAXZ;
        MINX = Position.x - Radius;
        MINZ = Position.z - Radius;
        MAXX = Position.x + Radius;
        MAXZ = Position.z + Radius;
        float X = Random.Range(MINX, MAXX);
        float Z = Random.Range(MINZ, MAXZ);
        float Y = Terrain.activeTerrain.SampleHeight(new Vector3(X, 0, Z));
        Y += 1;
        debugRand.MaxX = MAXX;
        debugRand.Minx = MINX;
        debugRand.MinZ = MINZ;
        debugRand.MaxZ = MAXZ;
        debugRand.RandX = X;
        debugRand.RandZ = Z;
       
        return new Vector3(X, Y, Z);
    }


    void SpawnExtractionPoint()
    {
        ExtractionLocation = new Vector3();
        //Generate a vector from the map to spawn the extraction location
        ExtractionLocation = GenerateRandomPoint(transform.position, ExtractionPointSpawnArea, out DebugExtractionRand);


        //Spawn the extraction point prefab at location
        ExtractionGameObject.transform.position = ExtractionLocation;
        
    }
    void SpawnPlayer()
    {
        player.Restart();
        PlayerSpawnLocation = new Vector3(); 
        //Generate a vector for the player to be created at
        PlayerSpawnLocation = GenerateRandomPoint(transform.position, PlayerSpawnArea,out DebugPlayerRand);
        player.transform.position = PlayerSpawnLocation;
        player.agentStats.Health = 100;


    }
    void SpawnAI()
    {
AISpawnLocation  = new Vector3();

        //Generate a vector for the ai to be created at
        AISpawnLocation = GenerateRandomPoint(transform.position, AISpawnArea,out DebugAIRand);
 
        //Create and Spawn AI
        foreach (var item in AIToManage)
        {
            item.Restart();
            item.transform.position = new Vector3();
            item.AINavAgent.Warp(AISpawnLocation);
            item.enabled = true;
            item.agentStats.Health = 100;
            item.transform.position = AISpawnLocation;
        }

    }
    public void NewGame()
    {
         foreach (var bullet in FindObjectsOfType<Bullet>())
        {
            Destroy(bullet.gameObject);
        }
        RestartLevel = true;
    }
}