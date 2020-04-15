using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        foreach (var item in Members)
        {
            if (item != me)
                return item;
        }
        return null;
    }
}