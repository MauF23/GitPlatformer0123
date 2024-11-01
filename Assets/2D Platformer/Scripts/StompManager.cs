using Platformer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class StompTrigger : MonoBehaviour
{
	public Collider2D collider;
	public PlayerController controller;
	public float bounceForce;
	CameraManager cameraManager;

	private void Start()
	{
		cameraManager = CameraManager.instance;
		collider.isTrigger = true;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Enemy"))
		{
			StartCoroutine(FreezeFrame());
			cameraManager.HitEnemyShake();
			collision.transform.DOScale(Vector3.zero, 0.15f);
			controller.rigidbody.velocity = new Vector2(controller.rigidbody.velocity.x, 0);
			controller.rigidbody.AddForce(Vector2.up * bounceForce);
		}
	}

	IEnumerator FreezeFrame()
	{
		Time.timeScale = 0;
		yield return new WaitForSecondsRealtime(0.15f);
		Time.timeScale = 1;
	}
}