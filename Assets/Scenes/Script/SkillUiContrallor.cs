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
            Skill1Button.onClick.AddListener(MoveUpSkillUi);  // 이전에는 MoveUpSkillUi를 호출했던 것을 MoveDownSkillUi로 수정
            Skill2Button.onClick.AddListener(MoveUpSkillUi);
        }
    }



public void MoveDownSkillUi()
{
        backDrop.SetActive(true);
    Time.timeScale = 0f;

        // 트윈 애니메이션 설정
        transform.DOMoveY(transform.position.y - 1000f, 3f)
                .SetEase(Ease.OutQuad)
                .OnComplete(() =>
                {
                    Debug.Log("Tween animation completed!");
                })
                .SetUpdate(true); // 이 부분을 추가하여 IgnoreTimeScale(true) 설정

}

    private void MoveUpSkillUi()
    {
        // 트윈 애니메이션 설정
        transform.DOMoveY(transform.position.y + 1000f, 3f)
                .SetEase(Ease.OutQuad)
                .OnComplete(() =>
                {
                    backDrop.SetActive(false);
                    Time.timeScale = 1f;
                })
                .SetUpdate(true); // 이 부분을 추가하여 IgnoreTimeScale(true) 설정
    }
}
