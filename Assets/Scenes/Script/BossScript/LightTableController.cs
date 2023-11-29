using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightTableController : MonoBehaviour
{
    [SerializeField]
    private FinalAttack finalAttack;
    void Update()
    {
        int activeChildCount = GetActiveChildCount(transform);
        // �ڽ� ����� ������ 4���� Ȯ��
        if (activeChildCount == 4)
        {
            Debug.Log("asdf");
            // 4�� �Ǹ� ���ϴ� �Լ��� ����
            Invoke("YourFunctionToExecute", 5f);
        }
    }

    private int GetActiveChildCount(Transform parent)
    {
        // Ȱ��ȭ�� �ڽĸ� ã�� ���� true ����
        Transform[] activeChildren = parent.GetComponentsInChildren<Transform>(false);

        // ���� Transform�� �����߱� ������ 1�� ���� ��ȯ
        return activeChildren.Length - 1;
    }
    bool doingAttack =true;
    void YourFunctionToExecute()
    {
        if (doingAttack) {
            finalAttack.FinalAttackOn();
            doingAttack = false;
        }
        
    }
}
