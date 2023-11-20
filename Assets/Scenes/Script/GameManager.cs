using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject talkPanel;
    public Text talkText;
    public GameObject scanObject;
    public bool isAction =false;
    [SerializeField]
    public GameObject SkillUi;
    private bool isPaused = false;
    private Rigidbody2D rb;

    private void Start()
    {
    }
    public void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            // 일시정지 중일 때 원하는 오브젝트의 업데이트를 건너뛰도록 설정
            PauseAllExceptSkillUi();
            Time.timeScale = 0f;
        }
        else
        {
            // 일시정지 해제 시 모든 오브젝트의 업데이트를 다시 활성화
            ResumeAllExceptSkillUi();
            Time.timeScale = 1f;
        }
    }

    void PauseAllExceptSkillUi()
    {
        // 게임 오브젝트를 찾거나 원하는 방법으로 필터링하여 일시정지
        foreach (var obj in FindObjectsOfType<GameObject>())
        {
            if (obj != SkillUi)
            {
                // SkillUiContrallor 스크립트가 아닌 경우에만 일시정지
                SkillUiContrallor skillUiController = obj.GetComponent<SkillUiContrallor>();
                if (skillUiController != null)
                {
                    skillUiController.OnPause();
                }
            }
        }
    }

    void ResumeAllExceptSkillUi()
    {
        // 게임 오브젝트를 찾거나 원하는 방법으로 필터링하여 일시정지 해제
        foreach (var obj in FindObjectsOfType<GameObject>())
        {
            if (obj != SkillUi)
            {
                // SkillUiContrallor 스크립트가 아닌 경우에만 일시정지 해제
                SkillUiContrallor skillUiController = obj.GetComponent<SkillUiContrallor>();
                if (skillUiController != null)
                {
                    skillUiController.OnResume();
                }
            }
        }
    }
    public void Action(GameObject scanObj)
    {
        if (isAction)
        {
            isAction = false;

        }
        else
        {
            isAction = true;
            scanObject = scanObj;
            talkText.text = "이것의 이름은" + scanObject.name;
        }
        talkPanel.SetActive(isAction);

    }



}
