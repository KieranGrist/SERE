using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using MLAgents;
[System.Serializable]
public class ExtractionArea : MonoBehaviour
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
    public int PlayerWins, AgentWins;
    [Header("Game Management")]
    public float TimeScale = 1;
    public float SpawnArea = 900;
    [Header("Player Management")]
    public Player Enemy;
    Vector3 PlayerSpawnLocation;
    public MapGrids mapGrids;

    [Header("AI Management")]
    public LearningSoldier LearningAIToManage;
    public Soldier ADHocAIToManage; 
    Vector3 AISpawnLocation;

    [Header("Extraction Management")]
    public ExtractionPoint ExtractionGameObject;
    Vector3 ExtractionLocation;


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireCube(transform.position, new Vector3(SpawnArea *2, SpawnArea * 2, SpawnArea * 2));

        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(PlayerSpawnLocation, new Vector3(50, 50, 50));
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(AISpawnLocation, new Vector3(50, 50, 50));
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(ExtractionLocation, new Vector3(50, 50, 50));

        Gizmos.color = Color.blue;

    }
    // Start is called before the first frame update
    void Awake()
    {
        ExtractionGameObject.MyArea = this;
        ExtractionLocation = ExtractionGameObject.transform.position;
        if (ADHocAIToManage)
        {
            ADHocAIToManage.MyArea = this;
            AISpawnLocation = ADHocAIToManage.transform.position;
        }
        if (LearningAIToManage)
        {
            LearningAIToManage.MyArea = this;
            AISpawnLocation = LearningAIToManage.transform.position;
        }
        Enemy.MyArea = this;
        PlayerSpawnLocation = Enemy.transform.position;

        Restart();
    }
    // Update is called once per frame
    void Update()
    {
        Time.timeScale = TimeScale;       
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
        SearchedGrids.Clear();
        SpawnPlayer();
        AISetup();
        SpawnAI();
        NewGame();
        SpawnExtractionPoint();
    }
    void AISetup()
    {
        ADHocAIToManage = FindObjectOfType<Soldier>();
        LearningAIToManage = FindObjectOfType<LearningSoldier>();
    }

    void NewGame()
    {
        foreach (var bullet in FindObjectsOfType<Bullet>())
            Destroy(bullet.gameObject);

    }
    void SpawnPlayer()
    {
        PlayerSpawnLocation = GenerateRandomPoint(transform.position, SpawnArea);
        Enemy.MyArea = this;
        Enemy.Restart();
        Enemy.transform.position = PlayerSpawnLocation;


        if (Enemy is AIPlayer a)
        {
            a.Restart();
            a.AINavAgent.Warp(PlayerSpawnLocation);


        }

    }
    void SpawnAI()
    {
        AISpawnLocation = GenerateRandomPoint(transform.position, SpawnArea);
        //Create and Spawn AI
        if (LearningAIToManage)
        {
            LearningAIToManage.MyArea = this;    
            LearningAIToManage.transform.position = new Vector3();
            LearningAIToManage.transform.position = AISpawnLocation;
            LearningAIToManage.enabled = true;
            LearningAIToManage.entityStats.Health = 100;
        //    LearningAIToManage.Restart();
        }
        if (ADHocAIToManage)
        {
            ADHocAIToManage.MyArea = this;
           
            ADHocAIToManage.Restart();
            ADHocAIToManage.AINavAgent.Warp(new Vector3(0, 0, 0));
            ADHocAIToManage.transform.position = new Vector3();
            ADHocAIToManage.AINavAgent.Warp(AISpawnLocation);
            ADHocAIToManage.enabled = true;
            ADHocAIToManage.entityStats.Health = 100;
            ADHocAIToManage.Restart();
        }

    }
    void SpawnExtractionPoint()
    {
        ExtractionLocation = GenerateRandomPoint(transform.position, SpawnArea);
        ExtractionGameObject.MyArea = this;
        //Spawn the extraction point prefab at location
        ExtractionGameObject.transform.position = ExtractionLocation;

    }

}