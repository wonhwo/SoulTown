using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class LevelUpMenuContrallor : MonoBehaviour
{
    public Sprite[] imgAraay;
    [SerializeField]
    private Image SkillImage;
    [SerializeField]
    private Button slectButton;
    [SerializeField]
    private WeaponSetting weaponSetting;
    [SerializeField]
    private TextMeshProUGUI SkillName1UI1;
    [SerializeField]
    private TextMeshProUGUI SkilldamageUI1;
    private string[] SkillName = new string[] { "���ǵ彽����", "���Ǿ����", "���������", "���۽�����", "ũ�ο콽����" };
    private string[] SkillDamage = new string[] { "25", "50", "30", "10", "30" };
    private int[] plusDamage  = new int[] {5,1,5,5,5};
    private List<int> selectedIndices = new List<int>();
    [SerializeField]
    private Image SkillOJ;

    int randomIndex;
    private void Start()
    {
        
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
        else if (!selectedIndices.Contains(randomIndex))
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
                weaponSetting.setSkill("SpeedSlash", num);
                selectedIndices.RemoveAll(index => index != 0);
                SkillOJ.sprite = imgAraay[randomIndex];
                break;
            case 1:
                weaponSetting.setSkill("SpearSlash", num);
                selectedIndices.RemoveAll(index => index != 1);
                SkillOJ.sprite = imgAraay[randomIndex];
                break;
            case 2:
                weaponSetting.setSkill("SquadSlash", num);
                selectedIndices.RemoveAll(index => index != 2);
                SkillOJ.sprite = imgAraay[randomIndex];
                break;
            case 3:
                weaponSetting.setSkill("SuperSlash", num);
                selectedIndices.RemoveAll(index => index != 3);
                SkillOJ.sprite = imgAraay[randomIndex];
                break;
            case 4:
                weaponSetting.setSkill("clawsSlash", num);
                selectedIndices.RemoveAll(index => index != 4);
                SkillOJ.sprite = imgAraay[randomIndex];
                break;
        }
    }
    private void defaultSelect(int randomIndex) {
        SkillImage.sprite = imgAraay[randomIndex];

        SkillName1UI1.text = SkillName[randomIndex];
        SkilldamageUI1.text = "������ : " + SkillDamage[randomIndex];
    }
    private void select(int randomIndex)
    {
        SkillImage.sprite = imgAraay[randomIndex];
        SkillName1UI1.text = SkillName[randomIndex];
        if(randomIndex==1)
            SkilldamageUI1.text = "������ : " + plusDamage[randomIndex];
        else
            SkilldamageUI1.text = "������ : +" + plusDamage[randomIndex];
    }
}
