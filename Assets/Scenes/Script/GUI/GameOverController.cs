using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GameOverController : MonoBehaviour
{
    [SerializeField] Image Panel;
    [SerializeField] Image GameOver;
    [SerializeField] Button ReturnBtn;
    [SerializeField] Button ExitBtn;

    private void Start()
    {
        ReturnBtn.onClick.AddListener(() => {
            Time.timeScale = 1f; LoadingMnager.LoadScene("Town2");
        });
        ExitBtn.onClick.AddListener(() => {
            Time.timeScale = 1f; LoadingMnager.LoadScene("StartMenu");
        });
    }
    private void OnEnable()
    {
        // 활성화되면 애니메이션 시작
        StartAnimation();
    }

    void StartAnimation()
    {
        // 초기 상태 설정
        Panel.color = new Color(0f, 0f, 0f, 0f); // 검정색(0, 0, 0)으로 초기화하고 투명도를 0으로 설정
        GameOver.color = new Color(1f, 1f, 1f, 0f);
        GameOver.rectTransform.anchoredPosition = new Vector2(0f, 0f);
        ReturnBtn.transform.localPosition = new Vector3(-2000f, -128f, 0f);
        ExitBtn.transform.localPosition = new Vector3(2000f, -344f, 0f);

        // Panel과 GameOver 동시에 불투명 상태로 변화 (3초 동안)
        DOTween.Sequence()
            .Append(Panel.DOFade(1f, 3f)) // Panel의 투명도를 1로 변경
            .Join(GameOver.DOFade(1f, 3f)) // GameOver의 투명도를 1로 변경
            .OnComplete(() =>
            {
                // GameOver 이동 애니메이션 (y축으로 250만큼 이동, 3초 동안)
                GameOver.rectTransform.DOAnchorPosY(207f, 3f);
                // 리턴 버튼 이동 애니메이션 (x축으로 200만큼 이동, 3초 동안)
                ReturnBtn.transform.DOLocalMoveX(0f, 3f);
                // Exit 버튼 이동 애니메이션 (x축으로 -200만큼 이동, 3초 동안)
                ExitBtn.transform.DOLocalMoveX(0f, 3f);
            });

        // 원하는 동작 후에 버튼 활성화 또는 다른 동작을 추가할 수 있습니다.
        // 이 예제에서는 6초 후에 버튼을 활성화하고자 함
        Invoke("EnableButtons", 6f);
    }

    void EnableButtons()
    {
        ReturnBtn.interactable = true;
        ExitBtn.interactable = true;
    }
}
