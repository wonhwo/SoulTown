using UnityEngine;

public class BoosEnemySpawnerController : MonoBehaviour
{
    public GameObject prefabToSpawn; // Inspector에서 프리팹을 설정하기 위한 변수
    private bool spawning = false; // 소환 중인지 여부를 나타내는 플래그
    private float spawnTimer = 0f; // 소환 타이머
    private void Start()
    {
        SpawnPrefab();
    }

    private void Update()
    {
        // 자식이 없고 소환 중이 아닌 경우
        if (transform.childCount == 0 && !spawning)
        {
            // 타이머가 30초 이상 경과한 경우 소환 수행
            if (spawnTimer >= 30f)
            {
                SpawnPrefab();
                spawnTimer = 0f; // 타이머 초기화
            }
            else
            {
                // 아직 30초가 경과하지 않은 경우 타이머 증가
                spawnTimer += Time.deltaTime;
            }
        }
    }

    private void SpawnPrefab()
    {
        if (prefabToSpawn != null)
        {
            // 스포너의 위치를 스폰 위치로 사용하여 프리팹을 소환
            GameObject spawnedObject = Instantiate(prefabToSpawn, transform.position, transform.rotation);

            // 스포너의 자식으로 설정
            spawnedObject.transform.parent = transform;

            // 소환 중인 상태로 변경
            spawning = true;

            // 3초 후에 소환 중인 상태 해제
            Invoke("ResetSpawningFlag", 3f);
        }
        else
        {
            Debug.LogWarning("Prefab이 설정되지 않았습니다.");
        }
    }

    private void ResetSpawningFlag()
    {
        // 소환 중인 상태 해제
        spawning = false;
    }
}
