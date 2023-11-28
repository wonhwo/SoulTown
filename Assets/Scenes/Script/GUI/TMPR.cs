using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TMPR : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textGameObject;
    [SerializeField] private Button button;
    [SerializeField] private Image image1;
    [SerializeField] private Image image2;
    [SerializeField] private GameObject Boss;

    void Start()
    {
        button.onClick.AddListener(() => { Destroy(Boss); LoadingMnager.LoadScene("StartMenu"); });
        textGameObject.color = new Color(1f, 1f, 1f, 0f); // 투명도를 0으로 설정
        button.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0f);
        image1.color = new Color(1f, 1f, 1f, 0f);
        image2.color = new Color(1f, 1f, 1f, 0f);

        DOTween.Sequence()
            .Append(DOTween.To(() => textGameObject.color, x => textGameObject.color = x, new Color(1f, 1f, 1f, 1f), 3f)) // 텍스트의 투명도를 1로 변경
            .Join(DOTween.To(() => button.GetComponent<Image>().color, x => button.GetComponent<Image>().color = x, new Color(1f, 1f, 1f, 0.5f), 3f)) // 버튼의 투명도를 0.5로 변경
            .Join(DOTween.To(() => image1.color, x => image1.color = x, new Color(1f, 1f, 1f, 1f), 3f)) // 이미지1의 투명도를 0.5로 변경
            .Join(DOTween.To(() => image2.color, x => image2.color = x, new Color(1f, 1f, 1f, 1f), 3f)) // 이미지2의 투명도를 0.5로 변경
            .OnComplete(() =>
            {
                DOTween.Sequence()
                    .Append(DOTween.To(() => textGameObject.color, x => textGameObject.color = x, new Color(1f, 1f, 1f, 0f), 3f)) // 텍스트의 투명도를 1로 변경
                            .Join(DOTween.To(() => image1.color, x => image1.color = x, new Color(1f, 1f, 1f, 0f), 3f)) // 이미지1의 투명도를 0.5로 변경
            .Join(DOTween.To(() => image2.color, x => image2.color = x, new Color(1f, 1f, 1f, 0f), 3f)); // 이미지2의 투명도를 0.5로 변경
            });
    }
}
