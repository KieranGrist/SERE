using UnityEngine;
using UnityEngine.UI;
using MLAgents;
using MLAgents.Sensors;
using MLAgents.SideChannels;
[RequireComponent(typeof(Rigidbody))]
public class LearningSoldier : LearningAgent
{

    [Header("Entity")]
    public Side Affiliation = Side.Friendly;
    public Inventory inventory;
    [Header("Movement")]
    public Vector3 TravelingDirection;
    [Header("Combat")]
    public Combat combat = new Combat();
    public BrainInformation brain;


    public EntityStats entityStats;
    public ExtractionArea MyArea;


}