// MovingAttack
using System.Collections;
using UnityEngine;

public class MovingAttack : Ability
{
	public GameObject createdObject;

	private GameObject currentSpawn;

	public float x_Origin;

	public float y_Origin;

	public float maxCooldown;

	private float currentCooldown;

	public bool requiresGrounded;

	public bool controlDirection;

	public float chargingTime;

	public float chargingSpeed;

	private float usedSpeed;

	public int abilityDamage;

	public float abilityKnockback;

	public float abilityStun;

	public override void UseAbility()
	{
		if (currentCooldown == 0f && (!requiresGrounded || abilityOwner.isGrounded))
		{
			currentCooldown = maxCooldown;
			abilityOwner.characterVoice.PlaySpecialSound();
			StartCoroutine(BeginCharging());
		}
	}

	public override void AbilityUpdate()
	{
		if (currentCooldown > 0f)
		{
			currentCooldown -= Time.deltaTime;
		}
		else
		{
			currentCooldown = 0f;
		}
	}

	public void CreateChargeObject()
	{
		GenerateEffectParticles(abilityOwner.transform);
		float num = x_Origin;
		if (!abilityOwner.facingRight)
		{
			num *= -1f;
		}
		GameObject gameObject = Object.Instantiate(createdObject, new Vector3(abilityOwner.transform.position.x + num, abilityOwner.transform.position.y + y_Origin, 0f), Quaternion.identity);
		gameObject.GetComponentInChildren<Hurtbox>().owner = abilityOwner;
		gameObject.GetComponentInChildren<Hurtbox>().SetHurtbox(abilityDamage, abilityKnockback, abilityStun, "Hit3");
		currentSpawn = gameObject;
		usedSpeed = chargingSpeed;
		if (!abilityOwner.facingRight)
		{
			gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x * -1f, gameObject.transform.localScale.y, 1f);
			usedSpeed *= -1f;
		}
		if (controlDirection && ((Input.GetKey(abilityOwner.rightKey) && !abilityOwner.facingRight) || (Input.GetKey(abilityOwner.leftKey) && abilityOwner.facingRight)))
		{
			gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x * -1f, gameObject.transform.localScale.y, 1f);
			usedSpeed *= -1f;
		}
	}

	public IEnumerator BeginCharging()
	{
		abilityOwner.canAct = false;
		abilityOwner.characterAnim.SetTrigger("Special");
		if (abilitySpeed > 0.1f)
		{
			abilityOwner.characterAnim.SetFloat("SpecialTime", 1f / abilitySpeed);
		}
		else
		{
			abilityOwner.characterAnim.SetFloat("SpecialTime", 1f / chargingTime);
		}
		yield return new WaitForSeconds(abilitySpeed);
		if (abilityOwner.stunTime == 0f)
		{
			abilityOwner.hasSuperArmor = true;
			CreateChargeObject();
			Object.FindObjectOfType<AudioManager>().Play(abilitySound);
			float timeSpentMoving = chargingTime;
			while (timeSpentMoving > 0f)
			{
				timeSpentMoving -= Time.deltaTime;
				abilityOwner.rigidBody.velocity = new Vector2(usedSpeed, 0f);
				currentSpawn.transform.position = new Vector2(abilityOwner.transform.position.x - x_Origin, abilityOwner.transform.position.y + y_Origin);
				yield return new WaitForEndOfFrame();
			}
			abilityOwner.rigidBody.velocity = Vector2.zero;
			abilityOwner.moveVector = new Vector2(0f, 0f);
			abilityOwner.hasSuperArmor = false;
			Object.Destroy(currentSpawn);
			abilityOwner.characterAnim.SetTrigger("Special");
			abilityOwner.characterAnim.SetFloat("SpecialTime", 4f);
			yield return new WaitForSeconds(0.25f);
			if ((abilityOwner.transform.position.x > abilityOwner.opponent.transform.position.x && abilityOwner.facingRight) || (abilityOwner.transform.position.x < abilityOwner.opponent.transform.position.x && !abilityOwner.facingRight))
			{
				abilityOwner.FlipUnit();
			}
			abilityOwner.canAct = true;
			yield return null;
		}
		else
		{
			abilityOwner.canAct = true;
		}
	}
}
