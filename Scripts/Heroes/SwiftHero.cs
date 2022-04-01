// SwiftHero
using System.Collections;
using UnityEngine;

public class SwiftHero : Character
{
	public GameObject[] possibleProjectiles;

	public float beanOrigin_X;

	private void Update()
	{
		if (canAct && stunTime == 0f)
		{
			ControlHero();
		}
		MoveHero();
	}

	public override void TakeDamage(int damage, float knockbackValue, float addedStun)
	{
		if (invulnerable)
		{
			return;
		}
		if (isBlocking)
		{
			shieldPercentage -= (float)damage / 100f;
			shieldSlider.GetComponent<Animator>().SetTrigger("ShieldHurt");
			return;
		}
		if (!hasSuperArmor)
		{
			stunTime = addedStun;
			if (knockbackValue != 0f)
			{
				StartCoroutine(ApplyPush(knockbackValue, addedStun = 0.05f));
			}
		}
		currentHP -= damage;
		if (currentHP < 1)
		{
			KillUnit();
		}
		associatedHealthBar.UpdateHealthBar();
		characterVoice.PlayHurtSound();
	}

	public override void ControlHero()
	{
		Vector2 zero = Vector2.zero;
		if (Input.GetKey(rightKey) && !Input.GetKey(leftKey))
		{
			zero.x = speedMult;
		}
		else if (Input.GetKey(leftKey) && !Input.GetKey(rightKey))
		{
			zero.x = 0f - speedMult;
		}
		else
		{
			zero.x = 0f;
		}
		characterAnim.SetBool("Running", isGrounded && zero.x != 0f);
		if (Input.GetKey(upKey) && isGrounded)
		{
			Jump();
		}
		if (Input.GetKeyDown(attackKey))
		{
			if (isGrounded)
			{
				GroundAttack();
			}
			else
			{
				AirAttack();
			}
		}
		else if (Input.GetKey(specialKey))
		{
			SpecialMove();
		}
		moveVector.x = zero.x;
	}

	public override void GroundAttack()
	{
		StartCoroutine(BeginSlashing());
	}

	public override void AirAttack()
	{
		StartCoroutine(AirBean());
	}

	public override void SpecialMove()
	{
		specialAbility.UseAbility();
	}

	public IEnumerator BeginSlashing()
	{
		canAct = false;
		rigidBody.velocity = Vector2.zero;
		characterAnim.SetBool("Running", value: false);
		characterAnim.SetTrigger("Attack");
		characterVoice.PlayAttackSound();
		yield return new WaitForSeconds(0.1f);
		float slashTimer = 2f;
		float hurtboxRefreshTimer = 0.1f;
		hurtBox.SetHurtbox(2, 2.5f, 0.4f, "Slash");
		while (slashTimer > 0f)
		{
			hurtboxRefreshTimer -= Time.deltaTime;
			if (hurtboxRefreshTimer < 0f)
			{
				hurtBox.SetHurtbox(2, 2.5f, 0.4f, "Slash");
				hurtboxRefreshTimer = 0.1f;
			}
			if (Input.GetKey(attackKey))
			{
				slashTimer -= Time.deltaTime;
				if (stunTime > 0f)
				{
					canAct = true;
					yield break;
				}
				yield return new WaitForEndOfFrame();
				continue;
			}
			StartCoroutine(FinishSlashing());
			yield break;
		}
		StartCoroutine(FinishSlashing());
		yield return null;
	}

	public IEnumerator FinishSlashing()
	{
		canAct = false;
		characterVoice.PlayAttackSound();
		hurtBox.GetComponent<Collider2D>().enabled = false;
		characterAnim.SetTrigger("FinalSlash");
		if (facingRight)
		{
			StartCoroutine(ApplyPush(3f, 0.67f));
		}
		else
		{
			StartCoroutine(ApplyPush(-3f, 0.67f));
		}
		hurtBox.SetHurtbox(8, 12f, 0.25f, "Slash");
		yield return new WaitForSeconds(0.67f);
		canAct = true;
		yield return null;
	}

	public IEnumerator AirBean()
	{
		canAct = false;
		characterAnim.SetTrigger("Attack");
		float halvedSpeed = rigidBody.velocity.x / 2f;
		characterVoice.PlayAttackSound();
		StartCoroutine(ApplyPush(halvedSpeed, 0.08f));
		yield return new WaitForSeconds(0.08f);
		if (stunTime == 0f)
		{
			CreateBeans();
			StartCoroutine(ApplyPush(halvedSpeed, 0.33f));
			yield return new WaitForSeconds(0.33f);
			canAct = true;
			yield return null;
		}
		else
		{
			canAct = true;
		}
	}

	private void CreateBeans()
	{
		float num = beanOrigin_X;
		if (!facingRight)
		{
			num *= -1f;
		}
		float num2 = 8f;
		float num3 = -3f;
		for (int i = 0; i < 3; i++)
		{
			int num4 = Random.Range(0, 20);
			num4 = ((num4 >= 14) ? ((num4 < 19) ? 1 : 2) : 0);
			Projectile component = Object.Instantiate(possibleProjectiles[num4], new Vector3(base.transform.position.x + num, base.transform.position.y, 0f), Quaternion.identity).GetComponent<Projectile>();
			component.projectileOwner = this;
			component.x_Speed = num2;
			component.y_Speed = num3;
			if (!facingRight)
			{
				component.transform.localScale = new Vector3(component.transform.localScale.x * -1f, component.transform.localScale.y, 1f);
				component.x_Speed *= -1f;
				component.knockback *= -1f;
			}
			num2 -= 2f;
			num3 -= 2f;
		}
	}

	public void Jump()
	{
		base.transform.position = new Vector3(base.transform.position.x, -3.75f, 0f);
		isGrounded = false;
		characterAnim.SetTrigger("Jump");
		characterAnim.ResetTrigger("Land");
		moveVector.y = 10f;
	}
}
