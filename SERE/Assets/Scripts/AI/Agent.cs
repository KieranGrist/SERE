using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public abstract class Agent : Entity
{
    
    public NavMeshAgent AINavAgent;
    [Header("Search")]
    [Tooltip("Center of search ")]
    public Vector3 SearchLocation;
    [Tooltip("Distance to search ")]
    public float SearchDistance;
    [Tooltip("How long the Agent should search the area for")]
    public float MaxSearchTime;


    float CurrentSearchTime;
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
}
