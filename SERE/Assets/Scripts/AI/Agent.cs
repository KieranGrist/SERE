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
    public List<Agent> Members = new List<Agent>();


    public Team()
    {
        TeamName = "1 - 1";
        TeamID = 0;
        Members = new List<Agent>();
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

    public Team AgentsTeam = new Team();

    [Header("Agent Stats")]
    public NavMeshAgent AINavAgent;
    public float Mass, MaxVelocity, MaxForce;
    public float StopDistance = 1;
    public Vector3 velocity;
    public Vector3 MoveToLocation;
    bool IsMoving;

    [Header("Search")]
    [Tooltip("Center of search ")]
    public Vector3 SearchLocation;
    [Tooltip("Distance to search ")]
    public float SearchDistance;
    [Tooltip("How long the Agent should search the area for")]
    public float MaxSearchTime;
    float CurrentSearchTime;
    public bool SearchInsideCicle;
    public List<Grid> SearchedGrids = new List<Grid>();
    public List<GameObject> SearchPoints = new List<GameObject>();

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
    public float SmellPower = 3;

    [Header("Radio")]
    public Radio AIRadio;
    public bool SquadTransmitRadio;



    public void MoveTo(Vector3 TargetPosition)
    {
        AINavAgent.stoppingDistance = StopDistance;
        var desiredVelocity = TargetPosition - transform.position;
        desiredVelocity = desiredVelocity.normalized * MaxVelocity;

        var steering = desiredVelocity - velocity;
        steering = Vector3.ClampMagnitude(steering, MaxForce);
        steering /= Mass;

        velocity = Vector3.ClampMagnitude(velocity + steering, MaxVelocity);
        transform.position += velocity * Time.deltaTime;
        transform.forward = velocity.normalized;
    }
}
