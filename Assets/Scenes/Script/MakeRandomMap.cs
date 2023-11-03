using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using TMPro;
using UnityEditor.Build.Reporting;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;
// 방을 만들고 방을 기준으로 복도를 만들고 벽을 만드는 클래스
public class MakeRandomMap : MonoBehaviour
{
    //방을만드는 변수들
    [SerializeField]
    private int distance;
    [SerializeField]
    private int minRoomWidth;
    [SerializeField]
    private int minRoomHeight;
    //나눠진 공간들의 리스트에 스페이스 리스트를 가져오는 변수
    [SerializeField]
    private DivideSpace divideSpace;
    //바닥파일과 벽타일 깔기
    [SerializeField]
    private SpreadTilemap spreadTilemap;
    //플레이어
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private GameObject entrance;
    [SerializeField]
    private Tilemap tilemap;
    [SerializeField]
    private TileBase tile;

    private HashSet<Vector2Int> floor;
    private HashSet<Vector2Int> wall;
    private HashSet<Vector2Int> corridor;
    HashSet<Vector2Int> commonTiles;
    //방의 x,y좌표
    List<Vector2> a = new List<Vector2>();
    //방의 중심 좌표
    List<Vector2> b = new List<Vector2>();
    private void Start()
    {
        StartRandomMap();
    }
    //렌덤 맵 실행 함수
    public void StartRandomMap()
    {
        //모든 타일 제거
        spreadTilemap.ClearAllTiles();
        divideSpace.totalSpace = new RectangleSpace(new Vector2Int(0, 0), divideSpace.totalWidth, divideSpace.totalHeight);
        divideSpace.spaceList = new List<RectangleSpace>();
        floor = new HashSet<Vector2Int>();
        wall = new HashSet<Vector2Int>();
        corridor = new HashSet<Vector2Int>();
        commonTiles = new HashSet<Vector2Int>(wall);
        //스페이스 리스트 생성
        divideSpace.DivideRoom(divideSpace.totalSpace);
        //방, 복도, 벽 좌표 저장
        MakeRandomRooms();
        MakeCorridors();
        corridor.ExceptWith(floor);
        commonTiles.IntersectWith(corridor);
        floor.UnionWith(corridor);
        MakeWall();
        findmapping();
        //타일 깔기
        spreadTilemap.SpreadFloorTilemap(floor);
        spreadTilemap.SpreadWallTilemap(wall);
        spreadTilemap.SpreadCorridorTilemap(corridor);


        //player.transform.position = (Vector2)divideSpace.spaceList[0].Center();
        //entrance.transform.position = (Vector2)divideSpace.spaceList[0].Center();
    }
    private void findmapping()
    {
        List<List<Vector2Int>> multiDimensionalList = new List<List<Vector2Int>>();
        List<Vector2Int> currentGroup = new List<Vector2Int>();

        foreach (Vector2Int tile in corridor)
        {
            if (currentGroup.Count == 0)
            {
                // 첫 좌표인 경우 새로운 그룹 시작
                currentGroup.Add(tile);
            }
            else
            {
                Vector2Int lastTile = currentGroup[currentGroup.Count - 1];
                // 변동이 발생하는 조건 확인 (예: x 또는 y가 1 이상 차이)
                if (Mathf.Abs(tile.x - lastTile.x) > 1 || Mathf.Abs(tile.y - lastTile.y) > 1)
                {
                    // 새로운 그룹 시작
                    multiDimensionalList.Add(currentGroup);
                    currentGroup = new List<Vector2Int>();
                }
                currentGroup.Add(tile);
            }
        }

        // 마지막 그룹 추가
        if (currentGroup.Count > 0)
        {
            multiDimensionalList.Add(currentGroup);
        }

        // 각 그룹에 접근하여 출력 또는 다른 작업 수행
        for (int i = 0; i < multiDimensionalList.Count; i++)
        {
            Vector2Int firstTile = multiDimensionalList[i][0]; // i번째 그룹의 첫 번째 좌표
            Vector2Int lastTile = multiDimensionalList[i][multiDimensionalList[i].Count - 1]; // i번째 그룹의 마지막 좌표
            spreadTilemap.SpreadDoorTilemap(firstTile,lastTile);
        }
    }

