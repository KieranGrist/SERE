using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
[System.Serializable]
public class Team
{
    [Header("Team")]
    public string TeamName;
    public int TeamID;
    public List<Agent> Members = new List<Agent>(4);
    public List<Grid> SearchedGrids = new List<Grid>();
    public TeamLeader teamLeader;
    public Team()
    {
        TeamName = "1 - 1";
        TeamID = 0;
        Members = new List<Agent>(4);
    }
    public void SetUpTeam()
    {
        foreach (var item in Members)
        {
            item.AIRadio.Frequency = TeamID;
        }
    }
    public void ReferenceMembersInTeam()
    {
        foreach (var item in Members)
            item.AgentsTeam = this;
    }
    public Agent GetMemberInTeam(Agent me)
    {
        foreach(var item in Members)
        {
            if (item != me)
                return item;
        }
        return null;
    }
}
[System.Serializable]
public class Search
{
    [Header("Search")]
    [Tooltip("Center of search ")]
    public Vector3 SearchLocation;
    [Tooltip("Distance to search ")]
    public float SearchDistance = 500;
    [Tooltip("How long the Agent should search the area for")]
    public float MaxSearchTime = 100;
    float CurrentSearchTime;
    public bool SearchInsideCicle;
    public bool Searching = true;
    public bool OrderedSearch = false;

    public Grid CurrentSearchGrid;
    public List<Grid> SearchedGrids = new List<Grid>();
    public List<GameObject> SearchPoints = new List<GameObject>();


 public   Search()
    {
        SearchLocation = new Vector3();
        SearchDistance = 500;
        MaxSearchTime = 100;
        SearchInsideCicle = false;
        SearchedGrids = new List<Grid>();
            }
    public Search(float Distance, float SearchTime, bool InsideCircle = false)
    {
        SearchLocation = new Vector3();
        SearchDistance = Distance;
        MaxSearchTime = SearchTime;
        SearchInsideCicle = InsideCircle;
        SearchedGrids = new List<Grid>();
    }
    public Search(bool insideCircle )
    {
        SearchLocation = new Vector3();
        SearchDistance = 500;
        MaxSearchTime = 100;
        SearchInsideCicle = insideCircle;
        SearchedGrids = new List<Grid>();
    }
}
[System.Serializable]
public class BrainInformation
{
    [Header("Brain Information")]
    public Vector3 PlayersLastKnownLocation;
    public Vector3 PlayersTravelingDirection;
    [Tooltip("How far the AI can see")]
    public float VisionDistance = 500;
    [Tooltip("How far the AI can shoot")]
    public float CombatDistance = 100;
    [Tooltip("How far the AI can hear")]
    public float HearingDistance = 150;
    [Tooltip("How powerfull the nose is")]
    public bool AbillityToSmell = false;

    public bool SeePlayer = false;
    public bool HearPlayer = false;
}
[System.Serializable]

public class DebugInformation
{
    Agent agent;
    public DebugInformation(Agent agent)
    {
        this.agent = agent;
    }
    public float DistanceToPlayer;

  public void UpdateInformation()
    {
        DistanceToPlayer = Vector3.Distance(agent.transform.position, GameManager.GM.player.transform.position);

    }
}
[System.Serializable]
[RequireComponent(typeof(NavMeshAgent))]
public abstract class Agent : Entity
{
    protected readonly string[] _firstNames = new string[]
{
        "Kieran",
        "Alex",
        "Gavin",
        "Lawrence",
       "Alice",
        "Sophie",
        "Iona",
        "Chloe",
        "Lucia",
        "Dale",
        "Georgina",
        "Nicole",
        "Kara",
       "Hailee",
       "Helen",
       "Emily",
       "Liberty",
       "Faye",
       "Carrie",
       "Elsie",
       "Crystal",
       "Maria",
       "Ayala",
       "Alanah",
       "Amie",
       "Jack",
       "Ben",
       "Adam",
       "Tegan",
       "Edan",
       "Alison",
       "Merle",
       "Aden",
       "Allyson",
       "Lyndsey",
       "Stacia",
        "Lauren",
        "Sarah",
        "Matt",
        "Paul",
        "Maddie",
        "Lando",
        "Lewis",
        "Sebastian",
        "Carlos",
        "Sergio",
        "Piere",
        "Nicco",
        "Esteban",
        "Robert",
        "George",
        "Charles",
        "Max",
        "Alex",
        "Lance",
        "Kevin",
        "Roman",
        "Jules",
        "Peter",
        "Mikey",
        "Valtteri",
        "Daniel"


};
    public Agent() : base ()
    {
        search = new Search();
        AgentsTeam = new Team();
     brain = new BrainInformation();
        AIRadio = null;
    }
    [Header("Agent")]
    public DebugInformation DebugInfo;
    public Node RootNode;
    public Team AgentsTeam = new Team();
    public Search search;
    public BrainInformation brain;
    


