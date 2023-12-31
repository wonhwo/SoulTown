using UnityEngine;
using DG.Tweening;

public class StartUI : MonoBehaviour
{
    public GameObject uiObject;
    public float fadeInDuration = 1f;
    public float fadeOutDuration = 1f;

    private void Start()
    {
        // UI를 초기에 비활성화
        uiObject.SetActive(false);

        // 일정 시간이 지난 후에 UI를 스르륵 보이게 하기
        ShowUI();
    }

    private void ShowUI()
    {
        // UI를 스르륵 보이게 하기
        uiObject.SetActive(true);
        CanvasGroup canvasGroup = uiObject.GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0f;

        canvasGroup.DOFade(1f, fadeInDuration)
            .OnStart(() => Debug.Log("UI Fade In Started")) // 애니메이션 시작할 때 로그 출력
            .OnComplete(() => HideUI());

    }

    public void HideUI()
    {
        // UI를 스르륵 사라지게 하기
        CanvasGroup canvasGroup = uiObject.GetComponent<CanvasGroup>();
        canvasGroup.DOFade(0f, fadeOutDuration)
            .OnStart(() => Debug.Log("UI Fade Out Started")) // 애니메이션 시작할 때 로그 출력
            .OnComplete(() => uiObject.SetActive(false)); // 애니메이션 완료되면 UI 비활성화
    }
}
