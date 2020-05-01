using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class BrainInformation
{

    [Header("Brain Information")]
    public Entity Enemy = null;
    public Vector3 EnemyTravelingDirection, EnemyLastPosition;

    public ExtractionPoint extractionPoint = null;
    public float DistanceToTeamLeader;

    public bool Hunting, Combat, Searching;

    public bool HasSensedPlayer = false;

    public bool SeePlayer = false;


    public BrainInformation()
    {
        Enemy = null;
        EnemyTravelingDirection = new Vector3();
        EnemyLastPosition = new Vector3();

        extractionPoint = null;
        DistanceToTeamLeader = 0;

        Hunting = false;
        Combat = false;
        Searching = false;
        HasSensedPlayer = false;        
        SeePlayer = false;

    }
    public bool CheckSenses()
    {
        if (SeePlayer )
            return true;
        return false;
    }

    public void UpdateEnemy(Entity enemy)
    {
        this.Enemy = enemy;
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
        if (Enemy || SeePlayer )
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