    public Vector2 GetRandomSpawnPosition(int index)
    {
        // 중심 좌표를 기준으로 가로 길이의 절반과 세로 길이의 절반 계산
        float halfWidth = a[index].x / 2;
        float halfHeight = a[index].y / 2;

        // 랜덤한 X 및 Y 좌표 생성
        float randomX = UnityEngine.Random.Range(-halfWidth, halfWidth);
        float randomY = UnityEngine.Random.Range(-halfHeight, halfHeight);

        // 중심 좌표에 더하여 최종 스폰 위치 계산
        Vector2 spawnPosition = b[index] + new Vector2(randomX, randomY);

        return spawnPosition;
    }
    //방을 만드는 함수
    private void MakeRandomRooms()
    {
        //스페이서에서 모든 스페이스 리스트 가져오기
        foreach (var space in divideSpace.spaceList){
            HashSet<Vector2Int> positions = MakeRandomRectangleRoom(space);
            floor.UnionWith(positions);
            b.Add(space.Center());
            //플로어에 좌표 추가 UnionWith 합집합
        }
    }
    //방의 넓이 정하는 함수(취소 길이 와 최대 길이를 기준으로)
    //공간의 중심 찾기
    
    private HashSet<Vector2Int> MakeRandomRectangleRoom(RectangleSpace space)
    {
        HashSet<Vector2Int> positions = new HashSet<Vector2Int>();
        int width = UnityEngine.Random.Range(minRoomWidth, space.width + 1 - distance * 2);
        int height = UnityEngine.Random.Range(minRoomHeight, space.height + 1 - distance * 2);
        a.Add(new Vector2(width, height));
        for (int i = space.Center().x - width / 2; i <= space.Center().x + width / 2; i++)
        {
            for (int j = space.Center().y - height / 2; j < space.Center().y + height / 2; j++)
            {
                positions.Add(new Vector2Int(i, j));
            }
        }


        return positions;
    }
    private void MakeCorridors()
    {
        List<Vector2Int> tempCenters = new List<Vector2Int>();
        foreach (var space in divideSpace.spaceList)
        {
            tempCenters.Add(new Vector2Int(space.Center().x, space.Center().y));
        }
        Vector2Int nextCenter;
        Vector2Int currentCenter = tempCenters[0];
        tempCenters.Remove(currentCenter);
        while (tempCenters.Count != 0)
        {
            nextCenter = ChooseShortestNextCorridor(tempCenters, currentCenter);
            MakeOneCorridor(currentCenter, nextCenter);
            currentCenter = nextCenter;
            tempCenters.Remove(currentCenter);
        }
        
    }
    private Vector2Int ChooseShortestNextCorridor(List<Vector2Int> tempCenters,Vector2Int previousCenter)
    {
        int n = 0;
        float minLength = float.MaxValue;
        for (int i = 0; i < tempCenters.Count; i++)
        {
            if (Vector2.Distance(previousCenter, tempCenters[i])< minLength)
            {
                minLength = Vector2.Distance(previousCenter, tempCenters[i]);
                n =i;
            }
        }


        return tempCenters[n];
    }
    private void MakeOneCorridor(Vector2Int currentCenter, Vector2Int nextCenter)
    {
        Vector2Int current = new Vector2Int(currentCenter.x, currentCenter.y);
        Vector2Int next = new Vector2Int(nextCenter.x, nextCenter.y);
        corridor.Add(current); // 복도의 좌표를 corridor에 추가
        while (current.x != next.x)
        {
            if (current.x < next.x)
            {
                current.x += 1;
                corridor.Add(current); // 복도의 좌표를 corridor에 추가
            }
            else
            {
                current.x -= 1;
                corridor.Add(current); // 복도의 좌표를 corridor에 추가
            }
        }
        while (current.y != next.y)
        {
            if (current.y < next.y)
            {
                current.y += 1;
                corridor.Add(current); // 복도의 좌표를 corridor에 추가
            }
            else
            {
                current.y -= 1;
                corridor.Add(current); // 복도의 좌표를 corridor에 추가
            }
        }
    }



    //벽을 만드는 함수
    private void MakeWall()
    {
        foreach(Vector2Int tile in floor)
        {
            HashSet<Vector2Int> boundary = Make3X3Square(tile);
            boundary.ExceptWith(floor);
            if(boundary.Count != 0)
            {
                wall.UnionWith(boundary);
            }
        }
    }
    private HashSet<Vector2Int> Make3X3Square(Vector2Int tile)
    {
        HashSet<Vector2Int> boundary = new HashSet<Vector2Int>();
        for(int i=tile.x -1; i<=tile.x + 1; i++)
        {
            for (int j = tile.y -1; j<= tile.y + 1; j++)
            {
                boundary.Add(new Vector2Int(i, j));
            }
        }
        return boundary;
    }
}
