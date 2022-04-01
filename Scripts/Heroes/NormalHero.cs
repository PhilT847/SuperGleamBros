// NormalHero
using System.Collections;
using UnityEngine;

public class NormalHero : Character
{
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
			shieldPercentage -= (float)damage / 50f;
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
		StartCoroutine(ComboAttack());
	}

	public override void AirAttack()
	{
		StartCoroutine(AirKick());
	}

	public override void SpecialMove()
	{
		specialAbility.UseAbility();
	}

	public IEnumerator ComboAttack()
	{
		canAct = false;
		rigidBody.velocity = Vector2.zero;
		characterAnim.SetBool("Running", value: false);
		characterAnim.SetTrigger("Attack");
		hurtBox.SetHurtbox(6, 5f, 0.2f, "Hit1");
		yield return new WaitForSeconds(0.17f);
		characterVoice.PlayAttackSound();
		float timeToCombo = 0.33f;
		bool continuingCombo = false;
		while (timeToCombo > 0f)
		{
			timeToCombo -= Time.deltaTime;
			if (Input.GetKey(attackKey))
			{
				continuingCombo = true;
			}
			yield return new WaitForEndOfFrame();
		}
		if (continuingCombo && stunTime == 0f)
		{
			hurtBox.SetHurtbox(12, 8f, 0.5f, "Hit2");
			characterVoice.PlayAttackSound();
			yield return new WaitForSeconds(0.5f);
		}
		else
		{
			characterAnim.SetTrigger("BackToIdle");
		}
		canAct = true;
		yield return null;
	}

	public IEnumerator AirKick()
	{
		canAct = false;
		characterAnim.SetTrigger("Attack");
		float originalSpeed = rigidBody.velocity.x;
		StartCoroutine(ApplyPush(originalSpeed, 0.25f));
		yield return new WaitForSeconds(0.25f);
		characterVoice.PlayAttackSound();
		hurtBox.SetHurtbox(15, 5f, 0.4f, "Hit2");
		StartCoroutine(ApplyPush(originalSpeed, 0.33f));
		yield return new WaitForSeconds(0.33f);
		canAct = true;
		yield return null;
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
