using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.AI;
[System.Serializable]
public class Grid 
{
    public int ID;
    public Vector3 Location;
    public Grid(int id, Vector3 loc)
    {
        ID = id; Location = loc;
    }
}
[RequireComponent(typeof(NavMeshSurface))]
public class MapGrids : MonoBehaviour
{
    public Terrain terrain;
    public int GridSize = 1000;
    public float CenterPointx, CenterPointy;
    public float TopX, BottomX;
    public float LeftY, RightY;
    public List<Grid> grids = new List<Grid>();

  
    private void OnDrawGizmos()
    {
        CenterPointx = transform.position.x;
        CenterPointx += terrain.terrainData.size.x / 2;
        TopX = CenterPointx - terrain.terrainData.size.x / 2;
        BottomX = CenterPointx + terrain.terrainData.size.x / 2;
        CenterPointy = transform.position.z;
        CenterPointy += terrain.terrainData.size.z / 2; ;
        LeftY = CenterPointy + terrain.terrainData.size.z / 2;
        RightY = CenterPointy - terrain.terrainData.size.z / 2;
        grids.Clear();


        GridSize = 500;



        int XGRID = 31;
        int YGRID = 50;
        for (var x = TopX + 250; x < BottomX; x += GridSize)
        {
            Handles.Label(new Vector3(x, 0, LeftY + 250), XGRID.ToString("00"));
            XGRID++;
        }
        for (var y = LeftY - 250; y > RightY; y -= GridSize)
        {
            Handles.Label(new Vector3(TopX - 500, 0, y), YGRID.ToString("00"));
            YGRID--;

        }
        XGRID = 31;
        YGRID = 50;

        for (var x = TopX + 250; x < BottomX; x += GridSize)
        {
            YGRID = 50;
            for (var y = LeftY - 250; y > RightY; y -= GridSize)
            {



                Gizmos.DrawWireCube(new Vector3(x, 0, y), new Vector3(GridSize, GridSize, GridSize));

                YGRID--;
                grids.Add(new Grid(int.Parse(XGRID.ToString() + YGRID.ToString()), new Vector3(x, 0, y)));
            }
            XGRID++;
        }

    }
}
