// Projectile
using UnityEngine;

public class Projectile : MonoBehaviour
{
	public Character projectileOwner;

	public GameObject onHitParticles;

	public string hitSound;

	public int damage;

	public float knockback;

	public float stunTime;

	public float duration;

	public float x_Speed;

	public float y_Speed;

	public float x_Acceleration;

	public bool ignoresShield;

	public bool pullToCaster;

	private void Update()
	{
		MoveProjectile();
	}

	private void MoveProjectile()
	{
		base.transform.position = new Vector3(base.transform.position.x + x_Speed * Time.deltaTime, base.transform.position.y + y_Speed * Time.deltaTime, 0f);
		duration -= Time.deltaTime;
		if (x_Speed > 0f)
		{
			x_Speed += x_Acceleration * Time.deltaTime;
		}
		else
		{
			x_Speed -= x_Acceleration * Time.deltaTime;
		}
		if (duration < 0f || base.transform.position.y < -3.78f)
		{
			Explode();
		}
	}

	private void Explode()
	{
		if (onHitParticles != null)
		{
			GameObject obj = Object.Instantiate(onHitParticles, new Vector3(base.transform.position.x, base.transform.position.y, 0f), Quaternion.identity);
			obj.GetComponentInChildren<ParticleSystem>().Clear();
			obj.GetComponentInChildren<ParticleSystem>().Play();
			Object.Destroy(obj.gameObject, 1f);
		}
		Object.Destroy(base.gameObject);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (((bool)collision.GetComponent<Character>() && collision.GetComponent<Character>() != projectileOwner) || (collision.tag == "ShieldCollider" && collision.transform.parent.parent.parent.GetComponent<Character>() != projectileOwner))
		{
			Character character = null;
			character = ((!(collision.tag == "ShieldCollider")) ? collision.GetComponent<Character>() : collision.transform.parent.parent.parent.GetComponent<Character>());
			if (ignoresShield)
			{
				character.isBlocking = false;
				character.characterAnim.SetBool("Blocking", value: false);
				character.shieldSlider.gameObject.SetActive(value: false);
			}
			if (pullToCaster)
			{
				knockback = -1.8f * (character.transform.position.x - projectileOwner.transform.position.x);
			}
			character.TakeDamage(damage, knockback, stunTime);
			Object.FindObjectOfType<AudioManager>().Play(hitSound);
			Explode();
		}
	}
}
