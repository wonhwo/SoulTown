using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class BossBody : MonoBehaviour
{
    [SerializeField] private Image shieldBar;
    [SerializeField] private Image HpBar;
    [SerializeField] private GameObject barrier;
    [SerializeField]
    private BossController bossController;

    private static int bossHP = 0; // ������ �� ü���� 0���� �ʱ�ȭ
    private const int bossMaxHP = 2000;
    private static int bossShield = 1; // ������ �� ���带 0���� �ʱ�ȭ
    private const int bossMaxShield = 100;
    private static int damage;
    private static int shiedDamage=100;

    private void Start()
    {
        // DOTween �ʱ�ȭ
        DOTween.Init();

        // ������ �� ��Ʈ���� ����Ͽ� ü�°� ���带 ä��� �ִϸ��̼�
        DOTween.To(() => bossShield, x => bossShield = x, bossMaxShield, 5f)
            .OnUpdate(() => statusController()); // ���带 ������Ʈ

        DOTween.To(() => bossHP, x => bossHP = x, bossMaxHP, 3f)
            .OnUpdate(() => statusController()); // ü���� ������Ʈ
    }
    public IEnumerator BreakBarrier()
    {
        DOTween.To(() => bossShield, x => bossShield = x, bossShield - shiedDamage, 2f)
            .OnUpdate(() => statusController());

        yield return new WaitUntil(() => bossShield == 0);

        // ������ �ڵ�
        Debug.Log("Barrier Broken!");

        bossController.StartMoving();
        barrier.SetActive(false);
    }
    private void Update()
    {
        statusController();
    }

    private void DecreaseHealth()
    {
        DOTween.To(() => bossHP, x => bossHP = x, bossHP - damage, 2f)
            .OnUpdate(() => statusController()); // ���� �߿� ü���� ������Ʈ
    }
    public void SetDamage(int damageAmount)
    {
        // ���� ���� �ޱ�
        damage = damageAmount;
        DecreaseHealth();
    }

    private void statusController()
    {
        //���� ������Ʈ
        int clampedShield = Mathf.Clamp(bossShield, 0, bossMaxShield);

        float fillAmountShield = (float)clampedShield / bossMaxShield;
        shieldBar.fillAmount = fillAmountShield;

        //hp������Ʈ
        int clampedHealth = Mathf.Clamp(bossHP, 0, bossMaxHP);

        float fillAmountHealth = (float)clampedHealth / bossMaxHP;
        HpBar.fillAmount = fillAmountHealth;
    }


}