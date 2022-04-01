// Hurtbox
using UnityEngine;

public class Hurtbox : MonoBehaviour
{
	public Character owner;

	public int damage;

	public float knockbackPower;

	public float stunTime;

	public string currentNoise;

	public bool alreadyHitEnemy;

	public void SetHurtbox(int newDamage, float newKnockbackPower, float newStunTime, string noise)
	{
		alreadyHitEnemy = false;
		damage = newDamage;
		if (owner.facingRight)
		{
			newKnockbackPower *= -1f;
		}
		knockbackPower = newKnockbackPower;
		stunTime = newStunTime;
		currentNoise = noise;
	}

	public void HitEnemy(Character hitEnemy)
	{
		hitEnemy.TakeDamage(damage, -1f * knockbackPower, stunTime);
		if (currentNoise != "")
		{
			Object.FindObjectOfType<AudioManager>().Play(currentNoise);
		}
	}

	private void OnTriggerStay2D(Collider2D collision)
	{
		if ((((bool)collision.GetComponent<Character>() && collision.GetComponent<Character>() != owner) || (collision.tag == "ShieldCollider" && collision.transform.parent.parent.parent.GetComponent<Character>() != owner)) && !alreadyHitEnemy)
		{
			Character character = null;
			character = ((!(collision.tag == "ShieldCollider")) ? collision.GetComponent<Character>() : collision.transform.parent.parent.parent.GetComponent<Character>());
			alreadyHitEnemy = true;
			HitEnemy(character);
		}
	}
}
