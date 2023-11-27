using UnityEngine;
using DG.Tweening;

public class StartUI : MonoBehaviour
{
    public GameObject uiObject;
    public float fadeInDuration = 1f;
    public float fadeOutDuration = 1f;

    private void Start()
    {
        // UI�� �ʱ⿡ ��Ȱ��ȭ
        uiObject.SetActive(false);

        // ���� �ð��� ���� �Ŀ� UI�� ������ ���̰� �ϱ�
        ShowUI();
    }

    private void ShowUI()
    {
        // UI�� ������ ���̰� �ϱ�
        uiObject.SetActive(true);
        CanvasGroup canvasGroup = uiObject.GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0f;

        canvasGroup.DOFade(1f, fadeInDuration)
            .OnStart(() => Debug.Log("UI Fade In Started")) // �ִϸ��̼� ������ �� �α� ���
            .OnComplete(() => HideUI());

    }

    public void HideUI()
    {
        // UI�� ������ ������� �ϱ�
        CanvasGroup canvasGroup = uiObject.GetComponent<CanvasGroup>();
        canvasGroup.DOFade(0f, fadeOutDuration)
            .OnStart(() => Debug.Log("UI Fade Out Started")) // �ִϸ��̼� ������ �� �α� ���
            .OnComplete(() => uiObject.SetActive(false)); // �ִϸ��̼� �Ϸ�Ǹ� UI ��Ȱ��ȭ
    }
}
