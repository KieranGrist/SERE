using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
[System.Serializable]
public class GameManager : MonoBehaviour
{
    [Header("Names ")]
    public List<string> FirstNames = new List<string>
    {
          "Kieran",

          "Lauren",
          "Liam",
"Noah",
"William",
"James",
"Oliver",
"Benjamin",
"Elijah",
"Lucas",
"Mason",
"Logan",
"Alexander",
"Ethan",
"Jacob",
"Michael",
"Daniel",
"Henry",
"Jackson",
"Sebastian",
"Aiden",
"Matthew",
"Samuel",
"David",
"Joseph",
"Carter",
"Owen",
"Wyatt",
"John",
"Jack",
"Luke",
"Jayden",
"Dylan",
"Grayson",
"Levi",
"Isaac",
"Gabriel",
"Julian",
"Mateo",
"Anthony",
"Jaxon",
"Lincoln",
"Joshua",
"Christopher",
"Andrew",
"Theodore",
"Caleb",
"Ryan",
"Asher",
"Nathan",
"Thomas",
"Leo",
"Isaiah",
"Charles",
"Josiah",
"Hudson",
"Christian",
"Hunter",
"Connor",
"Eli",
"Ezra",
"Aaron",
"Landon",
"Adrian",
"Jonathan",
"Nolan",
"Jeremiah",
"Easton",
"Elias",
"Colton",
"Cameron",
"Carson",
"Robert",
"Angel",
"Maverick",
"Nicholas",
"Dominic",
"Jaxson",
"Greyson",
"Adam",
"Ian",
"Austin",
"Santiago",
"Jordan",
"Cooper",
"Brayden",
"Roman",
"Evan",
"Ezekiel",
"Xavier",
"Jose",
"Jace",
"Jameson",
"Leonardo",
"Bryson",
"Axel",
"Everett",
"Parker",
"Kayden",
"Miles",
"Sawyer",
"Jason",
"Emma",
"Olivia",
"Ava",
"Isabella",
"Sophia",
"Charlotte",
"Mia",
"Amelia",
"Harper",
"Evelyn",
"Abigail",
"Emily",
"Elizabeth",
"Mila",
"Ella",
"Avery",
"Sofia",
"Camila",
"Aria",
"Scarlett",
"Victoria",
"Madison",
"Luna",
"Grace",
"Chloe",
"Penelope",
"Layla",
"Riley",
"Zoey",
"Nora",
"Lily",
"Eleanor",
"Hannah",
"Lillian",
"Addison",
"Aubrey",
"Ellie",
"Stella",
"Natalie",
"Zoe",
"Leah",
"Hazel",
"Violet",
"Aurora",
"Savannah",
"Audrey",
"Brooklyn",
"Bella",
"Claire",
"Skylar",
"Lucy",
"Paisley",
"Everly",
"Anna",
"Caroline",
"Nova",
"Genesis",
"Emilia",
"Kennedy",
"Samantha",
"Maya",
"Willow",
"Kinsley",
"Naomi",
"Aaliyah",
"Elena",
"Sarah",
"Ariana",
"Allison",
"Gabriella",
"Alice",
"Madelyn",
"Cora",
"Ruby",
"Eva",
"Serenity",
"Autumn",
"Adeline",
" Hailey",
"Gianna",
"Valentina",
"Isla",
"Eliana",
"Quinn",
"Nevaeh",
"Ivy",
"Sadie",
"Piper",
"Lydia",
"Alexa",
"Josephine",
"Emery",
"Julia",
"Delilah",
"Arianna",
"Vivian",
"Kaylee",
"Sophie",
"Brielle",
"Madeline"
    };
    public List<string> LastNames = new List<string>
    {
        "Grist",
"Smith",
"Taylor",
"Hayes",
"Anderson",
"Thomas",
"Jackson",
"White",
"Harris",
"Martin",
"Thompson",
"Garcia",
"Martinez",
"Johnson",
"Robinson",
"Clark",
"Rodriguez",
"Lewis",
"Lee",
"Walker",
"Hall",
"Allen",
"Young",
"Hernandez",
"Williams",
"King",
"Wright",
"Lopez",
"Hill",
"Scott",
"Green",
"Adams",
"Baker",
"Gonzalez",
"Nelson",
"Jones",
"Carter",
"Mitchell",
"Perez",
"Roberts",
"Turner",
"Phillips",
"Campbell",
"Parker",
"Evans",
"Edwards",
"Brown",
"Collins",
"Stewart",
"Sanchez",
"Morris",
"Rogers",
"Reed",
"Cook",
"Morgan",
"Bell",
"Murphy",
"Davis",
"Bailey",
"Rivera",
"Cooper",
"Richardson",
"Cox",
"Howard",
"Ward",
"Torres",
"Peterson",
"Gray",
"Miller",
"Ramirez",
"James",
"Watson",
"Brooks",
"Kelly",
"Sanders",
"Price",
"Bennett",
"Wood",
"Barnes",
"Wilson",
"Ross",
"Henderson",
"Coleman",
"Jenkins",
"Perry",
"Powell",
"Long",
"Patterson",
"Hughes",
"Flores",
"Moore",
"Washington",
"Butler",
"Simmons",
"Foster",
"Gonzales",
"Bryant",
"Alexander",
"Russell",
"Griffin",
"Diaz",
    };
    public List<Grid> SearchedGrids = new List<Grid>();

