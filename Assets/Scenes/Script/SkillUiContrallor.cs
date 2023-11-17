using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using MoreMountains.TopDownEngine;

public class SkillUiContrallor : MonoBehaviour
{
    // Start is called before the first frame update
    private void Awake()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            MoveSkillUi();
        }
    }
    bool isSkillUiActive = false;
    private void MoveSkillUi()
    {
        Vector3 initialPosition = transform.position;
        isSkillUiActive = !isSkillUiActive; // 현재 상태의 반대로 설정
        if (isSkillUiActive)
        {      
            // 트윈 애니메이션 설정
            transform.DOMoveY(initialPosition.y - 1000f, 2f) // Y축으로 5의 거리를 2초 동안 이동 (아래로 이동)
                .SetEase(Ease.OutQuad) // 이동의 가속도 설정
                .OnComplete(() =>
                {
                    // 애니메이션이 끝나면 호출될 콜백 함수
                    Debug.Log("Tween animation completed!");
                });
        }
        else if (!isSkillUiActive)
        {
            // 트윈 애니메이션 설정
            transform.DOMoveY(initialPosition.y + 1000f, 2f) // Y축으로 5의 거리를 2초 동안 이동 (아래로 이동)
                .SetEase(Ease.OutQuad) // 이동의 가속도 설정
                .OnComplete(() =>
                {
                    // 애니메이션이 끝나면 호출될 콜백 함수
                    Debug.Log("Tween animation completed!");
                });
        }
        // 초기 위치
        


    }
}
