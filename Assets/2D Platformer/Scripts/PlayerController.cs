using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

namespace Platformer
{
	public class PlayerController : MonoBehaviour
	{
		public float movingSpeed;
		public float jumpForce;
		private float moveInput;

		public bool facingRight { get { return _facingRight; } private set { _facingRight = value; } }
		private bool _facingRight;

		private bool isGrounded;

		//Si es true, el jugador no se mover�
		private bool movementBlocked;
		public Transform groundCheck;
		public Hp hp;
		public LayerMask ground;

		public Rigidbody2D rigidbody;
		private Animator animator;
		private GameManager gameManager;
		private int jumpCounter = 0;
		private int maxJumps = 2;

		void Start()
		{
			rigidbody = GetComponent<Rigidbody2D>();
			animator = GetComponent<Animator>();
			gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		}

		private void FixedUpdate()
		{
			CheckGround();
		}

		//private int Suma(int num1, int num2)
		//{
		//	return num1 + num2;
		//}

		void Update()
		{
			if (movementBlocked) //movementBlocked == true
			{
				return;
			}

			if (Input.GetButton("Horizontal"))
			{
				moveInput = Input.GetAxis("Horizontal");
				Vector3 direction = transform.right * moveInput;
				transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, movingSpeed * Time.deltaTime);
				animator.SetInteger("playerState", 1); // Turn on run animation
			}
			else
			{
				if (isGrounded) animator.SetInteger("playerState", 0); // Turn on idle animation
			}
			if (Input.GetKeyDown(KeyCode.Space) && jumpCounter > 0)
			{
				float adjustedJumpForce = isGrounded ? jumpForce : jumpForce * 0.85f;

				if (!isGrounded)
				{
					rigidbody.velocity = Vector3.zero;
				}

				rigidbody.AddForce(transform.up * adjustedJumpForce, ForceMode2D.Impulse);
				jumpCounter--;
			}

			if (!isGrounded) animator.SetInteger("playerState", 2); // Turn on jump animation

			if (facingRight == false && moveInput > 0)
			{
				Flip();
			}
			else if (facingRight == true && moveInput < 0)
			{
				Flip();
			}
		}

		public void BlockMovement(bool value)
		{
			movementBlocked = value;
		}

		private void Flip()
		{
			facingRight = !facingRight;
			Vector3 Scaler = transform.localScale;
			Scaler.x *= -1;
			transform.localScale = Scaler;
		}

		private void CheckGround()
		{
			Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.transform.position, 0.2f, ground);
			isGrounded = colliders.Length > 1;
			if (isGrounded)
			{
				jumpCounter = (maxJumps - 1);
			}
		}

		private void OnCollisionEnter2D(Collision2D other)
		{
			if (other.transform.CompareTag("Enemy"))
			{
				hp.ReduceHp(1);
			}
		}

		private void OnTriggerEnter2D(Collider2D other)
		{
			if (other.transform.CompareTag("Coin"))
			{
				CoinChecker.instance.coinCounter++;
				Destroy(other.gameObject);
			}
		}
	}
}
