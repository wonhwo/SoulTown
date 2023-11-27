using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TMPR : MonoBehaviour
{
    public TextMeshProUGUI textMeshPro;

    private void Start()
    {
        // textMeshPro 변수에는 TextMeshProUGUI 컴포넌트가 할당되어 있어야 합니다.
        // 이 스크립트를 사용하는 GameObject에 TextMeshProUGUI 컴포넌트를 추가한 후,
        // 해당 컴포넌트를 textMeshPro 변수에 할당하세요.

        // 예시: textMeshPro = GetComponent<TextMeshProUGUI>();

        // 텍스트 스타일링 및 그라데이션 설정 예제
        if (textMeshPro != null)
        {
            textMeshPro.text = "Stage 1"; // 스테이지 이름
            textMeshPro.fontSize = 36;
            textMeshPro.fontStyle = FontStyles.Bold;
            textMeshPro.alignment = TextAlignmentOptions.Center;

            // 그라데이션 설정
            VertexGradient gradient = new VertexGradient(Color.red, Color.blue, Color.green, Color.yellow);
            textMeshPro.colorGradient = gradient;
        }
        else
        {
            Debug.LogError("TextMeshProUGUI component not assigned!");
        }
    }
}
