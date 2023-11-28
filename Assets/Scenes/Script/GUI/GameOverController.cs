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
        // Ȱ��ȭ�Ǹ� �ִϸ��̼� ����
        StartAnimation();
    }

    void StartAnimation()
    {
        // �ʱ� ���� ����
        Panel.color = new Color(0f, 0f, 0f, 0f); // ������(0, 0, 0)���� �ʱ�ȭ�ϰ� ������ 0���� ����
        GameOver.color = new Color(1f, 1f, 1f, 0f);
        GameOver.rectTransform.anchoredPosition = new Vector2(0f, 0f);
        ReturnBtn.transform.localPosition = new Vector3(-2000f, -128f, 0f);
        ExitBtn.transform.localPosition = new Vector3(2000f, -344f, 0f);

        // Panel�� GameOver ���ÿ� ������ ���·� ��ȭ (3�� ����)
        DOTween.Sequence()
            .Append(Panel.DOFade(1f, 3f)) // Panel�� ������ 1�� ����
            .Join(GameOver.DOFade(1f, 3f)) // GameOver�� ������ 1�� ����
            .OnComplete(() =>
            {
                // GameOver �̵� �ִϸ��̼� (y������ 250��ŭ �̵�, 3�� ����)
                GameOver.rectTransform.DOAnchorPosY(207f, 3f);
                // ���� ��ư �̵� �ִϸ��̼� (x������ 200��ŭ �̵�, 3�� ����)
                ReturnBtn.transform.DOLocalMoveX(0f, 3f);
                // Exit ��ư �̵� �ִϸ��̼� (x������ -200��ŭ �̵�, 3�� ����)
                ExitBtn.transform.DOLocalMoveX(0f, 3f);
            });

        // ���ϴ� ���� �Ŀ� ��ư Ȱ��ȭ �Ǵ� �ٸ� ������ �߰��� �� �ֽ��ϴ�.
        // �� ���������� 6�� �Ŀ� ��ư�� Ȱ��ȭ�ϰ��� ��
        Invoke("EnableButtons", 6f);
    }

    void EnableButtons()
    {
        ReturnBtn.interactable = true;
        ExitBtn.interactable = true;
    }
}
