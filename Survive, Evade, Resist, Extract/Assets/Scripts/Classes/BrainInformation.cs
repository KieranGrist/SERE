using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class BrainInformation
{
    public string WhatIAmDoing = " I am not doing anything";
    [Header("Brain Information")]
    public Entity Enemy = null;
    public Vector3 EnemyTravelingDirection, EnemyLastPosition;

    public ExtractionPoint extractionPoint = null;
    public float DistanceToTeamLeader;

    public bool Hunting, Combat, Searching;

    public bool HasSensedPlayer = false;

    public bool AbillityToSmell = false;

    public bool HearPlayer = false;
    public bool SeePlayer = false;
    public bool SmellPlayer = false;

    public bool CheckSenses()
    {
        if (HearPlayer|| SeePlayer|| SmellPlayer)
        return true;
        return false;
    }
    public void UpdateEnemyInfo()
    {
        if (Enemy)
        {
            EnemyTravelingDirection = Enemy.transform.forward;
            EnemyLastPosition = Enemy.transform.position;
        }
    }
    public bool FoundEnemy()
    {
        if (Enemy||SeePlayer||HearPlayer||SmellPlayer)
            return true;
        return false;
    }
    public bool FoundExtractionPoint()
    {
        if (extractionPoint)
            return true;
        return false;
    }
}