    [Header("Team Management")]
    public Team TeamOne;
    public Team TeamTwo;
    public Team TeamThree;
    public Team TeamFour;

    [Header("Game Management")]
    public float TimeScale = 1;
    [Header("Player Spawn Management")]
    public List<Player> player;
    public float PlayerSpawnArea = 1000;
    public Vector3 PlayerSpawnLocation;



    [Header("AI Spawn Management")]
    public List<Agent> AIToManage;
    public float MiniumDistanceToPlayer;
    public float AISpawnArea = 500;
    public Vector3 AISpawnLocation;


    public int Alive;
    public int Dead;
    public int Total;

    [Header("Extraction Spawn Management")]
    public ExtractionPoint ExtractionGameObject;
    public float ExtractionPointSpawnArea = 1500;
    public Vector3 ExtractionLocation;
    public List<GameObject> CopiedGameObjects = new List<GameObject>();


    public GameManager()
    {
        
        TeamOne = new Team
        {
            TeamName = "Team One",
            TeamID = 1
        };

        TeamTwo = new Team
        {
            TeamName = "Team Two",
            TeamID = 2
        };


        TeamThree = new Team
        {
            TeamName = "Team Three",
            TeamID = 3
        };


        TeamFour = new Team
        {
            TeamName = "Team Team Four",
            TeamID = 4
        };

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position, new Vector3(PlayerSpawnArea * 2, PlayerSpawnArea * 2, PlayerSpawnArea * 2));
        Gizmos.DrawWireCube(PlayerSpawnLocation, new Vector3(50, 50, 50));

        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(AISpawnLocation, new Vector3(50, 50, 50));
        Gizmos.DrawWireCube(transform.position, new Vector3(AISpawnArea * 2, AISpawnArea * 2, AISpawnArea * 2));

        Gizmos.color = Color.red;

