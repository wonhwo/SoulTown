using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

//�ٴ�, ���� ��ġ�ϴ� ��ũ��Ʈ
public class SpreadTilemap : MonoBehaviour
{
    //SerializeField Ÿ�ϸ� �����̺� ������ �ν����� â���� ���� �� ���ְ� ���ִ� �Լ�
    // Ÿ�ϸ� ������Ʈ
    [SerializeField]
    private Tilemap floor;
    [SerializeField]
    private Tilemap wall;
    [SerializeField]
    private Tilemap corridor;
    //����� Ÿ�� ����
    [SerializeField]
    private TileBase[] floorTiles;
    [SerializeField]
    private TileBase wallTile;
    [SerializeField]
    private TileBase corridorTile;
    // �� Ÿ���� ��ġ Ȯ�� ����ġ
    [SerializeField]
    private int[] tileWeights;
    //HashSet << ���� ���� �Լ�
    //�ٴڿ� Ÿ���� ��� �Լ�
    public void SpreadFloorTilemap(HashSet<Vector2Int> positions)
    {
        SpreadRandomFloorTile(positions);
    }
    //���� ��� �Լ�
    public void SpreadWallTilemap(HashSet<Vector2Int> positions)
    {
        SpreadTile(positions, wall, wallTile);
    }
    public void SpreadCorridorTilemap(HashSet<Vector2Int> positions)
    {
        SpreadTile(positions, corridor, corridorTile);
    }


    //��ֹ� �Լ�
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

    // ����ġ�� ������ Ȯ�������� Ÿ���� �����ϴ� �Լ�
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

        // ������� ���� ���� ó�� �Ǵ� �⺻ Ÿ�� ��ȯ ����
        return tiles[0];
    }
    public void ClearAllTiles()
    {
        floor.ClearAllTiles();
        wall.ClearAllTiles();
    }

}