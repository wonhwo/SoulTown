using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
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
    private void OnTriggerEnter2D(Collider2D other)
    {

    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {

        }
    }
}
