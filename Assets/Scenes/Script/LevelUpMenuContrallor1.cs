using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

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
    private string[] SkillName = new string[] { "스피드슬레쉬", "스피어슬레쉬", "스퀘어슬레쉬", "슈퍼슬레쉬", "크로우슬레쉬", "라인슬레쉬" };
    private string[] SkillDamage = new string[] { "25", "50", "30", "10", "30", "20" };
    private int[] plusDamage = new int[] { 5, 1, 5, 5, 5 };
    private List<int> selectedIndices1 = new List<int>();
    [SerializeField]
    private Image SkillOJ1;

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
        // 배열에서 랜덤하게 인덱스를 선택
        randomIndex = Random.Range(0, imgAraay.Length);
        
        if (selectedIndices1.Contains(randomIndex))
        {
            num += plusDamage[randomIndex];
            isdindices = true;
            select(randomIndex);

        }
        else if(!selectedIndices1.Contains(randomIndex))
        {
            num = 0;
            isdindices = false;
            defaultSelect(randomIndex);

        }
        selectedIndices1.Add(randomIndex);


        // 선택된 이미지를 SkillImage에 할당

    }
    public void SendSkill()
    {
        switch (randomIndex)
        {
            case 0:
                weaponSetting.setSkill2("SpeedSlash", num);
                selectedIndices1.RemoveAll(index => index != 0);
                SkillOJ1.sprite = imgAraay[randomIndex];
                break;
            case 1:
                weaponSetting.setSkill2("SpearSlash", num);
                selectedIndices1.RemoveAll(index => index != 1);
                SkillOJ1.sprite = imgAraay[randomIndex];
                break;
            case 2:
                weaponSetting.setSkill2("SquadSlash", num);
                selectedIndices1.RemoveAll(index => index != 2);
                SkillOJ1.sprite = imgAraay[randomIndex];
                break;
            case 3:
                weaponSetting.setSkill2("SuperSlash", num);
                selectedIndices1.RemoveAll(index => index != 3);
                SkillOJ1.sprite = imgAraay[randomIndex];
                break;
            case 4:
                weaponSetting.setSkill2("clawsSlash", num);
                selectedIndices1.RemoveAll(index => index != 4);
                SkillOJ1.sprite = imgAraay[randomIndex];
                break;
        }
    }
    private void defaultSelect(int randomIndex) {
        SkillImage.sprite = imgAraay[randomIndex];
        SkillName1UI2.text = SkillName[randomIndex];
        SkilldamageUI2.text = "데미지 : " + SkillDamage[randomIndex];
    }
    private void select(int randomIndex)
    {
        SkillImage.sprite = imgAraay[randomIndex];
        SkillName1UI2.text = SkillName[randomIndex];
        if(randomIndex==1)
            SkilldamageUI2.text = "딜레이 : -" + plusDamage[randomIndex];
        else
            SkilldamageUI2.text = "데미지 : +" + plusDamage[randomIndex];
    }
}
