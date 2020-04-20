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
    public float SearchDistance = 500;

    public Grid CurrentSearchGrid;
    public List<Grid> SearchedGrids = new List<Grid>();

    public SearchInformation()
    {
        SearchLocation = new Vector3();
        SearchDistance = 500;
        CurrentSearchGrid = null;
        SearchedGrids = new List<Grid>();
    }
    public SearchInformation(float Distance)
    {
        SearchLocation = new Vector3();
        SearchDistance = Distance;
        SearchedGrids = new List<Grid>();
    }
}