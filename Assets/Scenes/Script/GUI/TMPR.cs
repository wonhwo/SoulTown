using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TMPR : MonoBehaviour
{
    public TextMeshProUGUI textMeshPro;

    private void Start()
    {
        // textMeshPro �������� TextMeshProUGUI ������Ʈ�� �Ҵ�Ǿ� �־�� �մϴ�.
        // �� ��ũ��Ʈ�� ����ϴ� GameObject�� TextMeshProUGUI ������Ʈ�� �߰��� ��,
        // �ش� ������Ʈ�� textMeshPro ������ �Ҵ��ϼ���.

        // ����: textMeshPro = GetComponent<TextMeshProUGUI>();

        // �ؽ�Ʈ ��Ÿ�ϸ� �� �׶��̼� ���� ����
        if (textMeshPro != null)
        {
            textMeshPro.text = "Stage 1"; // �������� �̸�
            textMeshPro.fontSize = 36;
            textMeshPro.fontStyle = FontStyles.Bold;
            textMeshPro.alignment = TextAlignmentOptions.Center;

            // �׶��̼� ����
            VertexGradient gradient = new VertexGradient(Color.red, Color.blue, Color.green, Color.yellow);
            textMeshPro.colorGradient = gradient;
        }
        else
        {
            Debug.LogError("TextMeshProUGUI component not assigned!");
        }
    }
}
