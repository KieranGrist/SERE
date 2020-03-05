using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
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
public class MapGrids : MonoBehaviour
{
    public int ZoomLevel;
    public Terrain terrain;
    public int GridSize = 1000;
    public float CenterPoint;
    public float TopX, BottomX;
    public float LeftY, RightY;
    public static MapGrids MG;
    public List<Grid> grids = new List<Grid>();

    // Start is called before the first frame update
    void Start()
    {
        MG = this;
    }
    private void OnDrawGizmos()
    {


        CenterPoint = terrain.terrainData.size.x / 2;
        TopX = CenterPoint - terrain.terrainData.size.x / 2;
        BottomX = CenterPoint + terrain.terrainData.size.x / 2;
        LeftY = CenterPoint + terrain.terrainData.size.z / 2;
        RightY = CenterPoint - terrain.terrainData.size.z / 2;
        grids.Clear();

        /*1: 4 = 1000
         *2: 6 = 100
         *3: 8 = 10
         *4: 10 = 1
         * 
         * 
         * 
         * */
        switch (ZoomLevel)
        {
            case 1:
                GridSize = 1000;


        
                int XGRID = 31;
                int YGRID = 50;
                for (var x = TopX + 500 ; x < BottomX; x+= GridSize)
                {
             
                    Handles.Label(new Vector3(x, 0, LeftY + 500), XGRID.ToString("00")) ;
                    XGRID++;
                }
                for (var y = LeftY - 500; y > RightY; y -= GridSize)
                {
                    Handles.Label(new Vector3(TopX - 1000, 0, y), YGRID.ToString("00"));
                    YGRID--;

                }
                 XGRID = 31;
                 YGRID = 50;

                for (var x = TopX + 500; x < BottomX; x += GridSize)
                {
                    YGRID = 50;
                    for (var y = LeftY - 500; y > RightY; y -= GridSize)
                    {


                
                        Gizmos.DrawWireCube(new Vector3(x, 0, y), new Vector3(GridSize, GridSize, GridSize));

                        YGRID--;
                        grids.Add(new Grid(int.Parse(XGRID.ToString() + YGRID.ToString()), new Vector3(x, 0, y)));
                    }
                    XGRID++;
                }
                break;

            case 2:
                GridSize = 100;

                 XGRID = 310;
                 YGRID = 509;
                for (var x = TopX + 50; x < BottomX; x += GridSize)
                {

                    Handles.Label(new Vector3(x, 0, LeftY + 50), XGRID.ToString("000"));
                    XGRID++;
                }
                for (var y = LeftY - 50; y > RightY; y -= GridSize)
                {
                    Handles.Label(new Vector3(TopX - 100, 0, y), YGRID.ToString("000"));
                    YGRID--;

                }
                XGRID = 310;
                YGRID = 509;

                for (var x = TopX + 50; x < BottomX; x += GridSize)
                {
                    YGRID = 509;
                    for (var y = LeftY - 50; y > RightY; y -= GridSize)
                    {


                
                        Gizmos.DrawWireCube(new Vector3(x, 0, y), new Vector3(GridSize, GridSize, GridSize));

                        YGRID--;
                        grids.Add(new Grid(int.Parse(XGRID.ToString() + YGRID.ToString()), new Vector3(x, 0, y)));
                    }
                    XGRID++;
                }
                break;

            case 3:
                GridSize = 10;
                YGRID = 0111;
                XGRID = 0111;
                for (var x = TopX + 50; x < BottomX; x += GridSize)
                {
                    XGRID = 0111;
                    for (var y = LeftY - 50; y > RightY; y -= GridSize)
                    {


                        Handles.Label(new Vector3(x, 0, y), YGRID.ToString("0000") + XGRID.ToString("0000"));
                        Gizmos.DrawWireCube(new Vector3(x, 0, y), new Vector3(GridSize, GridSize, GridSize));

                        XGRID++;
                        grids.Add(new Grid(int.Parse(YGRID.ToString() + XGRID.ToString()), new Vector3(x, 0, y)));
                    }

                    YGRID++;
                }
                break;

            case 4:
                GridSize = 1;
                YGRID = 01;
                XGRID = 01;
                for (var x = TopX + 50; x < BottomX; x += GridSize)
                {
                    XGRID = 01;
                    for (var y = LeftY - 50; y > RightY; y -= GridSize)
                    {


                        Handles.Label(new Vector3(x, 0, y), YGRID.ToString("00000") + XGRID.ToString("00000"));
                        Gizmos.DrawWireCube(new Vector3(x, 0, y), new Vector3(GridSize, GridSize, GridSize));

                        XGRID++;
                        grids.Add(new Grid(int.Parse(YGRID.ToString() + XGRID.ToString()), new Vector3(x, 0, y)));
                    }
                    YGRID++;
                }
                break;

        }

    }
    

    // Update is called once per frame
    void Update()
    {
        MG = this;
        ZoomLevel = Mathf.Clamp(ZoomLevel, 1, 4);
    }
}
