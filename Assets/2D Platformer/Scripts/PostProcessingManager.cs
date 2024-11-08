using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class PostProcessingManager : MonoBehaviour
{
	public Volume volume;
	public Vignette vignette;
	private ChromaticAberration chromaticAberration;
	public Image deathImage;
	private Tween vignetteTween;
	public string theName;
	private Action action;
	public static PostProcessingManager instance;

	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
	}

	private void Start()
	{
		GetPostProcessFx();
	}

	public void TweenVigenette(float intensity, float tweenTime, bool isDead)
	{
		Action action = () =>
		{
			if (isDead)
			{
				deathImage.DOFade(1, 0.15F);
				Debug.Log("DEAAAD");
			}
		};

		if(vignetteTween != null)
		{
			vignetteTween.Kill();
		}

		if (!isDead)
		{
			vignetteTween = DOTween.To(() => vignette.intensity.value, x => vignette.intensity.value = x, intensity, tweenTime);
			vignetteTween.SetLoops(2, LoopType.Yoyo);
		}
		else
		{
			vignetteTween = DOTween.To(() => vignette.intensity.value, x => vignette.intensity.value = x, intensity, tweenTime);
			vignetteTween.SetLoops(0);
			vignetteTween.OnComplete(action.Invoke);	
		}
		

		//DOTween.To(()=> chromaticAberration.intensity.value, x => chromaticAberration.intensity.value = x, 0.5f, 0.25f).SetLoops(2, LoopType.Yoyo);
	}

	private void GetName()
	{
		theName = transform.name;
	}

	private void GetPostProcessFx()
	{
		volume.profile.TryGet(out vignette);
		volume.profile.TryGet(out chromaticAberration);
	}

	float VigentteGetter()
	{
		return vignette.intensity.value;
	}

	void VigentteSetter(float targetValue)
	{
		vignette.intensity.value = targetValue;
	}

	//void Example()
	//{
	//	Action <Vector3> myAction = x => transform.position = x;
	//	Func<Vector3, Vector3> myFunc = x => x = Vector3.one;
	//}
}
