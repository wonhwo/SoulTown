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
    [SerializeField]
    private GameObject startUI;
    [SerializeField]
    private GameObject nexttUI;
    [SerializeField]
    private GameObject PortalUI;
    [SerializeField] GameObject BossRoom;
    [SerializeField] GameObject Level1Boss;
    [SerializeField] GameObject bossStatus;

    [SerializeField]
    GameObject backDrop;
    [SerializeField]
    GameObject GameMenu;
    [SerializeField]
    Button ContinueBtn;
    [SerializeField]
    Button ReturnBtn;
    [SerializeField]
    Button ExitBtn;
    [SerializeField]
    Button settingBtn;
    private void Start()
    {

        ContinueBtn.onClick.AddListener(() => {
            GameMenu.SetActive(false);
            backDrop.SetActive(false); Time.timeScale = 1f; });
        ReturnBtn.onClick.AddListener(() => {
            Time.timeScale = 1f; LoadingMnager.LoadScene("Town2"); });
        ExitBtn.onClick.AddListener(() => {
            Time.timeScale = 1f; LoadingMnager.LoadScene("StartMenu"); });
        settingBtn.onClick.AddListener(onMunu);


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
            talkText.text = scanObject.name;
        }
        talkPanel.SetActive(isAction);

    }
    public void showBoss()
    {
        BossRoom.SetActive(true);
        Level1Boss.SetActive(true);
        bossStatus.SetActive(true);
        

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            onMunu();
        }
    }
    private void onMunu() {
        if (!SceneManager.GetActiveScene().name.Equals("Town2"))
        {
            GameMenu.SetActive(true);
            backDrop.SetActive(true);
            Time.timeScale = 0f;
        }
    }



}
