using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuDOtween : MonoBehaviour
{
    [SerializeField]
    private camaraMoving camaraMoving;
    public Button play, option, quit;
    public float fadeInDuration = 2.0f; // Canvas가 나타나는 데 걸리는 시간
    CanvasGroup canvasGroup;
    void Start()
    {
        // CanvasGroup 컴포넌트 가져오기
        canvasGroup = GetComponent<CanvasGroup>();

        // CanvasGroup을 사용하여 Canvas를 점점 나타나게 함
        canvasGroup.DOFade(1f, fadeInDuration);
        // 버튼에 이벤트 리스너 추가
        play.onClick.AddListener(PlayButtonClicked);
        option.onClick.AddListener(OptionButtonClicked);
        quit.onClick.AddListener(QuitButtonClicked);
    }
    // 각 버튼의 클릭 이벤트에 대한 메소드
    void PlayButtonClicked()
    {
        camaraMoving.MoveToOriginalPosition();
        canvasGroup.DOFade(-1f, fadeInDuration);
            // 1초 뒤에 다음 씬으로 이동
            Invoke("LoadNextScene", 1.5f);
        Debug.Log("Play Button Clicked");
        // 버튼 클릭 시 수행할 작업 추가
    }
    void LoadNextScene()
    {
        // "NextScene"은 실제로 사용할 씬의 이름으로 대체해야 합니다.
        SceneManager.LoadScene("Town");
    }

    void OptionButtonClicked()
    {
        Debug.Log("Option Button Clicked");
        // 버튼 클릭 시 수행할 작업 추가
    }

    void QuitButtonClicked()
    {
        Debug.Log("Quit Button Clicked");
        // 버튼 클릭 시 수행할 작업 추가
    }

}
