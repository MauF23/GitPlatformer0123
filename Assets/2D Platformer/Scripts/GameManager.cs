using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

namespace Platformer
{
	public class GameManager : MonoBehaviour
	{
		public GameObject playerGameObject;
		public PlayerController player;
		public GameObject deathPlayerPrefab;
		public Text coinText;
		public Fader fader;
		public CanvasGroup canvasGroup;
		public KeyCode pauseKey;
		private bool paused;


		public static GameManager instance;
		public int hp 
		{
			get 
			{ 
				return _hp; 
			}
			set 
			{ 
				_hp = value;
				Debug.Log($"MyHpIs{_hp}");
			} 
		}
		private int _hp;

		private void Awake()
		{
			paused = false;
			DontDestroyOnLoad(gameObject);

			//Asegurarse que si ya existe el manager, que solo haya una instancia
			if (instance == null)
			{
				instance = this;
			}
			else
			{
				Destroy(gameObject);
			}
		}

		private void Update()
		{
			if (Input.GetKeyDown(pauseKey))
			{
				TogglePause();
			}
		}


		public void TogglePause()
		{
			paused = !paused;

			if (paused)
			{
				canvasGroup.DOFade(0.5f, 0.25f).SetUpdate(true);
				Time.timeScale = 0;
			}
			else
			{
				canvasGroup.DOFade(0, 0.25f).SetUpdate(true);
				Time.timeScale = 1;
			}
		}

		private void ReloadLevel()
		{
			LoadScene(SceneManager.GetActiveScene().buildIndex);
		}

		public void LoadScene(int level)
		{
			fader.FadeInToScene(level);
		}

		public void ResetPlayer()
		{
			coinText.text = 0.ToString();
			playerGameObject.SetActive(true);
			player.hp.Revive();
		}

		public void GameOver()
		{
			playerGameObject.SetActive(false);
			GameObject deathPlayer = Instantiate(deathPlayerPrefab, playerGameObject.transform.position, playerGameObject.transform.rotation);
			deathPlayer.transform.localScale = new Vector3(playerGameObject.transform.localScale.x, playerGameObject.transform.localScale.y, playerGameObject.transform.localScale.z);
			ReloadLevel();
		}
	}
}
