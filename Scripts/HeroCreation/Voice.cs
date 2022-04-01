// Voice
using UnityEngine;

public class Voice : MonoBehaviour
{
	public string[] tauntSounds;

	public string[] attackSounds;

	public string[] specialSounds;

	public string[] hurtSounds;

	public float noSoundsTimer;

	public void PlayTauntSound()
	{
		if (tauntSounds.Length != 0)
		{
			Object.FindObjectOfType<AudioManager>().Play(tauntSounds[Random.Range(0, tauntSounds.Length)]);
			noSoundsTimer = 1.5f;
		}
	}

	public void PlayAttackSound()
	{
		if (noSoundsTimer == 0f && attackSounds.Length != 0 && Random.Range(0, 10) < 6)
		{
			Object.FindObjectOfType<AudioManager>().Play(attackSounds[Random.Range(0, attackSounds.Length)]);
			noSoundsTimer = 1.5f;
		}
	}

	public void PlaySpecialSound()
	{
		if (noSoundsTimer == 0f && specialSounds.Length != 0 && Random.Range(0, 10) < 6)
		{
			Object.FindObjectOfType<AudioManager>().Play(specialSounds[Random.Range(0, specialSounds.Length)]);
			noSoundsTimer = 1.5f;
		}
	}

	public void PlayHurtSound()
	{
		if (noSoundsTimer == 0f && hurtSounds.Length != 0 && Random.Range(0, 10) < 8)
		{
			Object.FindObjectOfType<AudioManager>().Play(hurtSounds[Random.Range(0, hurtSounds.Length)]);
			noSoundsTimer = 1.5f;
		}
	}

	private void Update()
	{
		if (noSoundsTimer > 0f)
		{
			noSoundsTimer -= Time.deltaTime;
		}
		else
		{
			noSoundsTimer = 0f;
		}
	}
}
