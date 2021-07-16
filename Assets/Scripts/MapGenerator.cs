using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEditor;

public class MapGenerator : MonoBehaviour
{
     [Range(0,100)]
    public int iniChance;
    [Range(1,8)]
    public int birthLimit;
    [Range(1,8)]
    public int deathLimit;
    [Range(1,10)]
    public int numR;
    private int count = 0;


    public Vector2 spawn;
    public Vector2 end;
    public GameObject hero;

    private int[,] terrainMap;
    public Vector3Int tmpSize;
    public Tilemap wall;
    public Tilemap props;
    public Tilemap floor;
    public Tile[] walls;
    public Tile[] floorTiles;
    public Tile[] corners; // 0 = north, 1 = north-east, 2 = east, 3 = south-east, 4 = south, 5 = south-west, 6 = west, 7 = north-west, 8= North-east-south, 9= East-south-west, 10 =South-west-north, 11= West-North-East

    int width;
    int height;

    AstarPath pathing;

    void Awake()
    {
        pathing = GetComponent<AstarPath>();
    }

    public void spawnMap(){
        spawn = new Vector2(-0.5f*tmpSize.x+3, 0);
        end = new Vector2(0.5f*tmpSize.x, 0);
        hero.transform.position = spawn;
        clearMap(true);
        doSim(numR);
    }

    public void doSim(int nu)
    {
        clearMap(false);
        width = tmpSize.x;
        height = tmpSize.y;

        if (terrainMap==null)
            {
            terrainMap = new int[width, height];
            initPos();
            }


        for (int i = 0; i < nu; i++)
        {
            terrainMap = genTilePos(terrainMap);
        }
        
        cleanMap(terrainMap);

        for (int x = 0; x < width; x++)
        {
            //  y-1 = north / y+1 = south / x-1 = east / x+1 = West
            for (int y = 0; y < height; y++)
            {
               
                if (terrainMap[x, y] == 1 )
                    wall.SetTile(new Vector3Int(-x + width / 2, -y + height / 2, 0), walls[Random.Range(0,walls.Length)]);
                else { 
                    //props
                }
                
                floor.SetTile(new Vector3Int(-x + width / 2, -y + height / 2, 0), floorTiles[Random.Range(0,floorTiles.Length)]);
                    
                if(x > 0 && y > 0 && x < width-1 && y < height-1){
                    
                    if(terrainMap[x, y-1] == 1 && terrainMap[x-1,y] == 1 && terrainMap[x,y+1] == 1){
                        //North-east-south
                        //floor.SetTile(new Vector3Int(-x + width / 2, -y + height / 2, 0), corners[6]);
                    }
                    else if(terrainMap[x+1, y] == 1 && terrainMap[x-1,y] == 1 && terrainMap[x,y+1] == 1){
                        // East-south-west
                    }
                    else if(terrainMap[x+1, y] == 1 && terrainMap[x,y-1] == 1 && terrainMap[x,y+1] == 1){
                        //South-west-north
                    }
                    else if(terrainMap[x+1, y] == 1 && terrainMap[x-1,y] == 1 && terrainMap[x,y-1] == 1){
                        //West-North-East
                        floor.SetTile(new Vector3Int(-x + width / 2, -y + height / 2, 0), corners[7]);
                    }
                    else if(terrainMap[x, y-1] == 1 && terrainMap[x-1,y] == 1){
                        //North-east
                        floor.SetTile(new Vector3Int(-x + width / 2, -y + height / 2, 0), corners[1]);    
                    }
                    else if(terrainMap[x, y-1] == 1 && terrainMap[x+1,y] == 1){
                        //North-west
                        floor.SetTile(new Vector3Int(-x + width / 2, -y + height / 2, 0), corners[7]);    
                    }
                    else if(terrainMap[x, y+1] == 1 && terrainMap[x-1,y] == 1){
                        //South-east
                        floor.SetTile(new Vector3Int(-x + width / 2, -y + height / 2, 0), corners[3]);    
                    }
                    else if(terrainMap[x, y+1] == 1 && terrainMap[x+1,y] == 1){
                        //South-west
                        floor.SetTile(new Vector3Int(-x + width / 2, -y + height / 2, 0), corners[5]);    
                    }
                    else if(terrainMap[x, y+1] == 1 && terrainMap[x+1,y] == 1){
                        //South-west
                        floor.SetTile(new Vector3Int(-x + width / 2, -y + height / 2, 0), corners[5]);    
                    }
                    else if(terrainMap[x, y-1] == 1){
                        //North
                        floor.SetTile(new Vector3Int(-x + width / 2, -y + height / 2, 0), corners[0]);    
                    }
                    else if(terrainMap[x-1, y] == 1){
                        //east
                        floor.SetTile(new Vector3Int(-x + width / 2, -y + height / 2, 0), corners[2]);    
                    }
                    else if(terrainMap[x, y+1] == 1){
                        //south
                        floor.SetTile(new Vector3Int(-x + width / 2, -y + height / 2, 0), corners[4]);    
                    }
                    else if(terrainMap[x+1, y] == 1){
                        //west
                        floor.SetTile(new Vector3Int(-x + width / 2, -y + height / 2, 0), corners[6]);    
                    }
                }
                
            }
        }
        pathing.Scan();
    }

