using Platformer;
using System;
using System.Collections;
using System.Collections.Generic;

//Librería de DOTween
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

	[SerializeField]
	private int _currentHp;
	private GameManager gameManager;
	private HeartUi heartUi;

	void Start()
    {
		gameManager = GameManager.instance;
		currentHp = maxHp;
		heartUi = HeartUi.instance;
		heartUi?.InstantiateHearts(maxHp);
	}

    public void ReduceHp(int amount)
	{
		currentHp -= amount;
		currentHp = Mathf.Clamp(currentHp, 0, maxHp);
		heartUi?.ReflectHp(currentHp);

		//Efecto de daño por Tween
		spriteRenderer.DOColor(damageColor, damageTweenTime).SetLoops(2, LoopType.Yoyo).OnStart(StunPlayer).OnComplete(EndStunPlayer);

		//Aplicar efecto de Knokback
		Knockback();

		if (currentHp <= 0 && gameManager != null)
		{
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

		//Determinar a donde esta viendo el jugador para aplicar la fuerza de knobckack en la dirección opuesta
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
