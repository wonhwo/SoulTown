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
    private GameManager gameManager;
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
            EXP += 50;
            enemyName = "";
        }
        if (enemyName.Equals("Goblin1(Clone)"))
        {
            EXP += 50;
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
            gameManager.TogglePause();
            EXP = 0;
            EXPMax += 50;
        }
    }
}