        Gizmos.DrawWireCube(transform.position, new Vector3(ExtractionPointSpawnArea * 2, ExtractionPointSpawnArea * 2, ExtractionPointSpawnArea * 2));

        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(ExtractionLocation, new Vector3(50, 50, 50));

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
    void Awake()
    {   
        name = "Game Manager";       
        Restart();
    }
    // Update is called once per frame
    void Update()
    {
        TeamSetup();  
        ExtractionLocation = ExtractionGameObject.transform.position;
        Time.timeScale = TimeScale;
        var Targets = FindObjectsOfType<Player>();
        Total = Targets.Length;
        Alive = 0;
        Dead = 0;
        foreach (var item in Targets)
        {
            if (item.Affiliation == Side.Enemy)
                Alive++;
            if (item.Affiliation == Side.Civilian)
                Dead++;
        }
    }
    public string GenerateName()
    {
        string Ret;
        Ret = FirstNames[Random.Range(0, FirstNames.Count - 1)];
        Ret += " " + LastNames[Random.Range(0, LastNames.Count - 1)];
        return Ret;
    }
    public static GameObject Clone(GameObject gameObject, Transform transform)
    {
        GameObject GO = Instantiate(gameObject, transform.position, transform.rotation);
        return GO;
    }
    public static Vector3 GenerateRandomPoint(Vector3 Position, float Radius)
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
        return new Vector3(X, Y, Z);
    }
    public void Restart()
    {
        SearchedGrids = new List<Grid>();
        SpawnPlayer();
        AISetup();
        SpawnAI();
        AssignTeams();
        TeamSetup();
        NewGame();
        var Targets = FindObjectsOfType<Player>();
        Total = Targets.Length;
        Alive = 0;
        Dead = 0;
        foreach (var item in Targets)
        {
            if (item.Affiliation == Side.Enemy)
                Alive++;
            if (item.Affiliation == Side.Civilian)
                Dead++;
        }
        SpawnExtractionPoint();
        Copy();
    }      
    public void LearningAIRestart()
    {
        SpawnPlayer();
        SpawnExtractionPoint();
    }
    void AISetup()
    {
        AIToManage = new List<Agent>();
        AIToManage.Clear();
        AIToManage.AddRange(FindObjectsOfType<Agent>());
    }
    void TeamSetup()
    {

        TeamOne.ReferenceMembersInTeam();
        TeamTwo.ReferenceMembersInTeam();
        TeamThree.ReferenceMembersInTeam();
        TeamFour.ReferenceMembersInTeam();
        TeamOne.SetUpTeam();
        TeamTwo.SetUpTeam();
        TeamThree.SetUpTeam();
        TeamFour.SetUpTeam();
    }
    void AssignTeams()
    {
        TeamOne = new Team
        {
            TeamName = "Team One",
            TeamID = 1
        };

        TeamTwo = new Team
        {
            TeamName = "Team Two",
            TeamID = 2
        };


        TeamThree = new Team
        {
            TeamName = "Team Three",
            TeamID = 3
        };


        TeamFour = new Team
        {
            TeamName = "Team Team Four",
            TeamID = 4
        };
        var AITOAssign = AIToManage;
        for (int i = AITOAssign.Count - 1; i >= 0; i--)
        {
            if (AITOAssign[i] is Soldier s)
            {
                bool Assigned = false;
                if (s is TeamLeader t)
                {
                    if (TeamOne.teamLeader == null && Assigned == false)
                    {
                        Assigned = true;
                        TeamOne.teamLeader = t;
                        TeamOne.Members.Add(t);
                        AITOAssign.Remove(t);
                    }
                    if (TeamTwo.teamLeader == null && Assigned == false)
                    {
                        Assigned = true;
                        TeamTwo.teamLeader = t;
                        TeamTwo.Members.Add(t);
                        AITOAssign.Remove(t);
                    }
                    if (TeamThree.teamLeader == null && Assigned == false)
                    {
                        Assigned = true;
                        TeamThree.teamLeader = t;
                        TeamThree.Members.Add(t);
                        AITOAssign.Remove(t);
                    }
                    if (TeamFour.teamLeader == null && Assigned == false)
                    {
                        Assigned = true;
                        TeamFour.teamLeader = t;
                        TeamFour.Members.Add(t);
                        AITOAssign.Remove(t);
                    }
                }
                if ((TeamOne.teamLeader == true && (TeamOne.Members.Count < 4 && Assigned == false)) || (TeamOne.teamLeader == false && (TeamOne.Members.Count < 3 && Assigned == false)))
                {
                    Assigned = true;
                    TeamOne.Members.Add(s);
                    AITOAssign.Remove(s);
                }
                if ((TeamTwo.teamLeader == true && (TeamTwo.Members.Count < 4 && Assigned == false)) || (TeamTwo.teamLeader == false && (TeamTwo.Members.Count < 3 && Assigned == false)))
                {
                    Assigned = true;
                    TeamTwo.Members.Add(s);
                    AITOAssign.Remove(s);
                }
                if ((TeamThree.teamLeader == true && (TeamThree.Members.Count < 4 && Assigned == false)) || (TeamThree.teamLeader == false && (TeamThree.Members.Count < 3 && Assigned == false)))
                {
                    Assigned = true;
                    TeamThree.Members.Add(s);
                    AITOAssign.Remove(s);
                }
                if ((TeamFour.teamLeader == true && (TeamFour.Members.Count < 4 && Assigned == false)) || (TeamFour.teamLeader == false && (TeamFour.Members.Count < 3 && Assigned == false)))
                {
                    TeamFour.Members.Add(s);
                    AITOAssign.Remove(s);
                }

            }
        }
    }
    void NewGame()
    {
        foreach (var bullet in FindObjectsOfType<Bullet>())        
            Destroy(bullet.gameObject);

        foreach (var scent in FindObjectsOfType<ScentSphere>())
            Destroy(scent);
    }
    void SpawnPlayer()
    {
        player.Clear();
        foreach(var item in FindObjectsOfType<Player>())
            player.Add(item);     
        foreach (var item in FindObjectsOfType<ScentSphere>())
            item.Age = 0;
        foreach (var item in player)
        {
            item.GM = this;
            item.Restart();
            PlayerSpawnLocation = new Vector3();
            //Generate a vector for the player to be created at
            PlayerSpawnLocation = GenerateRandomPoint(transform.position, PlayerSpawnArea);
            item.transform.position = PlayerSpawnLocation;
            item.entityStats.Health = 100;
            AIPlayer aIPlayer = item.GetComponent<AIPlayer>();
            if (item is AIPlayer a)
            {
                a.Restart();
                a.AINavAgent.Warp(PlayerSpawnLocation);
     
        
            }
            if (item is ShootingTarget s)
            {
                s.Restart();
                s.AINavMeshAgent.Warp(PlayerSpawnLocation);
            

            }
            
        }

    }
    void SpawnAI()
    {
       
        AISpawnLocation = new Vector3();

        //Generate a vector for the ai to be created at
        

        //Create and Spawn AI
        foreach (var item in AIToManage)
        {
            item.GM = this;
            AISpawnLocation = GenerateRandomPoint(transform.position, AISpawnArea);
            item.Restart();
            item.AINavAgent.Warp(new Vector3(0, 0, 0));
            item.transform.position = new Vector3();
            item.AINavAgent.Warp(AISpawnLocation);
            item.enabled = true;
            item.entityStats.Health = 100;
            if (item is Soldier s)                       
                s.Restart();
             
            
        
        }

    }
    void SpawnExtractionPoint()
    {
       
        ExtractionLocation = new Vector3();
        //Generate a vector from the map to spawn the extraction location
        ExtractionLocation = GenerateRandomPoint(transform.position, ExtractionPointSpawnArea);

        ExtractionGameObject.GM = this;
        //Spawn the extraction point prefab at location
        ExtractionGameObject.transform.position = ExtractionLocation;

    }
    void Copy()
    {
        foreach (var item in CopiedGameObjects)
            DestroyImmediate(item);
        CopiedGameObjects.Clear();
        var GameObjectsToCopy = new List<GameObject>();
        GameObjectsToCopy.AddRange(FindObjectsOfType<GameObject>());
        GameManager gameManager = this;
        for (int i = GameObjectsToCopy.Count - 1; i >= 0; i--)
        {
            var item = GameObjectsToCopy[i];
            if ( item.GetComponent<Light>() || item.GetComponent<UserInterface>() || item.GetComponent<Camera>() || item.GetComponent<Agent>() )
                GameObjectsToCopy.Remove(item);
            gameManager = GetComponent<GameManager>();
        }
        foreach (var item in GameObjectsToCopy)
            CopiedGameObjects.Add(Clone(item, item.transform));
        foreach (var item in CopiedGameObjects)
        {
            item.transform.position += new Vector3(5000, 0, 0);
        }
        gameManager.LearningAIRestart();


        }
}