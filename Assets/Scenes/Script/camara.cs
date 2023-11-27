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

    private void Start()
    {
        // CinemachineVirtualCamera 컴포넌트 획득
        virtualCamera = GetComponent<CinemachineVirtualCamera>();

        // Shake 효과 적용
        StartCoroutine(ResetCameraShake());
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
