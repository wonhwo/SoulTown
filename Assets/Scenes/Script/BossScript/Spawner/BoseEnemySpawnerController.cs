using UnityEngine;

public class BoosEnemySpawnerController : MonoBehaviour
{
    public GameObject prefabToSpawn; // Inspector���� �������� �����ϱ� ���� ����
    private bool spawning = false; // ��ȯ ������ ���θ� ��Ÿ���� �÷���
    private float spawnTimer = 0f; // ��ȯ Ÿ�̸�
    private void Start()
    {
        SpawnPrefab();
    }

    private void Update()
    {
        // �ڽ��� ���� ��ȯ ���� �ƴ� ���
        if (transform.childCount == 0 && !spawning)
        {
            // Ÿ�̸Ӱ� 30�� �̻� ����� ��� ��ȯ ����
            if (spawnTimer >= 30f)
            {
                SpawnPrefab();
                spawnTimer = 0f; // Ÿ�̸� �ʱ�ȭ
            }
            else
            {
                // ���� 30�ʰ� ������� ���� ��� Ÿ�̸� ����
                spawnTimer += Time.deltaTime;
            }
        }
    }

    private void SpawnPrefab()
    {
        if (prefabToSpawn != null)
        {
            // �������� ��ġ�� ���� ��ġ�� ����Ͽ� �������� ��ȯ
            GameObject spawnedObject = Instantiate(prefabToSpawn, transform.position, transform.rotation);

            // �������� �ڽ����� ����
            spawnedObject.transform.parent = transform;

            // ��ȯ ���� ���·� ����
            spawning = true;

            // 3�� �Ŀ� ��ȯ ���� ���� ����
            Invoke("ResetSpawningFlag", 3f);
        }
        else
        {
            Debug.LogWarning("Prefab�� �������� �ʾҽ��ϴ�.");
        }
    }

    private void ResetSpawningFlag()
    {
        // ��ȯ ���� ���� ����
        spawning = false;
    }
}
