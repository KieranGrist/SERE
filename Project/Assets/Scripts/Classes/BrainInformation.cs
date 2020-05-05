using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class BrainInformation
{
    public ExtractionPoint extractionPoint = null;
    public float DistanceToTeamLeader;

    public bool Hunting, Combat, Searching;

    public bool HasSensedPlayer = false;

    public bool SeePlayer = false;


    public BrainInformation()
    {


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

   
    public bool FoundExtractionPoint()
    {
        if (extractionPoint)
            return true;
        return false;
    }
}
