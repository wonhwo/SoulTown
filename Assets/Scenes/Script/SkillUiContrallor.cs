using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SkillUiContrallor : MonoBehaviour
{
    [SerializeField]
    private GameManager gameManager;
    Vector3 initialPosition;
    [SerializeField]
    private Button Skill1Button;
    [SerializeField]
    private Button Skill2Button;
    [SerializeField]
    private GameObject backDrop;

    private void Start()
    {
        if (SceneManager.GetActiveScene().name.Equals("Dungeon"))
        {
            initialPosition = transform.position;
            Skill1Button.onClick.AddListener(MoveUpSkillUi);  // �������� MoveUpSkillUi�� ȣ���ߴ� ���� MoveDownSkillUi�� ����
            Skill2Button.onClick.AddListener(MoveUpSkillUi);
        }
    }



public void MoveDownSkillUi()
{
        backDrop.SetActive(true);
    Time.timeScale = 0f;

        // Ʈ�� �ִϸ��̼� ����
        transform.DOMoveY(transform.position.y - 1000f, 3f)
                .SetEase(Ease.OutQuad)
                .OnComplete(() =>
                {
                    Debug.Log("Tween animation completed!");
                })
                .SetUpdate(true); // �� �κ��� �߰��Ͽ� IgnoreTimeScale(true) ����

}

    private void MoveUpSkillUi()
    {
        // Ʈ�� �ִϸ��̼� ����
        transform.DOMoveY(transform.position.y + 1000f, 3f)
                .SetEase(Ease.OutQuad)
                .OnComplete(() =>
                {
                    backDrop.SetActive(false);
                    Time.timeScale = 1f;
                })
                .SetUpdate(true); // �� �κ��� �߰��Ͽ� IgnoreTimeScale(true) ����
    }
}
