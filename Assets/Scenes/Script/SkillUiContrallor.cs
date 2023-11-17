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
        isSkillUiActive = !isSkillUiActive; // ���� ������ �ݴ�� ����
        if (isSkillUiActive)
        {      
            // Ʈ�� �ִϸ��̼� ����
            transform.DOMoveY(initialPosition.y - 1000f, 2f) // Y������ 5�� �Ÿ��� 2�� ���� �̵� (�Ʒ��� �̵�)
                .SetEase(Ease.OutQuad) // �̵��� ���ӵ� ����
                .OnComplete(() =>
                {
                    // �ִϸ��̼��� ������ ȣ��� �ݹ� �Լ�
                    Debug.Log("Tween animation completed!");
                });
        }
        else if (!isSkillUiActive)
        {
            // Ʈ�� �ִϸ��̼� ����
            transform.DOMoveY(initialPosition.y + 1000f, 2f) // Y������ 5�� �Ÿ��� 2�� ���� �̵� (�Ʒ��� �̵�)
                .SetEase(Ease.OutQuad) // �̵��� ���ӵ� ����
                .OnComplete(() =>
                {
                    // �ִϸ��̼��� ������ ȣ��� �ݹ� �Լ�
                    Debug.Log("Tween animation completed!");
                });
        }
        // �ʱ� ��ġ
        


    }
}
