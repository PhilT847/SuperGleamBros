// FatHero
using System.Collections;
using UnityEngine;

public class FatHero : Character
{
	public ParticleSystem groundPoundStars;

	public ParticleSystem chargeParticles;

	public ParticleSystem fullChargeParticles;

	public bool chargingBellyAttack;

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
			if (stunTime > 0f && chargingBellyAttack)
			{
				chargingBellyAttack = false;
				chargeParticles.Stop();
				fullChargeParticles.Stop();
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
		StartCoroutine(FatThrust());
	}

	public override void AirAttack()
	{
		StartCoroutine(GroundPound());
	}

	public override void SpecialMove()
	{
		specialAbility.UseAbility();
	}

	public IEnumerator FatThrust()
	{
		canAct = false;
		chargingBellyAttack = true;
		rigidBody.velocity = Vector2.zero;
		characterAnim.SetBool("Running", value: false);
		characterAnim.SetTrigger("Attack");
		characterAnim.SetFloat("ChargeMult", 1f);
		yield return new WaitForSeconds(0.25f);
		float chargeTimer = 1f;
		characterAnim.SetFloat("ChargeMult", 0f);
		while (Input.GetKey(attackKey))
		{
			if (chargeTimer < 5f)
			{
				chargeTimer += Time.deltaTime;
				if (!chargeParticles.isPlaying && chargingBellyAttack)
				{
					fullChargeParticles.Stop();
					chargeParticles.Clear();
					chargeParticles.Play();
				}
			}
			else
			{
				chargeTimer = 5f;
				if (!fullChargeParticles.isPlaying && chargingBellyAttack)
				{
					chargeParticles.Stop();
					fullChargeParticles.Clear();
					fullChargeParticles.Play();
				}
			}
			yield return new WaitForEndOfFrame();
		}
		if (chargeTimer < 5f)
		{
			hurtBox.SetHurtbox((int)(20f * chargeTimer), 5f + chargeTimer, 0.25f, "Hit2");
		}
		else
		{
			hurtBox.SetHurtbox(150, 10f, 0.25f, "Hit2");
		}
		hasSuperArmor = true;
		yield return new WaitForSeconds(0.05f);
		characterVoice.PlayAttackSound();
		chargeParticles.Stop();
		fullChargeParticles.Stop();
		if (!chargingBellyAttack)
		{
			hasSuperArmor = false;
			canAct = true;
			yield break;
		}
		characterAnim.SetFloat("ChargeMult", 1f);
		chargingBellyAttack = false;
		if (facingRight)
		{
			rigidBody.velocity = new Vector2(10f * (0.5f + chargeTimer / 2f), 0f);
		}
		else
		{
			rigidBody.velocity = new Vector2(-10f * (0.5f + chargeTimer / 2f), 0f);
		}
		yield return new WaitForSeconds(0.7f);
		rigidBody.velocity = Vector2.zero;
		canAct = true;
		hasSuperArmor = false;
		yield return null;
	}

	public IEnumerator GroundPound()
	{
		canAct = false;
		hasSuperArmor = true;
		rigidBody.velocity = Vector2.zero;
		moveVector = Vector2.zero;
		affectedByGravity = false;
		characterAnim.SetTrigger("Attack");
		bool bigPound = base.transform.position.y > -1.25f;
		yield return new WaitForSeconds(0.5f);
		characterVoice.PlayAttackSound();
		if (bigPound)
		{
			hurtBox.SetHurtbox(40, 9f, 0.67f, "Hit2");
		}
		else
		{
			hurtBox.SetHurtbox(20, 6f, 0.5f, "Hit2");
		}
		while ((double)base.transform.position.y >= -3.78)
		{
			moveVector = new Vector2(0f, -25f);
			yield return new WaitForEndOfFrame();
		}
		characterAnim.SetTrigger("Pound");
		affectedByGravity = true;
		hasSuperArmor = false;
		moveVector = Vector2.zero;
		Object.FindObjectOfType<AudioManager>().Play("Hit3");
		if (bigPound)
		{
			groundPoundStars.Clear();
			groundPoundStars.Play();
		}
		GameObject.FindGameObjectWithTag("Map").GetComponent<Animator>().SetTrigger("MapShake");
		yield return new WaitForSeconds(0.01f);
		base.transform.position = new Vector3(base.transform.position.x, -3.78f, 0f);
		yield return new WaitForSeconds(0.99f);
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

