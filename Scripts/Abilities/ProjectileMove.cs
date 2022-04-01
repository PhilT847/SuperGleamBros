// ProjectileMove
using System.Collections;
using UnityEngine;

public class ProjectileMove : Ability
{
	public GameObject projectileObject;

	public float x_Origin;

	public float y_Origin;

	public float maxCooldown;

	private float currentCooldown;

	public bool particlesOnCast;

	public override void UseAbility()
	{
		if (currentCooldown == 0f)
		{
			currentCooldown = maxCooldown;
			StartCoroutine(CastProjectile());
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

	public void SpawnProjectile()
	{
		float num = x_Origin;
		if (!abilityOwner.facingRight)
		{
			num *= -1f;
		}
		Projectile component = Object.Instantiate(projectileObject, new Vector3(abilityOwner.transform.position.x + num, abilityOwner.transform.position.y + y_Origin, 0f), Quaternion.identity).GetComponent<Projectile>();
		component.projectileOwner = abilityOwner;
		if (!abilityOwner.facingRight)
		{
			component.transform.localScale = new Vector3(component.transform.localScale.x * -1f, component.transform.localScale.y, 1f);
			component.x_Speed *= -1f;
			component.knockback *= -1f;
		}
		if (abilityName == "Headshot")
		{
			component.GetComponentInChildren<SpriteRenderer>().sprite = abilityOwner.characterBody.headSprite.sprite;
		}
		if (particlesOnCast)
		{
			GenerateEffectParticles(abilityOwner.transform);
		}
	}

	public IEnumerator CastProjectile()
	{
		abilityOwner.canAct = false;
		abilityOwner.characterAnim.SetTrigger("Special");
		abilityOwner.characterAnim.SetFloat("SpecialTime", 1f / abilitySpeed);
		abilityOwner.characterVoice.PlaySpecialSound();
		yield return new WaitForSeconds(abilitySpeed / 2f);
		if (abilityOwner.stunTime == 0f)
		{
			SpawnProjectile();
			yield return new WaitForSeconds(abilitySpeed / 2f);
			abilityOwner.canAct = true;
			yield return null;
		}
		else
		{
			abilityOwner.canAct = true;
		}
	}
}
