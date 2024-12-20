using Platformer;
using System;
using System.Collections;
using System.Collections.Generic;

//Librer�a de DOTween
using DG.Tweening;
using UnityEngine;

public class Hp : MonoBehaviour
{
	public int maxHp;
	public int currentHp
	{
		get { return _currentHp; }
		private set { _currentHp = value; }
	}
	public SpriteRenderer spriteRenderer;
	public Color damageColor;
	public float damageTweenTime;

	public Rigidbody2D rigidbody;
	public float knockbackForce;
	private float damageEffectModifier = 0;

	[SerializeField]
	private int _currentHp;
	private GameManager gameManager;
	private CameraManager cameraManager;
	private PostProcessingManager postProcessingManager;
	private HeartUi heartUi;

	void Start()
	{
		gameManager = GameManager.instance;
		cameraManager = CameraManager.instance;
		postProcessingManager = PostProcessingManager.instance;
		currentHp = maxHp;
		heartUi = HeartUi.instance;
		heartUi?.InstantiateHearts(maxHp);
	}

	public void ReduceHp(int amount)
	{
		currentHp -= amount;
		currentHp = Mathf.Clamp(currentHp, 0, maxHp);
		heartUi?.ReflectHp(currentHp);

		//Efecto de da�o por Tween
		spriteRenderer.DOColor(damageColor, damageTweenTime).SetLoops(2, LoopType.Yoyo).OnStart(StunPlayer).OnComplete(EndStunPlayer);
		cameraManager.DamageShake();
		postProcessingManager.TweenVigenette(0.5f + damageEffectModifier, 0.15f, false);
		damageEffectModifier += 0.2f;

		//Aplicar efecto de Knokback
		Knockback();

		if (currentHp <= 0 && gameManager != null)
		{
			postProcessingManager.TweenVigenette(1, 0.15f, true);
			gameManager.GameOver();
		}
	}

	public void Revive()
	{
		currentHp = maxHp;
		heartUi?.ReflectHp(currentHp);
	}

	private void Knockback()
	{
		Vector2 direction = Vector2.zero;

		//Determinar a donde esta viendo el jugador para aplicar la fuerza de knobckack en la direcci�n opuesta
		if (gameManager.player.facingRight)
		{
			direction = Vector2.left;
		}
		else
		{
			direction = Vector2.right;
		}

		rigidbody.AddForce(direction * knockbackForce);
		rigidbody.AddForce(Vector2.up * knockbackForce);

	}

	private void StunPlayer()
	{
		//Debug.Log("StunPlayer");
		gameManager.player.BlockMovement(true);
	}

	private void EndStunPlayer()
	{
		//Debug.Log("EndStunPlayer");
		gameManager.player.BlockMovement(false);
	}
}
