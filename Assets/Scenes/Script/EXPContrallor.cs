using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EXPContrallor : MonoBehaviour
{
    [SerializeField]
    LevelUpMenuContrallor levelUpMenuContrallor;
    [SerializeField]
    LevelUpMenuContrallor1 levelUpMenuContrallor1;
    private int EXP=0;
    private int EXPMax = 50;
    [SerializeField]
    private Image EXPbar;
    [SerializeField]
    SkillUiContrallor skillUiContrallor;
    private void Update()
    {
        levelUp();
        // 현재 체력이 0에서 maxHealth 사이의 값을 가지도록 보정
        int clampedHealth = Mathf.Clamp(EXP, 0, EXPMax);

        // fillAmount에 대입할 값 계산 (0에서 1 사이의 값으로 정규화)
        float fillAmount = (float)clampedHealth / EXPMax;
        EXPbar.fillAmount = fillAmount;

    }
    public void EXPcharged(string enemyName)
    {
        if (enemyName.Equals("slime(Clone)"))
        {
            EXP += 5;
            enemyName = "";
        }
        if (enemyName.Equals("Goblin1(Clone)"))
        {
            EXP += 10;
            enemyName = "";
        }
        if (enemyName.Equals("Skeleton(Clone)"))
        {
            EXP += 20;
            enemyName = "";
        }
        if (enemyName.Equals("MageEnemy(Clone)"))
        {
            EXP += 30;
            enemyName = "";
        }
        Debug.Log(EXP);
    }
    private void levelUp()
    {
        if (EXP >= EXPMax)
        {
            levelUpMenuContrallor.ImageSelect();
            levelUpMenuContrallor1.ImageSelect();
            skillUiContrallor.MoveDownSkillUi();
            EXP = 0;
            EXPMax += 50;
        }
    }
}
