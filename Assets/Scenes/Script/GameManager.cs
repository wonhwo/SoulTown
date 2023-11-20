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
        // isPaused 플래그를 업데이트하고 Time.timeScale을 조절하여 게임을 일시정지하거나 재개합니다.
        isPaused = !isPaused;

        if (isPaused)
        {
            //PauseAllExceptCanvas();
            Time.timeScale = 0f; // 시간을 멈춤

        }
        else
        {
            //ResumeAllExceptCanvas();
            Time.timeScale = 1f; // 시간을 정상 속도로 복구

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
