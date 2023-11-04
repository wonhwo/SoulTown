using MoreMountains.TopDownEngine;
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
    [SerializeField]
    public GameObject rectangleBox;

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
        //spreadTilemap.SpreadTrrigerTilemap();
        CreateAndPositionRectangle();

        //player.transform.position = (Vector2)divideSpace.spaceList[0].Center();
        //entrance.transform.position = (Vector2)divideSpace.spaceList[0].Center();
    }
    HashSet<Vector2Int> room;
    /*public List<List<Vector2Int>> CalculateRoomArea()
    {
        List<List<Vector2Int>> rooms = new List<List<Vector2Int>>(); // 모든 방의 좌표 목록을 저장하는 리스트

        for (int i = 0; i < a.Count; i++)
        {
            HashSet<Vector2Int> roomArea = new HashSet<Vector2Int>(); // 현재 방의 좌표 목록을 저장하는 HashSet
            Vector2 roomCenter = b[i];
            Vector2 roomSize = a[i];

            // 방의 왼쪽 위 모서리 좌표 계산
            Vector2 roomTopLeft = roomCenter - new Vector2(roomSize.x / 2, roomSize.y / 2);

            // 방의 넓이 좌표 추가
            for (int x = 0; x < roomSize.x; x++)
            {
                for (int y = 0; y < roomSize.y; y++)
                {
                    roomArea.Add(new Vector2Int((int)(roomTopLeft.x + x), (int)(roomTopLeft.y + y)));
                }
            }

            roomSize.x += 1;

            // 오른쪽에 추가된 한 칸의 좌표 추가
            for (int y = 0; y < roomSize.y; y++)
            {
                roomArea.Add(new Vector2Int((int)(roomTopLeft.x + roomSize.x - 1), (int)(roomTopLeft.y + y)));
            }

            // 방의 좌표를 rooms 리스트에 추가
            

            roomArea.ExceptWith(wall);
            roomArea.ExceptWith(corridor);
            rooms.Add(roomArea.ToList());

        }

        // rooms 리스트에 각 방의 좌표가 저장되어 있음

        return rooms;
    }*/
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
    public void CreateAndPositionRectangle()
    {
        // rectangleBox를 생성한 후 사각형들을 그의 자식으로 만듭니다
        Transform rectangleBoxTransform = rectangleBox.transform;

        for (int i = 0; i < a.Count; i++)
        {
            // 룸의 중심 좌표를 사용
            Vector2 roomCenter = b[i];

            // 가로와 세로의 반 길이
            float halfWidth = a[i].x / 2;
            float halfHeight = a[i].y / 2;

            // 사각형 오브젝트 생성
            GameObject rectangle = new GameObject("Rectangle" + i.ToString());
            Transform rectangleTransform = rectangle.transform;

            // 사각형의 MeshRenderer 및 MeshFilter 추가
            MeshRenderer renderer = rectangle.AddComponent<MeshRenderer>();
            MeshFilter filter = rectangle.AddComponent<MeshFilter>();

            // Mesh 생성
            Mesh mesh = new Mesh();
            Vector3[] vertices = new Vector3[4];
            vertices[0] = new Vector2(-halfWidth, -halfHeight); // 왼쪽 아래
            vertices[1] = new Vector2(halfWidth, -halfHeight);  // 오른쪽 아래
            vertices[2] = new Vector2(-halfWidth, halfHeight);  // 왼쪽 위
            vertices[3] = new Vector2(halfWidth, halfHeight);   // 오른쪽 위

            Vector2[] uv = new Vector2[] { new Vector2(0, 0), new Vector2(1, 0), new Vector2(0, 1), new Vector2(1, 1) };
            int[] triangles = new int[] { 0, 1, 2, 2, 1, 3 };

            mesh.vertices = vertices;
            mesh.uv = uv;
            mesh.triangles = triangles;

            filter.mesh = mesh;

            // 위치 설정 (룸의 중심으로)
            rectangleTransform.position = new Vector3(roomCenter.x, roomCenter.y, 0);

            // rectangleBox의 자식으로 추가
            rectangleTransform.SetParent(rectangleBoxTransform);
            // Tilemap Collider 2D 추가
            BoxCollider2D boxCollider2D = rectangle.AddComponent<BoxCollider2D>();
            boxCollider2D.isTrigger = true;
            // 태그 설정
            rectangle.tag = "Rectangle";
        }
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
