using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public abstract class Agent : Entity 
{

    public NavMeshAgent AINavAgent;
    [Header("Search")]
    public Vector3 SearchLocation;
    public float SearchDistance;
    public float MaxSearchTime;
    public float VisionDistance = 500;
    public float CombatDistance = 100;
    float CurrentSearchTime;
    [Header("Brain Information")]
    public Vector3 PlayersLastKnownLocation;
    public Vector3 PlayersTravelingDirection;





    [Header("Radio")]
    public Radio AIRadio;
}
