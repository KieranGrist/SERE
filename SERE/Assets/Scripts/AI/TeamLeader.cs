using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class TeamLeaderInformation
{ 

}
[System.Serializable]
public class TeamLeader : Agent
{
    public TeamLeaderInformation TeamInfo;
    public Radio TeamLeaderRadio = new ANPRC119();
    public TeamLeader():base()
    {
        TeamLeaderRadio = new ANPRC119();
        TeamLeaderRadio.Frequency = 5;
    }
}