    void cleanMap(int[,] map){
       for(int x = 0; x < 10; x++){
          for(int y = -2; y<2; y++){
                map[x,Mathf.RoundToInt(height/2)+y] = 0;
                map[width-1-x,Mathf.RoundToInt(height/2)+y] = 0;
            }
        }
        
    }

    public void initPos()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                terrainMap[x, y] = Random.Range(1, 101) < iniChance ? 1 : 0;
            }

        }

    }


    public int[,] genTilePos(int[,] oldMap)
    {
        int[,] newMap = new int[width,height];
        int neighb;
        BoundsInt myB = new BoundsInt(-1, -1, 0, 3, 3, 1);


        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                neighb = 0;
                foreach (var b in myB.allPositionsWithin)
                {
                    if (b.x == 0 && b.y == 0) continue;
                    if (x+b.x >= 0 && x+b.x < width && y+b.y >= 0 && y+b.y < height)
                    {
                        neighb += oldMap[x + b.x, y + b.y];
                    }
                    else
                    {
                        neighb++;
                    }
                }

                if (oldMap[x,y] == 1)
                {
                    if (neighb < deathLimit) newMap[x, y] = 0;

                        else
                        {
                            newMap[x, y] = 1;

                        }
                }

                if (oldMap[x,y] == 0)
                {
                    if (neighb > birthLimit) newMap[x, y] = 1;

                else
                {
                    newMap[x, y] = 0;
                }
                }

            }

        }



        return newMap;
    }


	void Update () {

        if (Input.GetMouseButtonDown(1))
            {
            //clearMap(true);
        }



        if (Input.GetMouseButton(2))
        {
            //SaveAssetMap();
            count++;
        }

    }


    public void SaveAssetMap()
    {
        string saveName = "tmapXY_" + count;
        var mf = GameObject.Find("Grid");

        if (mf)
        {
            var savePath = "Assets/" + saveName + ".prefab";
            if (PrefabUtility.CreatePrefab(savePath,mf))
            {
                EditorUtility.DisplayDialog("Tilemap saved", "Your Tilemap was saved under" + savePath, "Continue");
            }
            else
            {
                EditorUtility.DisplayDialog("Tilemap NOT saved", "An ERROR occured while trying to saveTilemap under" + savePath, "Continue");
            }


        }


    }

    public void clearMap(bool complete)
    {

        wall.ClearAllTiles();
        floor.ClearAllTiles();
        if (complete)
        {
            terrainMap = null;
        }


    }
}
