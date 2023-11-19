using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class LevelUpMenuContrallor : MonoBehaviour
{
    public Sprite[] imgAraay;
    [SerializeField]
    private Image SkillImage;
    [SerializeField]
    private Button slectButton;
    [SerializeField]
    private WeaponSetting weaponSetting;
    int randomIndex;
    private void Start()
    {
        ImageSelect();
    }
    private void Update()
    {
        slectButton.onClick.AddListener(SendSkill);
    }
    private void ImageSelect()
    {
        // �迭���� �����ϰ� �ε����� ����
        randomIndex = Random.Range(0, imgAraay.Length);

        // ���õ� �̹����� SkillImage�� �Ҵ�
        SkillImage.sprite = imgAraay[randomIndex];
    }
    public void SendSkill()
    {
        switch (randomIndex)
        {
            case 0:
                weaponSetting.setSkill("SpeedSlash"); break;
            case 1:
                weaponSetting.setSkill("SpearSlash"); break;
            case 2:
                weaponSetting.setSkill("SquadSlash"); break;
            case 3:
                weaponSetting.setSkill("SuperSlash"); break;
            case 4:
                weaponSetting.setSkill("clawsSlash"); break;
            case 5:
                weaponSetting.setSkill("lineSlash"); break;
        }
    }
}
