using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;
// ���� ����� ���� �������� ������ ����� ���� ����� Ŭ����
public class MakeRandomMap : MonoBehaviour
{
    //��������� ������
    [SerializeField]
    private int distance;
    [SerializeField]
    private int minRoomWidth;
    [SerializeField]
    private int minRoomHeight;
    //������ �������� ����Ʈ�� �����̽� ����Ʈ�� �������� ����
    [SerializeField]
    private DivideSpace divideSpace;
    //�ٴ����ϰ� ��Ÿ�� ���
    [SerializeField]
    private SpreadTilemap spreadTilemap;
    //�÷��̾�
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private GameObject entrance;
    [SerializeField]
    private GameObject portalPrefab; // ��Ż �������� ������ ����
    [SerializeField]
    private GameObject outPortalPrefab; // ��Ż �������� ������ ����
    [SerializeField]
    private GameObject Portal; //�θ� ��Ż
    [SerializeField]
    private GameObject OutPortal; //�θ� ��Ż
    [SerializeField]
    private Tilemap tilemap;
    [SerializeField]
    private TileBase tile;

    private HashSet<Vector2Int> floor;
    private HashSet<Vector2Int> wall;
    private HashSet<Vector2Int> corridor;
    private void Start()
    {
        StartRandomMap();
    }
    //���� �� ���� �Լ�
    public void StartRandomMap()
    {
        //��� Ÿ�� ����
        spreadTilemap.ClearAllTiles();
        divideSpace.totalSpace = new RectangleSpace(new Vector2Int(0, 0), divideSpace.totalWidth, divideSpace.totalHeight);
        divideSpace.spaceList = new List<RectangleSpace>();
        floor = new HashSet<Vector2Int>();
        wall = new HashSet<Vector2Int>();
        corridor = new HashSet<Vector2Int>();
        //�����̽� ����Ʈ ����
        divideSpace.DivideRoom(divideSpace.totalSpace);
        //��, ����, �� ��ǥ ����
        MakeRandomRooms();

        MakeCorridors();
        corridor.ExceptWith(floor);
        wall.ExceptWith(corridor);
        floor.UnionWith(corridor);
        MakeWall();

        //Ÿ�� ���
        spreadTilemap.SpreadFloorTilemap(floor);
        spreadTilemap.SpreadWallTilemap(wall);
        spreadTilemap.SpreadCorridorTilemap(corridor);

        player.transform.position = (Vector2)divideSpace.spaceList[0].Center();
        //entrance.transform.position = (Vector2)divideSpace.spaceList[0].Center();
        foreach (Vector2Int position in corridor)
        {
            Debug.Log("Corridor Position: " + position.x + ", " + position.y);
        }

    }
    //���� ����� �Լ�
    private void MakeRandomRooms()
    {
        //�����̼����� ��� �����̽� ����Ʈ ��������
        foreach (var space in divideSpace.spaceList){
            HashSet<Vector2Int> positions = MakeRandomRectangleRoom(space);
            floor.UnionWith(positions);
            //�÷ξ ��ǥ �߰� UnionWith ������
        }
    }
    //���� ���� ���ϴ� �Լ�(��� ���� �� �ִ� ���̸� ��������)
    //������ �߽� ã��
    List<Vector2> a= new List<Vector2>();
    private HashSet<Vector2Int> MakeRandomRectangleRoom(RectangleSpace space)
    {
        HashSet<Vector2Int> positions = new HashSet<Vector2Int>();
        int width = Random.Range(minRoomWidth, space.width + 1 - distance * 2);
        int height = Random.Range(minRoomHeight, space.height + 1 - distance * 2);
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
        corridor.Add(current); // ������ ��ǥ�� corridor�� �߰�
        while (current.x != next.x)
        {
            if (current.x < next.x)
            {
                current.x += 1;
                corridor.Add(current); // ������ ��ǥ�� corridor�� �߰�
            }
            else
            {
                current.x -= 1;
                corridor.Add(current); // ������ ��ǥ�� corridor�� �߰�
            }
        }
        while (current.y != next.y)
        {
            if (current.y < next.y)
            {
                current.y += 1;
                corridor.Add(current); // ������ ��ǥ�� corridor�� �߰�
            }
            else
            {
                current.y -= 1;
                corridor.Add(current); // ������ ��ǥ�� corridor�� �߰�
            }
        }
    }



    //���� ����� �Լ�
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
