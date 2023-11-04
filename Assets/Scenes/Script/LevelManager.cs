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

    // 이 스크립트가 연결된 게임 오브젝트가 파괴될 때 호출됩니다.
    private void OnDestroy()
    {
        // 게임 오브젝트가 파괴되었을 때 실행할 작업을 여기에 추가합니다.
        Debug.Log("게임 오브젝트가 파괴되었습니다.");
    }
}