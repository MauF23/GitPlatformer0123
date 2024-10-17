using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Fader : MonoBehaviour
{
	public Image image;

	public float fadeOutWaitTime;
	public float fadeTime;
	public float endValue;
	private int sceneToLoad;

	private void Start()
	{
		StartCoroutine(FadeOut(fadeOutWaitTime));
	}

	public void FadeInToScene(int sceneIndex)
	{
		sceneToLoad = sceneIndex;
		image.DOFade(1, fadeTime).OnComplete(SceneChange);
	}

	public void SceneChange()
	{
		SceneManager.LoadScene(sceneToLoad);
	}

	public IEnumerator FadeOut(float waitTime)
	{
		yield return new WaitForSeconds(waitTime);
		image.DOFade(endValue, fadeTime);
	}

}
