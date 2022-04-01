// FrisbeeToss
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrisbeeToss : Ability
{
	public GameObject discObject;

	public Color[] potentialColors;

	public List<Color> queuedDiscs;

	public float x_Origin;

	public float y_Origin;

	public float discReloadTime;

	private float waitBeforeReload;

	public float timeCharging;

	public bool chargingDisc;

	private void Start()
	{
		GenerateNewDisc();
		GenerateNewDisc();
		GenerateNewDisc();
	}

	public override void UseAbility()
	{
		if (!chargingDisc && queuedDiscs.Count > 0)
		{
			abilityOwner.canAct = false;
			chargingDisc = true;
			abilityOwner.characterAnim.SetTrigger("Special");
			abilityOwner.characterAnim.SetFloat("SpecialTime", 0.5f);
			timeCharging = 0f;
		}
	}

	public override void AbilityUpdate()
	{
		if (queuedDiscs.Count < 3)
		{
			if (waitBeforeReload > 0f)
			{
				waitBeforeReload -= Time.deltaTime;
			}
			else
			{
				GenerateNewDisc();
				waitBeforeReload = discReloadTime;
			}
		}
		else
		{
			waitBeforeReload = discReloadTime;
		}
		if (!chargingDisc)
		{
			return;
		}
		if (abilityOwner.stunTime == 0f)
		{
			if (Input.GetKey(abilityOwner.specialKey))
			{
				if (timeCharging < 0.5f)
				{
					timeCharging += Time.deltaTime;
					return;
				}
				timeCharging = 0.5f;
				StartCoroutine(ThrowDisc(timeCharging));
			}
			else
			{
				StartCoroutine(ThrowDisc(timeCharging));
			}
		}
		else
		{
			chargingDisc = false;
			abilityOwner.canAct = true;
		}
	}

	public void GenerateNewDisc()
	{
		queuedDiscs.Add(potentialColors[Random.Range(0, potentialColors.Length)]);
		abilityOwner.associatedHealthBar.frisbeeCounter.UpdateFrisbeeCounter();
	}

	public void SpawnProjectile()
	{
		float num = x_Origin;
		if (!abilityOwner.facingRight)
		{
			num *= -1f;
		}
		DiscProjectile component = Object.Instantiate(discObject, new Vector3(abilityOwner.transform.position.x + num, abilityOwner.transform.position.y + y_Origin, 0f), Quaternion.identity).GetComponent<DiscProjectile>();
		component.projectileOwner = abilityOwner;
		component.damage = (int)(10f + timeCharging * 20f);
		component.x_Speed = 5f + timeCharging * 10f;
		component.knockback = 2f + timeCharging * 8f;
		component.stunTime = 0.25f + timeCharging * 1f;
		component.flightTime = 1.25f + timeCharging * 0.5f;
		if (!abilityOwner.facingRight)
		{
			component.transform.localScale = new Vector3(component.transform.localScale.x * -1f, component.transform.localScale.y, 1f);
			component.x_Speed *= -1f;
			component.knockback *= -1f;
		}
		component.GetComponentInChildren<SpriteRenderer>().color = queuedDiscs[0];
		queuedDiscs.RemoveAt(0);
		abilityOwner.associatedHealthBar.frisbeeCounter.UpdateFrisbeeCounter();
	}

	public IEnumerator ThrowDisc(float chargeTime)
	{
		chargingDisc = false;
		abilityOwner.canAct = false;
		abilityOwner.characterAnim.SetTrigger("Special");
		abilityOwner.characterAnim.SetFloat("SpecialTime", 1f / abilitySpeed);
		abilityOwner.characterVoice.PlaySpecialSound();
		yield return new WaitForSeconds(abilitySpeed / 2f);
		if (abilityOwner.stunTime == 0f)
		{
			SpawnProjectile();
			timeCharging = 0f;
			yield return new WaitForSeconds(abilitySpeed / 2f);
			abilityOwner.canAct = true;
			yield return null;
		}
		else
		{
			abilityOwner.canAct = true;
			timeCharging = 0f;
		}
	}
}
