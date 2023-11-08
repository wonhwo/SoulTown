using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager levelManager;
    [SerializeField]
    public DivideSpace divideSpace;
    [SerializeField]
    public MakeRandomMap makeRandomMap;
    [SerializeField]
    public player player;
    [SerializeField]
    public SpreadTilemap spreadTilemap;
    [SerializeField]
    public Spawner spawner;




}