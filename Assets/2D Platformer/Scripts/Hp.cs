using Platformer;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hp : MonoBehaviour
{
    public int maxHp;
	public int currentHp
	{
		get { return _currentHp; } 
		private set { _currentHp = value; } 
	}

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
}
