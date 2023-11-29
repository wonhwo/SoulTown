using UnityEngine;
using System.Collections;

public class EnerhyGenerator : MonoBehaviour
{
    [SerializeField]
    private FinalAttack finalAttack;
    public GameObject prefabToSpawn; // ��ȯ�� ������
    public GameObject[] spawners; // �����ʵ��� �迭
    public GameObject[] LightCount;
    bool monsterCount1 = false;
    bool monsterCount2 = false;
    bool monsterCount3 = false;
    bool monsterCount4 = false;
    private float timerLight = 0f;
    private float timerPrefab = 0f;
    public float interval = 5;

    private int currentActiveLightIndex = 0; // ���� Ȱ��ȭ�� ����Ʈ�� �ε���

    private void Start()
    {
        lastSpawnTime = Time.time;
    }

    private void Update()
    {
        trueMonsterCount();
        SpanerChildCount();

        if (monsterCount1 && monsterCount2 && monsterCount3 && monsterCount4)
        {
            timerLight += Time.deltaTime;

            // ���� �������� üũ�ϰ� ����Ʈ�� Ȱ��ȭ
            if (timerLight >= 10)
            {
                ActivateLight(currentActiveLightIndex, true); // ���� ����Ʈ Ȱ��ȭ
                currentActiveLightIndex = (currentActiveLightIndex + 1) % LightCount.Length; // ���� ����Ʈ �ε��� ���
                timerLight = 0f; // Ÿ�̸� �ʱ�ȭ
            }
        }
        else
        {
            // ������ �������� �ʾ��� �� ���� �������� ���� ����Ʈ�� ��Ȱ��ȭ
            timerLight += Time.deltaTime;

            if (timerLight >= 5)
            {
                ActivateLight(currentActiveLightIndex, false); // ���� ����Ʈ ��Ȱ��ȭ
                currentActiveLightIndex = (currentActiveLightIndex - 1 + LightCount.Length) % LightCount.Length; // ���� ����Ʈ �ε��� ���
                timerLight = 0f; // Ÿ�̸� �ʱ�ȭ
            }
        }

        // ������ ��ȯ�� ���� Ÿ�̸� ���� �߰�
        if (monsterCount1 && monsterCount2 && monsterCount3 && monsterCount4)
        {
            timerPrefab += Time.deltaTime;

            if (timerPrefab >= interval)
            {
                if (HasSpawnedPrefab())
                {
                    timerPrefab = 0f;
                    lastSpawnTime = Time.time;
                }

                SpawnPrefab(transform);
            }
        }
    

}

    // ����Ʈ�� Ȱ��ȭ �Ǵ� ��Ȱ��ȭ�ϴ� �Լ�
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

    float lastSpawnTime; // ���������� ������ �ð��� �����ϴ� ����
    float delay = 5f; // ������ �ð� (��)

    void SpawnPrefab(Transform spawnPoint)
    {
        // lastSpawnTime�� �ʱ�ȭ���� �ʾҰų�, ���� �ð� �̻� ������� �� ������ ���
        if (lastSpawnTime == 0f || (Time.time - lastSpawnTime >= delay && !HasSpawnedPrefab()))
        {
            Instantiate(prefabToSpawn, spawnPoint.position, Quaternion.identity, gameObject.transform);

            // ������ �Ŀ� Ÿ�̸� �ʱ�ȭ
            lastSpawnTime = Time.time;

            // �߰��� �κ�: ���� �Ŀ��� Ÿ�̸Ӹ� �ʱ�ȭ�մϴ�.
            timerPrefab = 0f;
        }
    }

    bool HasSpawnedPrefab()
    {
        // �ڽ� ��ü�� ���� ���θ� Ȯ���Ͽ� ��ȯ
        return transform.childCount > 0;
    }
}