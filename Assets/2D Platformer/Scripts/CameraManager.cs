using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
	[Header("DamageShake")]
	public float damageShakeAmplitude;
	public float damageShakeFrecuency;

	[Range(0f, 2f)]
	public float damageShakeDuration;

	[Header("HitEnemyShake")]
	public float hitShakeAmplitude;
	public float hitShakeFrecuency;

	[Range(0f, 2f)]
	public float hitShakeDuration;


	public CinemachineVirtualCamera virtualCamera;

	private CinemachineBasicMultiChannelPerlin noise;

	public static CameraManager instance;

	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
	}

	void Start()
	{
		noise = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
		ResetShake();
	}

	public void DamageShake()
	{
		StartCoroutine(ShakeRoutine(damageShakeAmplitude, damageShakeFrecuency, damageShakeDuration));
	}

	public void HitEnemyShake()
	{
		StartCoroutine(ShakeRoutine(hitShakeAmplitude, hitShakeAmplitude, hitShakeDuration));
	}

	private void ResetShake()
	{
		noise.m_AmplitudeGain = 0;
		noise.m_FrequencyGain = 0;
	}

	IEnumerator ShakeRoutine(float amplitude, float frecuency, float duration)
	{
		noise.m_AmplitudeGain = amplitude;
		noise.m_FrequencyGain = frecuency;
		yield return new WaitForSeconds(duration);
		ResetShake();
	}
}
