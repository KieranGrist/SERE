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
    public Team TeamOne;
    public Team TeamTwo;
    public Team TeamThree;
    public Team TeamFour;
    public GameManager()
    {
        TeamOne = new Team();
        TeamOne.TeamName = "Team One";
        TeamOne.TeamID = 1;

        TeamTwo = new Team();
        TeamTwo.TeamName = "Team Two";
        TeamTwo.TeamID = 2;


        TeamThree = new Team();
        TeamThree.TeamName = "Team Three";
        TeamThree.TeamID = 3;


        TeamFour = new Team();
        TeamFour.TeamName = "Team Team Four";
        TeamFour.TeamID = 4;

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
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        if (TeamOne.teamLeader)
            foreach (var item in TeamOne.Members)
                Gizmos.DrawLine(item.transform.position, TeamOne.teamLeader.transform.position);
        if (TeamTwo.teamLeader)
            foreach (var item in TeamTwo.Members)
                Gizmos.DrawLine(item.transform.position, TeamTwo.teamLeader.transform.position);
        if (TeamThree.teamLeader)
            foreach (var item in TeamThree.Members)
                Gizmos.DrawLine(item.transform.position, TeamThree.teamLeader.transform.position);
        if (TeamFour.teamLeader)
            foreach (var item in TeamFour.Members)
                Gizmos.DrawLine(item.transform.position, TeamFour.teamLeader.transform.position);
    }

    // Start is called before the first frame update
    void Start()
    {
        AIToManage = new List<Agent>();
        AIToManage.Clear();
        AIToManage.AddRange(FindObjectsOfType<Agent>());
        TeamOne.ReferenceMembersInTeam();
        TeamTwo.ReferenceMembersInTeam();
        TeamThree.ReferenceMembersInTeam();
        TeamFour.ReferenceMembersInTeam();
        TeamOne.SetUpTeam();
        TeamTwo.SetUpTeam();
        TeamThree.SetUpTeam();
        TeamFour.SetUpTeam();  

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
        AIPlayer aIPlayer = player.GetComponent<AIPlayer>();
        if(aIPlayer)
        {
            aIPlayer.agent.Warp(PlayerSpawnLocation);
        }

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