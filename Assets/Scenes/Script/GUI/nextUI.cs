using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class nextUI : MonoBehaviour
{
    public float fadeInDuration = 1f;
    public float fadeOutDuration = 1f;

    private void Start()
    {
        // UI�� �ʱ⿡ ��Ȱ��ȭ
        //gameObject.SetActive(false);

        // ���� �ð��� ���� �Ŀ� UI�� ������ ���̰� �ϱ�
        //ShowUI();
    }

    public void ShowUI()
    {
        // UI�� ������ ���̰� �ϱ�
        gameObject.SetActive(true);
        CanvasGroup canvasGroup = gameObject.GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0f;

        canvasGroup.DOFade(1f, fadeInDuration)
            .OnStart(() => Debug.Log("UI Fade In Started")) // �ִϸ��̼� ������ �� �α� ���
            .OnComplete(() => HideUI());

    }

    public void HideUI()
    {
        // UI�� ������ ������� �ϱ�
        CanvasGroup canvasGroup = gameObject.GetComponent<CanvasGroup>();
        canvasGroup.DOFade(0f, fadeOutDuration)
            .OnStart(() => Debug.Log("UI Fade Out Started")) // �ִϸ��̼� ������ �� �α� ���
            .OnComplete(() => gameObject.SetActive(false)); // �ִϸ��̼� �Ϸ�Ǹ� UI ��Ȱ��ȭ
    }
}
