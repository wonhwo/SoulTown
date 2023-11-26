using UnityEngine;
using System.Collections;

public class EnerhyGenerator : MonoBehaviour
{
    [SerializeField]
    private FinalAttack finalAttack;
    public GameObject prefabToSpawn; // 소환할 프리팹
    public GameObject[] spawners; // 스포너들의 배열
    public GameObject[] LightCount;
    bool monsterCount1 = false;
    bool monsterCount2 = false;
    bool monsterCount3 = false;
    bool monsterCount4 = false;
    private float timer = 0f;
    public float interval = 5; 

    private int currentActiveLightIndex = 0; // 현재 활성화된 라이트의 인덱스

    private void Update()
    {
        trueMonsterCount();
        SpanerChildCount();
        

        if (monsterCount1 && monsterCount2 && monsterCount3 && monsterCount4)
        {
            timer += Time.deltaTime;

            // 일정 간격으로 체크하고 라이트를 활성화
            if (timer >= interval)
            {
                ActivateLight(currentActiveLightIndex, true); // 현재 라이트 활성화
                currentActiveLightIndex = (currentActiveLightIndex + 1) % LightCount.Length; // 다음 라이트 인덱스 계산
                timer = 0f; // 타이머 초기화
            }

            SpawnPrefab(transform);
        }
        else
        {
            // 조건이 충족되지 않았을 때 일정 간격으로 이전 라이트를 비활성화
            timer += Time.deltaTime;

            if (timer >= interval)
            {
                ActivateLight(currentActiveLightIndex, false); // 현재 라이트 비활성화
                currentActiveLightIndex = (currentActiveLightIndex - 1 + LightCount.Length) % LightCount.Length; // 이전 라이트 인덱스 계산
                timer = 0f; // 타이머 초기화
            }
        }
    }

    // 라이트를 활성화 또는 비활성화하는 함수
    private void ActivateLight(int index, bool activate)
    {
        LightCount[index].SetActive(activate);
    }
    void SpanerChildCount()
    {
        if (spawners[0].transform.childCount != 0)
        {
            monsterCount1 = true;
        }
        if (spawners[1].transform.childCount != 0)
        {
            monsterCount2 = true;
        }

        if (spawners[2].transform.childCount != 0)
        {
            monsterCount3 = true;
        }

        if (spawners[3].transform.childCount != 0)
        {
            monsterCount4 = true;
        }
    }
    void trueMonsterCount()
    {
        monsterCount1 = false;
        monsterCount2 = false;
        monsterCount3 = false;
        monsterCount4 = false;
    }

    float lastSpawnTime; // 마지막으로 스폰된 시간을 저장하는 변수
    float delay = 5f; // 딜레이 시간 (초)

    void SpawnPrefab(Transform spawnPoint)
    {
        // lastSpawnTime이 초기화되지 않았거나, 일정 시간 이상 경과했을 때 스폰을 허용
        if (lastSpawnTime == 0f || (Time.time - lastSpawnTime >= delay && transform.childCount == 0))
        {
            Instantiate(prefabToSpawn, spawnPoint.position, Quaternion.identity, gameObject.transform);

            // 스폰된 시간 갱신
            lastSpawnTime = Time.time;
        }
    }
}