    [Header("Agent Stats")]
    public NavMeshAgent AINavAgent;
    public float Mass, MaxVelocity, MaxForce;
    public float StopDistance = 1;
    public Vector3 velocity;
    public Vector3 MoveToLocation;
    bool IsMoving;

      
    [Header("Radio")]
    public Radio AIRadio;
    public bool SquadTransmitRadio;

    public virtual new void Start()
    {
        base.Start();
        AINavAgent = GetComponent<NavMeshAgent>();
        DebugInfo = new DebugInformation(this);
        Selector root = new Selector(this, "Root Node");
        RootNode = root;
        search = new Search();
        brain = new BrainInformation();
        CreateSearchPoints();
        BT_Combat CombatNode = new BT_Combat(this, "Combat Node");
        BT_Search SearchNode = new BT_Search(this, "Search Node");
        BT_Chase ChaseNode = new BT_Chase(this, "Chase Node");
        CombatDecorator CombatDec = new CombatDecorator(CombatNode, this, "Combat Decorator");
        SearchDecorator SearchDec = new SearchDecorator(SearchNode, this, "Search Decorator");
        ChaseDecorator ChaseDec = new ChaseDecorator(ChaseNode, this, "Chase Decorator");
        root.AddChild(CombatDec);
        root.AddChild(SearchDec);
        root.AddChild(ChaseDec);
    }

    public virtual new void Update()
    {
        base.Update();

        if (PerceptionSystem())
            AINavAgent.speed = 10;
        else
            AINavAgent.speed = 5;
        if (search.SearchPoints.Count == 0)
            CreateSearchPoints();
        RootNode.Execute();
    }
    public virtual new void Restart()
    {
        base.Restart();
        AINavAgent = GetComponent<NavMeshAgent>();
        DebugInfo = new DebugInformation(this);
        Selector root = new Selector(this, "Root Node");
        RootNode = root;
        search = new Search();
        brain = new BrainInformation();
        CreateSearchPoints();
        BT_Combat CombatNode = new BT_Combat(this, "Combat Node");
        BT_Search SearchNode = new BT_Search(this, "Search Node");
        BT_Chase ChaseNode = new BT_Chase(this, "Chase Node");
        CombatDecorator CombatDec = new CombatDecorator(CombatNode, this, "Combat Decorator");
        SearchDecorator SearchDec = new SearchDecorator(SearchNode, this, "Search Decorator");
        ChaseDecorator ChaseDec = new ChaseDecorator(ChaseNode, this, "Chase Decorator");
        root.AddChild(CombatDec);
        root.AddChild(SearchDec);
        root.AddChild(ChaseDec);
    }

    public void CreateSearchPoints()
    {
       search.SearchPoints = new List<GameObject>();
        GameObject sp = Resources.Load("SearchPoint") as GameObject;
        for (var i=0; i < 30; i ++)
        {
            GameObject go = Instantiate(sp, transform.position, transform.rotation);
            go.name = name +" Search Point :" + i;
            search.SearchPoints.Add(go);
        }
    }

    public void Reload()
    {

        StartCoroutine(combat.CurrentWeapon.Reload(inventory));
    }
    public Entity PerceptionSystem()
    {
        float ClosestDistance = float.MaxValue;
        foreach (var item in Physics.OverlapSphere(transform.position, brain.VisionDistance, LayerMask.GetMask("Enemy")))
        {
            if (item.gameObject != gameObject)
                if (Vector3.Distance(transform.position, item.transform.position) < ClosestDistance)
                {
                    brain.SeePlayer = true;
                    brain.PlayersLastKnownLocation = item.transform.position;
                    brain.PlayersTravelingDirection = item.transform.forward;
                    ClosestDistance = Vector3.Distance(transform.position, item.transform.position);
                    return item.GetComponent<Entity>();

                }
        }
        return null;
    }
        public Entity CombatSystem()
    {
        float ClosestDistance = float.MaxValue;
        foreach (var item in Physics.OverlapSphere(transform.position, brain.CombatDistance, LayerMask.GetMask("Enemy")))
        {
            if (item.gameObject != gameObject)
                if (Vector3.Distance(transform.position, item.transform.position) < ClosestDistance)
                {
                    brain.SeePlayer = true;
                    brain.PlayersLastKnownLocation = item.transform.position;
                    brain.PlayersTravelingDirection = item.transform.forward;
                    ClosestDistance = Vector3.Distance(transform.position, item.transform.position);
                    return item.GetComponent<Entity>();
                    
                }
        }
        return null;
    }
    public void MoveTo(Vector3 TargetPosition)
    {      
        AINavAgent.stoppingDistance = StopDistance;
        AINavAgent.destination = TargetPosition;
    }
}
