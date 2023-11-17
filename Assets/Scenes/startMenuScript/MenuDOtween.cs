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
    public float fadeInDuration = 2.0f; // Canvas�� ��Ÿ���� �� �ɸ��� �ð�
    CanvasGroup canvasGroup;
    void Start()
    {
        // CanvasGroup ������Ʈ ��������
        canvasGroup = GetComponent<CanvasGroup>();

        // CanvasGroup�� ����Ͽ� Canvas�� ���� ��Ÿ���� ��
        canvasGroup.DOFade(1f, fadeInDuration);
        // ��ư�� �̺�Ʈ ������ �߰�
        play.onClick.AddListener(PlayButtonClicked);
        option.onClick.AddListener(OptionButtonClicked);
        quit.onClick.AddListener(QuitButtonClicked);
    }
    // �� ��ư�� Ŭ�� �̺�Ʈ�� ���� �޼ҵ�
    void PlayButtonClicked()
    {
        camaraMoving.MoveToOriginalPosition();
        canvasGroup.DOFade(-1f, fadeInDuration);
            // 1�� �ڿ� ���� ������ �̵�
            Invoke("LoadNextScene", 1.5f);
        Debug.Log("Play Button Clicked");
        // ��ư Ŭ�� �� ������ �۾� �߰�
    }
    void LoadNextScene()
    {
        // "NextScene"�� ������ ����� ���� �̸����� ��ü�ؾ� �մϴ�.
        SceneManager.LoadScene("Town");
    }

    void OptionButtonClicked()
    {
        Debug.Log("Option Button Clicked");
        // ��ư Ŭ�� �� ������ �۾� �߰�
    }

    void QuitButtonClicked()
    {
        Debug.Log("Quit Button Clicked");
        // ��ư Ŭ�� �� ������ �۾� �߰�
    }

}
