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
        // 자식 요소의 개수가 4인지 확인
        if (activeChildCount == 4)
        {
            Debug.Log("asdf");
            // 4가 되면 원하는 함수를 실행
            Invoke("YourFunctionToExecute", 5f);
        }
    }

    private int GetActiveChildCount(Transform parent)
    {
        // 활성화된 자식만 찾기 위해 true 전달
        Transform[] activeChildren = parent.GetComponentsInChildren<Transform>(false);

        // 현재 Transform을 포함했기 때문에 1을 빼서 반환
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
