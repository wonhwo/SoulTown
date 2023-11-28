using Cinemachine;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class camara : MonoBehaviour
{
    public float shakeDuration;
    public float shakeAmplitude;
    public float shakeFrequency;

    private CinemachineVirtualCamera virtualCamera;
    [SerializeField] private float duration = 5f; // 변화에 걸리는 시간
    private void Start()
    {
        // CinemachineVirtualCamera 컴포넌트 획득
        virtualCamera = GetComponent<CinemachineVirtualCamera>();

        // Shake 효과 적용
        StartCoroutine(ResetCameraShake());
        SetDarkness(1f);

        // 5초 동안 점점 어둡게 만들기
        DOTween.To(() => GetDarkness(), SetDarkness, 0f, duration);
    }
    float GetDarkness()
    {
        return RenderSettings.ambientIntensity;
    }

    void SetDarkness(float value)
    {
        RenderSettings.ambientIntensity = value;
    }

    public void ShakeCamera()
    {
        // CinemachineVirtualCamera의 Noise 설정
        var noiseProfile = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        noiseProfile.m_AmplitudeGain = shakeAmplitude;
        noiseProfile.m_FrequencyGain = shakeFrequency;

        // 흔들림 지속 시간 후에 원래대로 복구
        StartCoroutine(ResetCameraShake());
    }

    IEnumerator ResetCameraShake()
    {
        yield return new WaitForSeconds(shakeDuration);

        // 흔들림 복구
        var noiseProfile = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        noiseProfile.m_AmplitudeGain = 0.0f;
        noiseProfile.m_FrequencyGain = 0.0f;
    }
}
