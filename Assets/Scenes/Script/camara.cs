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
        // CinemachineVirtualCamera ������Ʈ ȹ��
        virtualCamera = GetComponent<CinemachineVirtualCamera>();

        // Shake ȿ�� ����
        StartCoroutine(ResetCameraShake());
    }

    public void ShakeCamera()
    {
        // CinemachineVirtualCamera�� Noise ����
        var noiseProfile = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        noiseProfile.m_AmplitudeGain = shakeAmplitude;
        noiseProfile.m_FrequencyGain = shakeFrequency;

        // ��鸲 ���� �ð� �Ŀ� ������� ����
        StartCoroutine(ResetCameraShake());
    }

    IEnumerator ResetCameraShake()
    {
        yield return new WaitForSeconds(shakeDuration);

        // ��鸲 ����
        var noiseProfile = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        noiseProfile.m_AmplitudeGain = 0.0f;
        noiseProfile.m_FrequencyGain = 0.0f;
    }
}
