using MoreMountains.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;
using UnityEngine.WSA;

//바닥, 벽을 설치하는 스크립트
public class SpreadTilemap : MonoBehaviour
{
    [SerializeField]
    private MakeRandomMap makeRandom;
    //SerializeField 타일맵 프라이빗 변수를 인스펙터 창에서 관리 할 수있게 해주는 함수
    // 타일맵 오브젝트
    [SerializeField]
    private Tilemap floor;
    [SerializeField]
    private Tilemap wall;
    [SerializeField]
    private Tilemap corridor;
    [SerializeField]
    private Tilemap door;
    //[SerializeField]
    //private Tilemap trriger;
    //사용할 타일 에셋
    [SerializeField]
    private TileBase[] floorTiles;
    [SerializeField]
    private TileBase wallTile;
    [SerializeField]
    private TileBase corridorTile;
    [SerializeField]
    private TileBase doorTile;
    [SerializeField]
    //private TileBase noneTile;
    public LevelManager levelManager = new LevelManager();

    // 각 타일의 배치 확률 가중치
    [SerializeField]
    private int[] tileWeights;
    //HashSet << 집합 관련 함수
    //바닥에 타일을 까는 함수
    public void SpreadFloorTilemap(HashSet<Vector2Int> positions)
    {
        SpreadRandomFloorTile(positions);
    }
    //벽을 까는 함수
    public void SpreadWallTilemap(HashSet<Vector2Int> positions)
    {
        SpreadTile(positions, wall, wallTile);
    }
    public void SpreadCorridorTilemap(HashSet<Vector2Int> positions)
    {
        SpreadTile(positions, corridor, corridorTile);
    }
    public void SpreadDoorTilemap(Vector2Int firstTile, Vector2Int lastTile)
    {
        door.SetTile((Vector3Int)firstTile,doorTile);
        door.SetTile((Vector3Int)lastTile, doorTile);
    }
    /*public void SpreadTrrigerTilemap()
    {
        List<List<Vector2Int>> roomCoordinates = new List<List<Vector2Int>>(makeRandom.CalculateRoomArea());

        // 부모 타일맵 (trriger)을 가져옴
        Tilemap parentTilemap = trriger.GetComponent<Tilemap>();

        // 각 roomCoordinates의 좌표를 순차적으로 자식 타일맵에 추가
        for (int i = 0; i < roomCoordinates.Count; i++)
        {
            // 자식 타일맵을 생성하고 부모 타일맵에 추가
            GameObject childTilemapObject = new GameObject("ChildTilemap" + i);
            childTilemapObject.transform.SetParent(trriger.transform);

            Tilemap childTilemap = childTilemapObject.AddComponent<Tilemap>();

            // 추가된 자식 타일맵에 타일 렌더러를 추가
            TilemapRenderer childRenderer = childTilemapObject.AddComponent<TilemapRenderer>();

            // Tilemap Collider 2D 추가
            TilemapCollider2D tilemapCollider = childTilemapObject.AddComponent<TilemapCollider2D>();
            tilemapCollider.isTrigger = true;

            // 태그 설정
            childTilemapObject.tag = "Room"+i.ToString();

            foreach (Vector2Int coordinate in roomCoordinates[i])
            {
                // 좌표를 자식 타일맵에 추가
                childTilemap.SetTile((Vector3Int)coordinate, noneTile);
            }
        }
    }*/


    //장애물 함수
    private void SpreadTile(HashSet<Vector2Int> positions, Tilemap tilemap, TileBase tile)
    {
        foreach (var position in positions)
        {
            tilemap.SetTile((Vector3Int)position, tile);
        }
    }
    public void SpreadRandomFloorTile(HashSet<Vector2Int> positions)
    {
        foreach (Vector2Int position in positions)
        {
            TileBase selectedTile = GetRandomTileWithWeight(floorTiles, tileWeights);

                floor.SetTile((Vector3Int)position, selectedTile);
        }
    }

    // 가중치를 고려한 확률적으로 타일을 선택하는 함수
    private TileBase GetRandomTileWithWeight(TileBase[] tiles, int[] weights)
    {
        int totalWeight = 0;
        foreach (int weight in weights)
        {
            totalWeight += weight;
        }

        int randomValue = UnityEngine.Random.Range(0, totalWeight + 1);
        int cumulativeWeight = 0;

        for (int i = 0; i < tiles.Length; i++)
        {
            cumulativeWeight += weights[i];
            if (randomValue < cumulativeWeight)
            {
                return tiles[i];
            }
        }

        // 여기까지 오면 예외 처리 또는 기본 타일 반환 가능
        return tiles[0];
    }
    public void ClearAllTiles()
    {
        floor.ClearAllTiles();
        wall.ClearAllTiles();
    }

}