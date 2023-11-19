using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class LevelUpMenuContrallor1 : MonoBehaviour
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
                weaponSetting.setSkill2("SpeedSlash"); break;
            case 1:
                weaponSetting.setSkill2("SpearSlash"); break;
            case 2:
                weaponSetting.setSkill2("SquadSlash"); break;
            case 3:
                weaponSetting.setSkill2("SuperSlash"); break;
            case 4:
                weaponSetting.setSkill2("clawsSlash"); break;
            case 5:
                weaponSetting.setSkill2("lineSlash"); break;
        }
    }
}
