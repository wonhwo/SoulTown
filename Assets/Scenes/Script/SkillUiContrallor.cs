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
        
            // Ʈ�� �ִϸ��̼� ����
            transform.DOMoveY(initialPosition.y - 1000f, 3f) // Y������ 5�� �Ÿ��� 2�� ���� �̵� (�Ʒ��� �̵�)
                .SetEase(Ease.OutQuad) // �̵��� ���ӵ� ����
                .OnComplete(() =>
                {
                    // �ִϸ��̼��� ������ ȣ��� �ݹ� �Լ�
                    Debug.Log("Tween animation completed!");
                });
    }
    private void MoveUpSkillUi()
    {
        
        {
            // Ʈ�� �ִϸ��̼� ����
            transform.DOMoveY(initialPosition.y + 1000f, 3f) // Y������ 5�� �Ÿ��� 2�� ���� �̵� (�Ʒ��� �̵�)
                .SetEase(Ease.OutQuad) // �̵��� ���ӵ� ����
                .OnComplete(() =>
                {
                    // �ִϸ��̼��� ������ ȣ��� �ݹ� �Լ�
                    Debug.Log("Tween animation completed!");
                });
        }
    }
}
