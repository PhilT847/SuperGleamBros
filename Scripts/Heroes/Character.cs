// Character
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public abstract class Character : MonoBehaviour
{
	public string characterName;

	public int maxHP;

	public int currentHP;

	public float speedMult;

	public float jumpHeight;

	public bool isGrounded;

	public Vector2 moveVector;

	private float sprintMult;

	public float stunTime;

	public bool canAct;

	public bool invulnerable;

	public bool hasSuperArmor;

	public Body characterBody;

	public Animator characterAnim;

	public Transform characterShadow;

	public Rigidbody2D rigidBody;

	public bool affectedByGravity;

	public bool beingPushed;

	public Hurtbox hurtBox;

	public bool isBlocking;

	public float shieldPercentage;

	public Slider shieldSlider;

	public Transform grabber;

	private Character grabbedCharacter;

	public Ability specialAbility;

	public HealthBar associatedHealthBar;

	public Voice characterVoice;

	public Transform flippedBody;

	[HideInInspector]
	public bool facingRight;

	public KeyCode rightKey;

	public KeyCode leftKey;

	public KeyCode upKey;

	public KeyCode downKey;

	public KeyCode attackKey;

	public KeyCode specialKey;

	public Character opponent;

	public int crowns;

	public abstract void TakeDamage(int damage, float knockbackValue, float stunTime);

	public abstract void ControlHero();

	public abstract void GroundAttack();

	public abstract void AirAttack();

	public abstract void SpecialMove();

	public void KillUnit()
	{
		Object.FindObjectOfType<GameController>().StartCoroutine(Object.FindObjectOfType<GameController>().WinRound(opponent, this));
		currentHP = 0;
	}

	public void FlipUnit()
	{
		facingRight = !facingRight;
		flippedBody.localScale = new Vector3(flippedBody.localScale.x * -1f, flippedBody.localScale.y, 1f);
		shieldSlider.GetComponent<RectTransform>().localScale = new Vector3(shieldSlider.GetComponent<RectTransform>().localScale.x * -1f, 1f, 1f);
	}

	public void MoveHero()
	{
		if (base.transform.position.x > 8f)
		{
			base.transform.position = new Vector2(8f, base.transform.position.y);
		}
		else if (base.transform.position.x < -8f)
		{
			base.transform.position = new Vector2(-8f, base.transform.position.y);
		}
		if (stunTime > 0f)
		{
			stunTime -= Time.deltaTime;
		}
		else
		{
			stunTime = 0f;
		}
		if (!isGrounded && base.transform.position.y < -3.78f)
		{
			base.transform.position = new Vector3(base.transform.position.x, -3.78f, 0f);
			isGrounded = true;
			characterAnim.SetTrigger("Land");
			moveVector.y = 0f;
		}
		ApplyGravity();
		if (isGrounded)
		{
			if ((Input.GetKey(rightKey) || Input.GetKey(leftKey)) && sprintMult < 1.75f)
			{
				sprintMult += 1.5f * Time.deltaTime;
			}
			else if (sprintMult > 1f)
			{
				sprintMult -= 4f * Time.deltaTime;
			}
			else
			{
				sprintMult = 1f;
			}
		}
		else
		{
			sprintMult = 1.75f;
		}
		if ((!Input.GetKey(rightKey) && !Input.GetKey(leftKey)) || stunTime > 0f || !canAct)
		{
			if (Mathf.Abs(rigidBody.velocity.x) > 0.05f)
			{
				sprintMult = 1f;
				if (rigidBody.velocity.x > 0f)
				{
					moveVector = new Vector2(rigidBody.velocity.x - 20f * Time.deltaTime, moveVector.y);
				}
				else
				{
					moveVector = new Vector2(rigidBody.velocity.x + 20f * Time.deltaTime, moveVector.y);
				}
			}
			else
			{
				moveVector.x = 0f;
			}
		}
		moveVector = new Vector2(moveVector.x * sprintMult, moveVector.y);
		rigidBody.velocity = moveVector;
		if (canAct && ((opponent.transform.position.x > base.transform.position.x && !facingRight) || (opponent.transform.position.x < base.transform.position.x && facingRight)))
		{
			FlipUnit();
		}
		characterShadow.position = new Vector3(base.transform.position.x, -3.8f, 0f);
		float num = 1f - (base.transform.position.y + 3.78f) * 0.1f;
		if (num < 0.33f)
		{
			num = 0.33f;
		}
		else if (num > 1f)
		{
			num = 1f;
		}
		characterShadow.localScale = new Vector3(2f * num, 0.5f * num, 1f);
		if (Input.GetKey(downKey) && isGrounded && canAct && stunTime == 0f)
		{
			rigidBody.velocity = Vector2.zero;
			characterAnim.SetBool("Running", value: false);
			Block();
		}
		else
		{
			isBlocking = false;
			characterAnim.SetBool("Blocking", value: false);
			shieldSlider.gameObject.SetActive(value: false);
			if (shieldPercentage < 1f)
			{
				shieldPercentage += 0.1f * Time.deltaTime;
			}
		}
		characterAnim.SetBool("Grounded", isGrounded);
		characterAnim.SetFloat("StunTime", stunTime);
		if (specialAbility != null)
		{
			specialAbility.AbilityUpdate();
		}
	}

	public IEnumerator ApplyPush(float pushValue, float time)
	{
		while (time > 0f)
		{
			time -= Time.deltaTime;
			rigidBody.velocity = new Vector2(pushValue, rigidBody.velocity.y);
			if (base.transform.position.x > 7.98f && pushValue > 0f)
			{
				pushValue *= -1f;
				base.transform.position = new Vector3(7.97f, base.transform.position.y, 0f);
			}
			else if (base.transform.position.x < -7.98f && pushValue < 0f)
			{
				pushValue *= -1f;
				base.transform.position = new Vector3(-7.97f, base.transform.position.y, 0f);
			}
			yield return new WaitForEndOfFrame();
		}
		moveVector.x = 0f;
		yield return null;
	}

	public void ApplyGravity()
	{
		if (isGrounded || !affectedByGravity)
		{
			return;
		}
		float num = 1f;
		if (moveVector.y > 0f && stunTime == 0f)
		{
			if (base.transform.position.y < jumpHeight - 2f)
			{
				num = 1f;
			}
			else if (base.transform.position.y < jumpHeight)
			{
				num = 0.1f;
			}
			else if (base.transform.position.y > jumpHeight)
			{
				num = 2f;
			}
		}
		else if (base.transform.position.y < jumpHeight - 1f)
		{
			num = 3f;
		}
		moveVector = new Vector2(moveVector.x, moveVector.y -= 15f * Time.deltaTime * num);
	}

	public void Block()
	{
		shieldSlider.value = shieldPercentage;
		shieldSlider.gameObject.SetActive(value: true);
		if (shieldPercentage > 0f)
		{
			characterAnim.SetBool("Blocking", value: true);
			shieldPercentage -= 0.2f * Time.deltaTime;
			isBlocking = true;
		}
		else
		{
			isBlocking = false;
			characterAnim.SetBool("Blocking", value: false);
			TakeDamage(0, 0f, 5f);
			rigidBody.velocity = Vector2.zero;
			Object.FindObjectOfType<AudioManager>().Play("Shatter");
		}
	}
}
