using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
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
    [SerializeField]
    private TextMeshProUGUI SkillName1UI2;
    [SerializeField]
    private TextMeshProUGUI SkilldamageUI2;
    private string[] SkillName = new string[] { "���ǵ彽����", "���Ǿ����", "���������", "���۽�����", "ũ�ο콽����", "���ν�����" };
    private string[] SkillDamage = new string[] { "25", "50", "30", "10", "30", "20" };
    private int[] plusDamage = new int[] { 5, 1, 5, 5, 5 };
    private List<int> selectedIndices = new List<int>();

    private void Start()
    {
        ImageSelect();
    }
    private void Update()
    {
        slectButton.onClick.AddListener(SendSkill);
    }
    bool isdindices=false;
    int num=0;
    public void ImageSelect()
    {
        // �迭���� �����ϰ� �ε����� ����
        randomIndex = Random.Range(0, imgAraay.Length);
        
        if (selectedIndices.Contains(randomIndex))
        {
            num += plusDamage[randomIndex];
            isdindices = true;
            select(randomIndex);

        }
        else
        {
            num = 0;
            isdindices = false;
            defaultSelect(randomIndex);

        }
        selectedIndices.Add(randomIndex);


        // ���õ� �̹����� SkillImage�� �Ҵ�

    }
    public void SendSkill()
    {
        switch (randomIndex)
        {
            case 0:
                weaponSetting.setSkill2("SpeedSlash", num);
                selectedIndices.RemoveAll(index => index != 0);
                break;
            case 1:
                weaponSetting.setSkill2("SpearSlash", num);
                selectedIndices.RemoveAll(index => index != 1);
                break;
            case 2:
                weaponSetting.setSkill2("SquadSlash", num);
                selectedIndices.RemoveAll(index => index != 2);
                break;
            case 3:
                weaponSetting.setSkill2("SuperSlash", num);
                selectedIndices.RemoveAll(index => index != 3);
                break;
            case 4:
                weaponSetting.setSkill2("clawsSlash", num);
                selectedIndices.RemoveAll(index => index != 4);
                break;
        }
    }
    private void defaultSelect(int randomIndex) {
        SkillImage.sprite = imgAraay[randomIndex];
        SkillName1UI2.text = SkillName[randomIndex];
        SkilldamageUI2.text = "������ : " + SkillDamage[randomIndex];
    }
    private void select(int randomIndex)
    {
        SkillImage.sprite = imgAraay[randomIndex];
        SkillName1UI2.text = SkillName[randomIndex];
        if(randomIndex==1)
            SkilldamageUI2.text = "������ : -" + plusDamage[randomIndex];
        else
            SkilldamageUI2.text = "������ : +" + plusDamage[randomIndex];
    }
}
