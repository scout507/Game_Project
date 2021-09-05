using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterTable : MonoBehaviour
{
    public GameObject[] monsters;
    int total = 975;
    int[] monsterTable = {
        500,
        250,
        125,
        60,
        40
    };

    public int roll(int weight, int cap){
        int r = Random.Range(0,total)+weight;
        if(r > cap) r = cap;
        for(int i = 0; i<monsterTable.Length; i++){
            if(r < monsterTable[i]) return i;
            else r -= monsterTable[i];
        }
        return monsterTable.Length-1;
    }
}
