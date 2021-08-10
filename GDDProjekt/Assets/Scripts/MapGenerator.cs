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

    public Vector2 spawn;
    public Vector2 end;
    public GameObject hero;

    private int[,] terrainMap;
    public List<Vector3> freeSpots;
    public Vector3Int tmpSize;
    public Tilemap wall;
    public Tilemap floor;
    public Tilemap innerObs;
    public Tilemap marked;
    public Tile[] walls;
    public Tile[] innerWalls;
    public Tile[] floorTiles;
    public Tile[] corners; // 0 = north, 1 = north-east, 2 = east, 3 = south-east, 4 = south, 5 = south-west, 6 = west, 7 = north-west, 8= North-east-south, 9= East-south-west, 10 =South-west-north, 11= West-North-East
    public Tile marker;
    Dictionary<int, List<int>> knownSpots = new Dictionary<int, List<int>>();
    public int propAmount = 10;
    public int propLowAmount = 3;
    public GameObject[] propsHighDensity;
    public GameObject[] propsLowDensity;

    int xStart;
    int xEnd;
    int yStart;
    int yEnd;

    int width;
    int height;
    int monsters;
    AstarPath pathing;
    Manager manager;
    MonsterTable monsterTable;

    void Awake()
    {
        pathing = GetComponent<AstarPath>();
        manager = GetComponent<Manager>();
        monsterTable = GetComponent<MonsterTable>();
        
    }

    public void spawnMap(string[] settings){
        iniChance = int.Parse(settings[0]);
        birthLimit = int.Parse(settings[1]);
        deathLimit = int.Parse(settings[2]);
        spawn = new Vector2(-0.5f*tmpSize.x+3, 0);
        end = new Vector2(0.5f*tmpSize.x, 0);
        hero.transform.position = spawn;
        clearMap(true);
        createMap(int.Parse(settings[3]));
    }

    public void createMap(int nu)
    {
        clearMap(false);
        width = tmpSize.x;
        height = tmpSize.y;
        xStart = 3;
        xEnd = width-3;
        yStart = Mathf.RoundToInt(height/2);
        yEnd = Mathf.RoundToInt(height/2);

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
        knownSpots.Clear();
        if(!checkBrokenMap(terrainMap) && letsGo(terrainMap)){
            for (int x = 0; x < width; x++)
            {
                //  y-1 = north / y+1 = south / x-1 = east / x+1 = West
                for (int y = 0; y < height; y++)
                {
                    
                    if (terrainMap[x, y] == 1 ){
                        if(x < 2 || y < 2 || x > width-3 || y > height-3){
                            wall.SetTile(new Vector3Int(-x + width / 2, -y + height / 2, 0), walls[Random.Range(0,walls.Length)]);
                        }
                        else{
                            innerObs.SetTile(new Vector3Int(-x + width / 2, -y + height / 2, 0), innerWalls[Random.Range(0,innerWalls.Length)]);
                        }
                    } 
                    else {
                        freeSpots.Add(new Vector3Int(-x + width / 2, -y + height / 2, 0));    
                    }
                    
                    if(terrainMap[x,y] == 3){
                        //wall.SetTile(new Vector3Int(-x + width / 2, -y + height / 2, 0), marker);
                    } 
                    floor.SetTile(new Vector3Int(-x + width / 2, -y + height / 2, 0), floorTiles[Random.Range(0,floorTiles.Length)]);
                        
                    if(x > 0 && y > 0 && x < width-1 && y < height-1){
                        
                        if(terrainMap[x, y-1] == 1 && terrainMap[x-1,y] == 1 && terrainMap[x,y+1] == 1){
                            //North-east-south
                            floor.SetTile(new Vector3Int(-x + width / 2, -y + height / 2, 0), corners[8]);
                        }
                        else if(terrainMap[x+1, y] == 1 && terrainMap[x-1,y] == 1 && terrainMap[x,y+1] == 1){
                            // East-south-west
                            floor.SetTile(new Vector3Int(-x + width / 2, -y + height / 2, 0), corners[9]);
                        }
                        else if(terrainMap[x+1, y] == 1 && terrainMap[x,y-1] == 1 && terrainMap[x,y+1] == 1){
                            //South-west-north
                            floor.SetTile(new Vector3Int(-x + width / 2, -y + height / 2, 0), corners[10]);
                        }
                        else if(terrainMap[x+1, y] == 1 && terrainMap[x-1,y] == 1 && terrainMap[x,y-1] == 1){
                            //West-North-East
                            floor.SetTile(new Vector3Int(-x + width / 2, -y + height / 2, 0), corners[11]);
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
            spawnProbs();
            spawnMonsters();
            pathing.Scan();
        }
        else{
            Debug.Log("MapError Detected");
            string[] newSpawn = {"15","2","2","2"};
            spawnMap(newSpawn);
        }
    }

    void cleanMap(int[,] map){
       for(int x = 1; x < 10; x++){
          for(int y = -2; y<2; y++){
                map[x,Mathf.RoundToInt(height/2)+y] = 0;
                map[width-1-x,Mathf.RoundToInt(height/2)+y] = 0;
            }
        }

        map[2, Mathf.RoundToInt(height/2)] = 3;
        map[width-2, Mathf.RoundToInt(height/2)] = 3;
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
    
    void spawnMonsters(){
        for(int i = 0; i<manager.monsterAmount; i++){
            int r = Random.Range(0,freeSpots.Count);
            GameObject monster = Instantiate(monsterTable.monsters[monsterTable.roll(manager.level*10, manager.level*100)],freeSpots[r],Quaternion.identity);
            manager.monstersInLevel.Add(monster);
            freeSpots.RemoveAt(r);
        }
        freeSpots.Clear();
    }

    void spawnProbs(){
        List<int> usedPlaces = new List<int>(); 
        for(int i = 0; i<propAmount; i++){
            int r = Random.Range(0,freeSpots.Count);
            if(i <= propLowAmount && !usedPlaces.Contains(r)){
                GameObject prop = Instantiate(propsLowDensity[Random.Range(0,propsLowDensity.Length)],freeSpots[r],Quaternion.identity);
                manager.props.Add(prop);
            } 
            else if(!usedPlaces.Contains(r)){
                GameObject prop = Instantiate(propsHighDensity[Random.Range(0,propsHighDensity.Length)],freeSpots[r],Quaternion.identity);
                manager.props.Add(prop);
            } 
            for(int j = -2; j<3; j++){
                usedPlaces.Add(r+j);
            }
        }
    }

    public void clearMap(bool complete)
    {
        innerObs.ClearAllTiles();
        wall.ClearAllTiles();
        floor.ClearAllTiles();
        marked.ClearAllTiles();
        if (complete)
        {
            terrainMap = null;
        }


    }

    bool checkBrokenMap(int[,] map){
        for (int x = 3; x < width-2; x++){
            int zeroCounts = 0;
            for (int y = 0; y < height; y++)
            {
                if(map[x,y] == 0){
                    zeroCounts++;
                    break;
                } 
            }
            if(zeroCounts == 0) return true; 
        }
        return false;
    }

    bool letsGo(int[,] map) {
        return go(map, xStart, yStart, xEnd, yEnd);
    }

    bool go (int[,] map, int x, int y, int xEnd, int yEnd){

        //this is for testing 
        marked.SetTile(new Vector3Int(-x + width / 2, -y + height / 2, 0), marker);

        if(knownSpots.ContainsKey(x)){
            if(knownSpots[x].Contains(y)) return false;
            else knownSpots[x].Add(y);
        } 
        else{
            knownSpots.Add(x, new List<int>(y));
        }  

        //Abbruch Bedingung

        if(x == xEnd && y == yEnd) return true;
        if(x >= width || y <= 0 || y >= height) return false;
        if(map[x,y] == 1) return false;
        

        //Rekursiver Abstieg
        if(go(map, x+1, y, xEnd, yEnd) == false)
            if(go(map, x, y+1, xEnd, yEnd) == false)
                return go(map, x, y-1, xEnd, yEnd);

        return true;
    }

}
