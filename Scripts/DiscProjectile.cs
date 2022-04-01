// DiscProjectile
using UnityEngine;

public class DiscProjectile : MonoBehaviour
{
	public Character projectileOwner;

	public int damage;

	public float x_Speed;

	public float knockback;

	public float stunTime;

	public float flightTime;

	private void Update()
	{
		MoveProjectile();
	}

	private void MoveProjectile()
	{
		if (flightTime > 0f)
		{
			base.transform.position = new Vector3(base.transform.position.x + x_Speed * (flightTime + 0.25f) * Time.deltaTime, base.transform.position.y + (flightTime - 0.67f) * 4f * Time.deltaTime, 0f);
		}
		else
		{
			GetComponent<Animator>().SetBool("Falling", value: true);
			GetComponentInChildren<TrailRenderer>().enabled = false;
			damage = 2;
			knockback = 0f;
			stunTime = 0.1f;
			base.transform.position = new Vector3(base.transform.position.x + x_Speed * 0.25f * Time.deltaTime, base.transform.position.y - 5f * Time.deltaTime, 0f);
		}
		if (base.transform.position.y < -3.78f)
		{
			Object.Destroy(base.gameObject);
		}
		flightTime -= Time.deltaTime;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (((bool)collision.GetComponent<Character>() && collision.GetComponent<Character>() != projectileOwner) || (collision.tag == "ShieldCollider" && collision.transform.parent.parent.parent.GetComponent<Character>() != projectileOwner))
		{
			Character character = null;
			character = ((!(collision.tag == "ShieldCollider")) ? collision.GetComponent<Character>() : collision.transform.parent.parent.parent.GetComponent<Character>());
			character.TakeDamage(damage, knockback, stunTime);
			Object.FindObjectOfType<AudioManager>().Play("Hit2");
			Object.Destroy(base.gameObject);
		}
	}
}
