using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MovingPlatform : MonoBehaviour
{
	public Transform [] destinations;
	private Transform currentDestination;
	public Transform platform;
	public Ease ease;
	public float movementTime;
	private int counter;

	private void Start()
	{
		
		counter = 0;
		currentDestination = destinations[destinations.Length-1];

		platform.DOMove(currentDestination.position, movementTime).OnComplete(UpdateDestination).SetLoops(-1, LoopType.Yoyo)
			.SetEase(ease);
	}

	private void UpdateDestination()
	{
		counter++;
		if(counter > destinations.Length)
		{
			counter = 0;
		}

		currentDestination = destinations[counter];
	}
}
