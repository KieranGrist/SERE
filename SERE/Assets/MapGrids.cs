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
    public float LeftX, RightX;
    public float TopZ, BottomZ;
    public List<Grid> grids = new List<Grid>();

    // Start is called before the first frame update
    void Start()
    {

    }
    private void OnDrawGizmos()
    {


        CenterPoint = terrain.terrainData.size.x / 2;
        LeftX = CenterPoint - terrain.terrainData.size.x / 2;
        RightX = CenterPoint + terrain.terrainData.size.x / 2;
        TopZ = CenterPoint + terrain.terrainData.size.z / 2;
        BottomZ = CenterPoint - terrain.terrainData.size.z / 2;
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


                int XGRID = 01;
                int ZGRID = 01;
                for (var x = LeftX + 500; x < RightX; x += GridSize)
                {
                    ZGRID = 01;
                    for (var z = TopZ - 500; z > BottomZ; z -= GridSize)
                    {


                        Handles.Label(new Vector3(x, 0, z), XGRID.ToString("0")  + ZGRID.ToString("0"));
                        Gizmos.DrawWireCube(new Vector3(x, 0, z), new Vector3(GridSize, GridSize, GridSize));
                    
                            ZGRID++;
                        grids.Add(new Grid(int.Parse(XGRID.ToString() + ZGRID.ToString()), new Vector3(x, 0, z)));
                    }
                    XGRID++;
                }
                break;

            case 2:
                GridSize = 100;
                 XGRID = 01;
                 ZGRID = 01;
                for (var x = LeftX + 50; x < RightX; x += GridSize)
                {
                    ZGRID = 01;
                    for (var z = TopZ - 50; z > BottomZ; z -= GridSize)
                    {

                       
                        Handles.Label(new Vector3(x, 0, z), XGRID.ToString("00") + ZGRID.ToString("00"));
                        Gizmos.DrawWireCube(new Vector3(x, 0, z), new Vector3(GridSize, GridSize, GridSize));

                        ZGRID++;
                        grids.Add(new Grid(int.Parse(XGRID.ToString() + ZGRID.ToString()), new Vector3(x, 0, z)));
                    }
                    XGRID++;
                }
                break;

            case 3:
                GridSize = 10;
                XGRID = 01;
                ZGRID = 01;
                for (var x = LeftX + 50; x < RightX; x += GridSize)
                {
                    ZGRID = 01;
                    for (var z = TopZ - 50; z > BottomZ; z -= GridSize)
                    {


                        Handles.Label(new Vector3(x, 0, z), XGRID.ToString("00") + ZGRID.ToString("00"));
                        Gizmos.DrawWireCube(new Vector3(x, 0, z), new Vector3(GridSize, GridSize, GridSize));

                        ZGRID++;
                        grids.Add(new Grid(int.Parse(XGRID.ToString() + ZGRID.ToString()), new Vector3(x, 0, z)));
                    }
                    XGRID++;
                }
                break;

            case 4:
                GridSize = 1;
                XGRID = 01;
                ZGRID = 01;
                for (var x = LeftX + 50; x < RightX; x += GridSize)
                {
                    ZGRID = 01;
                    for (var z = TopZ - 50; z > BottomZ; z -= GridSize)
                    {


                        Handles.Label(new Vector3(x, 0, z), XGRID.ToString("00") + ZGRID.ToString("00"));
                        Gizmos.DrawWireCube(new Vector3(x, 0, z), new Vector3(GridSize, GridSize, GridSize));

                        ZGRID++;
                        grids.Add(new Grid(int.Parse(XGRID.ToString() + ZGRID.ToString()), new Vector3(x, 0, z)));
                    }
                    XGRID++;
                }
                break;

        }

    }

    // Update is called once per frame
    void Update()
    {
        ZoomLevel = Mathf.Clamp(ZoomLevel, 1, 4);
    }
}
