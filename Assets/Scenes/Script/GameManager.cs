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
    public void showBoss()
    {
        BossRoom.SetActive(true);
        Level1Boss.SetActive(true);
        bossStatus.SetActive(true);

    }



}
