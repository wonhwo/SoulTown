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
    private int EXPMax = 100;
    [SerializeField]
    private Image EXPbar;
    [SerializeField]
    private SkillUiContrallor SkillUiContrallor;
    private void Update()
    {
        levelUp();
        // ���� ü���� 0���� maxHealth ������ ���� �������� ����
        int clampedHealth = Mathf.Clamp(EXP, 0, EXPMax);

        // fillAmount�� ������ �� ��� (0���� 1 ������ ������ ����ȭ)
        float fillAmount = (float)clampedHealth / EXPMax;
        EXPbar.fillAmount = fillAmount;

    }
    public void EXPcharged(string enemyName)
    {
        if (enemyName.Equals("slime(Clone)"))
        {
            EXP += 20;
            enemyName = "";
        }
        if (enemyName.Equals("Goblin1(Clone)"))
        {
            EXP += 20;
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
            SkillUiContrallor.MoveDownSkillUi();
            EXP = 0;
            EXPMax += 50;
        }
    }
}
