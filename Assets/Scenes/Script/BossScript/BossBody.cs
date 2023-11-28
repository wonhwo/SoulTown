using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class BossBody : MonoBehaviour
{
    [SerializeField] private Image shieldBar;
    [SerializeField] private Image HpBar;
    [SerializeField] private GameObject barrier;
    [SerializeField] private BossController bossController;
    [SerializeField] private CircleCollider2D circleCollider;
    private BoxCollider2D boxCollider;



    private static int bossHP = 1; // 시작할 때 체력을 0으로 초기화
    private const int bossMaxHP = 100;
    private static int bossShield = 1; // 시작할 때 쉴드를 0으로 초기화
    private const int bossMaxShield = 100;
    private static int damage;
    private static int shiedDamage=100;

    private void Awake()
    {
        boxCollider=gameObject.GetComponent<BoxCollider2D>();
        // DOTween 초기화
        DOTween.Init();

        // 시작할 때 두트윈을 사용하여 체력과 쉴드를 채우는 애니메이션
        DOTween.To(() => bossShield, x => bossShield = x, bossMaxShield, 5f)
            .OnUpdate(() => statusController()); // 쉴드를 업데이트

        DOTween.To(() => bossHP, x => bossHP = x, bossMaxHP, 3f)
            .OnUpdate(() => statusController()); // 체력을 업데이트
    }
    public IEnumerator BreakBarrier()
    {
        DOTween.To(() => bossShield, x => bossShield = x, bossShield - shiedDamage, 2f)
            .OnUpdate(() => statusController());

        yield return new WaitUntil(() => bossShield == 0);

        // 실행할 코드
        Debug.Log("Barrier Broken!");
        circleCollider.isTrigger = true;
        bossController.StartMoving();
        boxCollider.enabled = true;
        barrier.SetActive(false);
    }
    bool isdead=true;
    private void Update()
    {
        statusController();
        if (bossHP <= 0&&isdead)
        {
            bossController.isDead();
            isdead=false;
        }
    }

    private void DecreaseHealth()
    {
        DOTween.To(() => bossHP, x => bossHP = x, bossHP - damage, 2f)
            .OnUpdate(() => statusController()); // 감소 중에 체력을 업데이트
    }
    public void SetDamage(int damageAmount)
    {
        // 변수 전달 받기
        damage = damageAmount;
        DecreaseHealth();
        page2Start();
    }
    bool isPage=true;
    private void page2Start()
    {
        if (bossHP<=bossMaxHP/2&& isPage)
        {
            barrier.SetActive(true);
            boxCollider.enabled = false;
            circleCollider.isTrigger = false;
            DOTween.To(() => bossShield, x => bossShield = x, bossMaxShield, 5f)
                .OnUpdate(() => statusController()); // 쉴드를 업데이트
            bossController.isCenterTure();
            isPage = false;
        }
    }

    private void statusController()
    {
        //쉴드 업데이트
        int clampedShield = Mathf.Clamp(bossShield, 0, bossMaxShield);

        float fillAmountShield = (float)clampedShield / bossMaxShield;
        shieldBar.fillAmount = fillAmountShield;

        //hp업데이트
        int clampedHealth = Mathf.Clamp(bossHP, 0, bossMaxHP);

        float fillAmountHealth = (float)clampedHealth / bossMaxHP;
        HpBar.fillAmount = fillAmountHealth;
    }


}
