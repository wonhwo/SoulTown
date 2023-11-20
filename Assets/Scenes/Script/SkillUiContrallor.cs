using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using MoreMountains.TopDownEngine;
using UnityEngine.UI;
using System;
using System.Runtime.Remoting.Messaging;
using UnityEngine.Events;
using DG.Tweening.Core.Easing;

public class SkillUiContrallor : MonoBehaviour
{
    [SerializeField]
    private GameManager gameManager;
    Vector3 initialPosition;
    [SerializeField]
    private Button Skill1Button;
    [SerializeField]
    private Button Skill2Button;
    // Start is called before the first frame update
    private void Start()
    {
        initialPosition = transform.position;
        Skill1Button.onClick.AddListener(MoveUpSkillUi);
        Skill2Button.onClick.AddListener(MoveUpSkillUi);
    }

    // Update is called once per frame
    public void OnPause()
    {
        MoveDownSkillUi();
    }

    public void OnResume()
    {
        MoveUpSkillUi();
    }

    public void MoveDownSkillUi()
    {
        
            // 트윈 애니메이션 설정
            transform.DOMoveY(initialPosition.y - 1000f, 3f) // Y축으로 5의 거리를 2초 동안 이동 (아래로 이동)
                .SetEase(Ease.OutQuad) // 이동의 가속도 설정
                .OnComplete(() =>
                {
                    // 애니메이션이 끝나면 호출될 콜백 함수
                    Debug.Log("Tween animation completed!");
                });
    }
    private void MoveUpSkillUi()
    {
        
        {
            // 트윈 애니메이션 설정
            transform.DOMoveY(initialPosition.y + 1000f, 3f) // Y축으로 5의 거리를 2초 동안 이동 (아래로 이동)
                .SetEase(Ease.OutQuad) // 이동의 가속도 설정
                .OnComplete(() =>
                {
                    // 애니메이션이 끝나면 호출될 콜백 함수
                    Debug.Log("Tween animation completed!");
                });
        }
    }
}
