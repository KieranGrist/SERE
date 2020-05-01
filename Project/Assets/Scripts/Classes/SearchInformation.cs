using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SearchInformation
{
    [Header("Search")]
    [Tooltip("Center of search ")]
    public Vector3 SearchLocation;
    [Tooltip("Distance to search ")]
    public float SearchDistance =200;

    public Grid CurrentSearchGrid;

    public SearchInformation()
    {
        SearchLocation = new Vector3();
        SearchDistance = 200;
        CurrentSearchGrid = null;
    }
    public SearchInformation(float Distance)
    {
        SearchLocation = new Vector3();
        SearchDistance = Distance;
    }
}