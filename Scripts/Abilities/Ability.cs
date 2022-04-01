// Ability
using UnityEngine;

public abstract class Ability : MonoBehaviour
{
	public Character abilityOwner;

	public string abilityName;

	public string abilitySound;

	public float abilitySpeed;

	public GameObject effectParticles;

	public abstract void UseAbility();

	public abstract void AbilityUpdate();

	public void GenerateEffectParticles(Transform target)
	{
		if (effectParticles != null)
		{
			GameObject gameObject = Object.Instantiate(effectParticles, target);
			if ((bool)target.transform.GetComponent<Character>())
			{
				gameObject.transform.position = new Vector2(target.position.x, target.position.y + 1.5f);
			}
			gameObject.GetComponentInChildren<ParticleSystem>().Clear();
			gameObject.GetComponentInChildren<ParticleSystem>().Play();
			Object.Destroy(gameObject.gameObject, 1f);
		}
	}
}

// AudioManager
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
	public Sound currentMusic;

	public Sound[] sounds;

	public static AudioManager instance;

	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
			UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
			Sound[] array = sounds;
			foreach (Sound sound in array)
			{
				sound.source = base.gameObject.AddComponent<AudioSource>();
				sound.source.clip = sound.clip;
				sound.source.volume = sound.volume;
				sound.source.pitch = sound.pitch;
				sound.source.loop = sound.looping;
			}
		}
		else
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	public void Play(string name)
	{
		Sound sound2 = Array.Find(sounds, (Sound sound) => sound.name == name);
		sound2?.source.PlayOneShot(sound2.clip);
	}

	public void SetMusic(string name)
	{
		Sound sound2 = Array.Find(sounds, (Sound sound) => sound.name == name);
		if (sound2 != null)
		{
			if (currentMusic.name != "")
			{
				currentMusic.source.Stop();
			}
			currentMusic = sound2;
			currentMusic.source.Play();
		}
	}
}

// BattleAnimator
using TMPro;
using UnityEngine;

public class BattleAnimator : MonoBehaviour
{
	public Animator battleAnim;

	public Body firstBody;

	public Body secondBody;

	public TextMeshProUGUI winnerName;

	public void SetAnimBodies(Blueprint firstPlayer, Blueprint secondPlayer)
	{
		firstBody.headSprite.sprite = firstPlayer.chosenHead;
		firstBody.torsoSprite.sprite = firstPlayer.chosenTorso;
		firstBody.armFSprite.sprite = firstPlayer.chosenArm_F;
		firstBody.armBSprite.sprite = firstPlayer.chosenArm_B;
		firstBody.legFSprite.sprite = firstPlayer.chosenLeg_F;
		firstBody.legBSprite.sprite = firstPlayer.chosenLeg_B;
		firstBody.headSprite.color = firstPlayer.chosenHeadColor;
		firstBody.torsoSprite.color = firstPlayer.chosenTorsoColor;
		firstBody.armFSprite.color = firstPlayer.chosenTorsoColor;
		firstBody.armBSprite.color = firstPlayer.chosenTorsoColor;
		firstBody.legFSprite.color = firstPlayer.chosenLegColor;
		firstBody.legBSprite.color = firstPlayer.chosenLegColor;
		secondBody.headSprite.sprite = secondPlayer.chosenHead;
		secondBody.torsoSprite.sprite = secondPlayer.chosenTorso;
		secondBody.armFSprite.sprite = secondPlayer.chosenArm_F;
		secondBody.armBSprite.sprite = secondPlayer.chosenArm_B;
		secondBody.legFSprite.sprite = secondPlayer.chosenLeg_F;
		secondBody.legBSprite.sprite = secondPlayer.chosenLeg_B;
		secondBody.headSprite.color = secondPlayer.chosenHeadColor;
		secondBody.torsoSprite.color = secondPlayer.chosenTorsoColor;
		secondBody.armFSprite.color = secondPlayer.chosenTorsoColor;
		secondBody.armBSprite.color = secondPlayer.chosenTorsoColor;
		secondBody.legFSprite.color = secondPlayer.chosenLegColor;
		secondBody.legBSprite.color = secondPlayer.chosenLegColor;
	}

	public void VictoryAnimation(Character winner)
	{
		battleAnim.SetTrigger("Victory");
		winnerName.SetText(winner.characterName + "!!!");
	}
}

// BattleHeroes
using UnityEngine;

public class BattleHeroes : MonoBehaviour
{
	public Blueprint fighter1;

	public Blueprint fighter2;

	public static BattleHeroes instance;

	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
			Object.DontDestroyOnLoad(base.gameObject);
		}
		else
		{
			Object.Destroy(base.gameObject);
		}
	}
}

// Blueprint
using UnityEngine;

public class Blueprint : MonoBehaviour
{
	public string fighterName;

	public Voice characterVoice;

	public Sprite chosenHead;

	public Sprite chosenTorso;

	public Sprite chosenArm_F;

	public Sprite chosenArm_B;

	public Sprite chosenLeg_F;

	public Sprite chosenLeg_B;

	public Color chosenHeadColor;

	public Color chosenTorsoColor;

	public Color chosenLegColor;

	public string fightingStyle;

	public Ability chosenSpecialMove;

	public string fighterCode;
}

// Body
using UnityEngine;

public class Body : MonoBehaviour
{
	public SpriteRenderer headSprite;

	public SpriteRenderer torsoSprite;

	public SpriteRenderer armFSprite;

	public SpriteRenderer armBSprite;

	public SpriteRenderer legFSprite;

	public SpriteRenderer legBSprite;
}

// BuilderGuide
using TMPro;
using UnityEngine;

public class BuilderGuide : MonoBehaviour
{
	public string[] infoTitles;

	public TextMeshProUGUI titleText;

	public TextMeshProUGUI infoText;

	public void SetInfo(int thisIndex)
	{
		titleText.SetText(infoTitles[thisIndex]);
		switch (thisIndex)
		{
		case 0:
		{
			infoText.fontSize = 40f;
			string text3 = "";
			text3 += "The Editor allows you to customize a figher's appearance, combat style, and special move.\n\n";
			text3 += "Click on a hero icon to edit a fighter or create a new one.";
			infoText.SetText(text3);
			break;
		}
		case 1:
		{
			infoText.fontSize = 40f;
			string text2 = "";
			text2 += "There are three combat styles, each with their own strengths and weaknesses.\n\n";
			text2 += "Normal fighters are well-rounded. Hold the Attack button to perform a combo attack.\n\n";
			text2 += "Fat fighters have high health. Hold the Attack button to charge a devastating belly bump.\n\n";
			text2 += "Speedy fighters are nimble, yet frail. Hold the Attack button to continuously poke at an enemy.";
			infoText.SetText(text2);
			break;
		}
		case 2:
		{
			infoText.fontSize = 36f;
			string text = "";
			text += "Headshot: Press the Special key to shoot your head out at high speed.\n\n";
			text += "Come Here!: Press the Special key to shoot a hand that pulls your opponent to you, even if they're blocking.\n\n";
			text += "Matercide: Press the Special key while on the ground to call Mater for help.\n\n";
			text += "Dracflip: Press the Special key to quickly dodge. You can choose the direction. \n\n";
			text += "Gleam Beam: Hold the Special key to fire a continuous beam.\n\n";
			text += "Disc Golf: Press and hold the Special key to toss a disc. Fighters carry a limited number of discs that regenerates over time.";
			infoText.SetText(text);
			break;
		}
		}
	}
}

// Character
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public abstract class Character : MonoBehaviour
{
	public string characterName;

	public int maxHP;

	public int currentHP;

	public float speedMult;

	public float jumpHeight;

	public bool isGrounded;

	public Vector2 moveVector;

	private float sprintMult;

	public float stunTime;

	public bool canAct;

	public bool invulnerable;

	public bool hasSuperArmor;

	public Body characterBody;

	public Animator characterAnim;

	public Transform characterShadow;

	public Rigidbody2D rigidBody;

	public bool affectedByGravity;

	public bool beingPushed;

	public Hurtbox hurtBox;

	public bool isBlocking;

	public float shieldPercentage;

	public Slider shieldSlider;

	public Transform grabber;

	private Character grabbedCharacter;

	public Ability specialAbility;

	public HealthBar associatedHealthBar;

	public Voice characterVoice;

	public Transform flippedBody;

	[HideInInspector]
	public bool facingRight;

	public KeyCode rightKey;

	public KeyCode leftKey;

	public KeyCode upKey;

	public KeyCode downKey;

	public KeyCode attackKey;

	public KeyCode specialKey;

	public Character opponent;

	public int crowns;

	public abstract void TakeDamage(int damage, float knockbackValue, float stunTime);

	public abstract void ControlHero();

	public abstract void GroundAttack();

	public abstract void AirAttack();

	public abstract void SpecialMove();

	public void KillUnit()
	{
		Object.FindObjectOfType<GameController>().StartCoroutine(Object.FindObjectOfType<GameController>().WinRound(opponent, this));
		currentHP = 0;
	}

	public void FlipUnit()
	{
		facingRight = !facingRight;
		flippedBody.localScale = new Vector3(flippedBody.localScale.x * -1f, flippedBody.localScale.y, 1f);
		shieldSlider.GetComponent<RectTransform>().localScale = new Vector3(shieldSlider.GetComponent<RectTransform>().localScale.x * -1f, 1f, 1f);
	}

	public void MoveHero()
	{
		if (base.transform.position.x > 8f)
		{
			base.transform.position = new Vector2(8f, base.transform.position.y);
		}
		else if (base.transform.position.x < -8f)
		{
			base.transform.position = new Vector2(-8f, base.transform.position.y);
		}
		if (stunTime > 0f)
		{
			stunTime -= Time.deltaTime;
		}
		else
		{
			stunTime = 0f;
		}
		if (!isGrounded && base.transform.position.y < -3.78f)
		{
			base.transform.position = new Vector3(base.transform.position.x, -3.78f, 0f);
			isGrounded = true;
			characterAnim.SetTrigger("Land");
			moveVector.y = 0f;
		}
		ApplyGravity();
		if (isGrounded)
		{
			if ((Input.GetKey(rightKey) || Input.GetKey(leftKey)) && sprintMult < 1.75f)
			{
				sprintMult += 1.5f * Time.deltaTime;
			}
			else if (sprintMult > 1f)
			{
				sprintMult -= 4f * Time.deltaTime;
			}
			else
			{
				sprintMult = 1f;
			}
		}
		else
		{
			sprintMult = 1.75f;
		}
		if ((!Input.GetKey(rightKey) && !Input.GetKey(leftKey)) || stunTime > 0f || !canAct)
		{
			if (Mathf.Abs(rigidBody.velocity.x) > 0.05f)
			{
				sprintMult = 1f;
				if (rigidBody.velocity.x > 0f)
				{
					moveVector = new Vector2(rigidBody.velocity.x - 20f * Time.deltaTime, moveVector.y);
				}
				else
				{
					moveVector = new Vector2(rigidBody.velocity.x + 20f * Time.deltaTime, moveVector.y);
				}
			}
			else
			{
				moveVector.x = 0f;
			}
		}
		moveVector = new Vector2(moveVector.x * sprintMult, moveVector.y);
		rigidBody.velocity = moveVector;
		if (canAct && ((opponent.transform.position.x > base.transform.position.x && !facingRight) || (opponent.transform.position.x < base.transform.position.x && facingRight)))
		{
			FlipUnit();
		}
		characterShadow.position = new Vector3(base.transform.position.x, -3.8f, 0f);
		float num = 1f - (base.transform.position.y + 3.78f) * 0.1f;
		if (num < 0.33f)
		{
			num = 0.33f;
		}
		else if (num > 1f)
		{
			num = 1f;
		}
		characterShadow.localScale = new Vector3(2f * num, 0.5f * num, 1f);
		if (Input.GetKey(downKey) && isGrounded && canAct && stunTime == 0f)
		{
			rigidBody.velocity = Vector2.zero;
			characterAnim.SetBool("Running", value: false);
			Block();
		}
		else
		{
			isBlocking = false;
			characterAnim.SetBool("Blocking", value: false);
			shieldSlider.gameObject.SetActive(value: false);
			if (shieldPercentage < 1f)
			{
				shieldPercentage += 0.1f * Time.deltaTime;
			}
		}
		characterAnim.SetBool("Grounded", isGrounded);
		characterAnim.SetFloat("StunTime", stunTime);
		if (specialAbility != null)
		{
			specialAbility.AbilityUpdate();
		}
	}

	public IEnumerator ApplyPush(float pushValue, float time)
	{
		while (time > 0f)
		{
			time -= Time.deltaTime;
			rigidBody.velocity = new Vector2(pushValue, rigidBody.velocity.y);
			if (base.transform.position.x > 7.98f && pushValue > 0f)
			{
				pushValue *= -1f;
				base.transform.position = new Vector3(7.97f, base.transform.position.y, 0f);
			}
			else if (base.transform.position.x < -7.98f && pushValue < 0f)
			{
				pushValue *= -1f;
				base.transform.position = new Vector3(-7.97f, base.transform.position.y, 0f);
			}
			yield return new WaitForEndOfFrame();
		}
		moveVector.x = 0f;
		yield return null;
	}

	public void ApplyGravity()
	{
		if (isGrounded || !affectedByGravity)
		{
			return;
		}
		float num = 1f;
		if (moveVector.y > 0f && stunTime == 0f)
		{
			if (base.transform.position.y < jumpHeight - 2f)
			{
				num = 1f;
			}
			else if (base.transform.position.y < jumpHeight)
			{
				num = 0.1f;
			}
			else if (base.transform.position.y > jumpHeight)
			{
				num = 2f;
			}
		}
		else if (base.transform.position.y < jumpHeight - 1f)
		{
			num = 3f;
		}
		moveVector = new Vector2(moveVector.x, moveVector.y -= 15f * Time.deltaTime * num);
	}

	public void Block()
	{
		shieldSlider.value = shieldPercentage;
		shieldSlider.gameObject.SetActive(value: true);
		if (shieldPercentage > 0f)
		{
			characterAnim.SetBool("Blocking", value: true);
			shieldPercentage -= 0.2f * Time.deltaTime;
			isBlocking = true;
		}
		else
		{
			isBlocking = false;
			characterAnim.SetBool("Blocking", value: false);
			TakeDamage(0, 0f, 5f);
			rigidBody.velocity = Vector2.zero;
			Object.FindObjectOfType<AudioManager>().Play("Shatter");
		}
	}
}

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

// FrisbeeCounter
using UnityEngine;
using UnityEngine.UI;

public class FrisbeeCounter : MonoBehaviour
{
	public FrisbeeToss inventory;

	public Image[] frisbeeImages;

	public void UpdateFrisbeeCounter()
	{
		switch (inventory.queuedDiscs.Count)
		{
		case 0:
			frisbeeImages[0].color = new Color32(byte.MaxValue, 200, byte.MaxValue, 100);
			frisbeeImages[1].color = new Color32(byte.MaxValue, 200, byte.MaxValue, 100);
			frisbeeImages[2].color = new Color32(byte.MaxValue, 200, byte.MaxValue, 100);
			break;
		case 1:
			frisbeeImages[0].color = inventory.queuedDiscs[0];
			frisbeeImages[1].color = new Color32(byte.MaxValue, 200, byte.MaxValue, 100);
			frisbeeImages[2].color = new Color32(byte.MaxValue, 200, byte.MaxValue, 100);
			break;
		case 2:
			frisbeeImages[0].color = inventory.queuedDiscs[0];
			frisbeeImages[1].color = inventory.queuedDiscs[1];
			frisbeeImages[2].color = new Color32(byte.MaxValue, 200, byte.MaxValue, 100);
			break;
		case 3:
			frisbeeImages[0].color = inventory.queuedDiscs[0];
			frisbeeImages[1].color = inventory.queuedDiscs[1];
			frisbeeImages[2].color = inventory.queuedDiscs[2];
			break;
		}
	}
}

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

// GameController
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
	public GameObject normalFighterObject;

	public GameObject fatFighterObject;

	public GameObject swiftFighterObject;

	public GameObject shadowObject;

	public Blueprint DEBUG_1;

	public Blueprint DEBUG_2;

	public Character firstCharacter;

	public Character secondCharacter;

	public HealthBar firstHealthBar;

	public HealthBar secondHealthBar;

	public BattleAnimator battleAnimator;

	private void Start()
	{
		StartCoroutine(SetupMatch(Object.FindObjectOfType<BattleHeroes>().fighter1, Object.FindObjectOfType<BattleHeroes>().fighter2));
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Equals))
		{
			SceneManager.LoadSceneAsync(0);
		}
	}

	public IEnumerator SetupMatch(Blueprint playerOne, Blueprint playerTwo)
	{
		Object.FindObjectOfType<AudioManager>().SetMusic("MusicBattle");
		InitializeCharacter(playerOne, firstPlayer: true);
		InitializeCharacter(playerTwo, firstPlayer: false);
		firstCharacter.opponent = secondCharacter;
		secondCharacter.opponent = firstCharacter;
		firstCharacter.canAct = false;
		secondCharacter.canAct = false;
		firstCharacter.invulnerable = true;
		secondCharacter.invulnerable = true;
		battleAnimator.SetAnimBodies(playerOne, playerTwo);
		yield return new WaitForSeconds(1f);
		battleAnimator.battleAnim.SetTrigger("Setup");
		yield return new WaitForSeconds(1.5f);
		firstCharacter.characterVoice.PlayTauntSound();
		yield return new WaitForSeconds(3f);
		secondCharacter.characterVoice.PlayTauntSound();
		yield return new WaitForSeconds(3f);
		StartCoroutine(BeginRound());
		yield return null;
	}

	public IEnumerator BeginRound()
	{
		SetStartingPositions();
		firstCharacter.currentHP = 1;
		secondCharacter.currentHP = 1;
		battleAnimator.battleAnim.SetTrigger("Countdown");
		float currentHP_Percentage = 0f;
		while (firstCharacter.currentHP < firstCharacter.maxHP)
		{
			currentHP_Percentage += 0.01f;
			firstCharacter.currentHP = (int)((float)firstCharacter.maxHP * currentHP_Percentage);
			firstCharacter.associatedHealthBar.UpdateHealthBar();
			secondCharacter.currentHP = (int)((float)secondCharacter.maxHP * currentHP_Percentage);
			secondCharacter.associatedHealthBar.UpdateHealthBar();
			yield return new WaitForSeconds(0.01f);
		}
		Object.FindObjectOfType<AudioManager>().Play("Hit3");
		firstCharacter.currentHP = firstCharacter.maxHP;
		secondCharacter.currentHP = secondCharacter.maxHP;
		firstCharacter.canAct = true;
		secondCharacter.canAct = true;
		firstCharacter.invulnerable = false;
		secondCharacter.invulnerable = false;
		yield return null;
	}

	public IEnumerator WinRound(Character winner, Character loser)
	{
		winner.invulnerable = true;
		loser.invulnerable = true;
		Object.FindObjectOfType<AudioManager>().Play("Cheer");
		if (winner == firstCharacter)
		{
			battleAnimator.battleAnim.SetTrigger("Player1_Win");
		}
		else
		{
			battleAnimator.battleAnim.SetTrigger("Player2_Win");
		}
		yield return new WaitForSeconds(1.5f);
		Object.FindObjectOfType<AudioManager>().Play("Slash");
		yield return new WaitForSeconds(1.5f);
		winner.crowns++;
		winner.associatedHealthBar.AddCrown();
		if (winner.crowns == 2)
		{
			battleAnimator.VictoryAnimation(winner);
			yield return new WaitForSeconds(2.75f);
			Object.FindObjectOfType<AudioManager>().SetMusic("MusicMenu");
			SceneManager.LoadScene(0);
		}
		else
		{
			StartCoroutine(BeginRound());
		}
		yield return null;
	}

	private void SetStartingPositions()
	{
		firstCharacter.transform.position = new Vector3(-5f, -3.78f, 0f);
		secondCharacter.transform.position = new Vector3(5f, -3.78f, 0f);
		firstCharacter.invulnerable = false;
		secondCharacter.invulnerable = false;
	}

	public IEnumerator AddCharactersToMap(Blueprint playerOne, Blueprint playerTwo)
	{
		InitializeCharacter(playerOne, firstPlayer: true);
		InitializeCharacter(playerTwo, firstPlayer: false);
		firstCharacter.opponent = secondCharacter;
		secondCharacter.opponent = firstCharacter;
		yield return new WaitForSeconds(0.5f);
		float currentHP_Percentage = 0f;
		while (firstCharacter.currentHP < firstCharacter.maxHP)
		{
			currentHP_Percentage += 0.01f;
			firstCharacter.currentHP = (int)((float)firstCharacter.maxHP * currentHP_Percentage);
			firstCharacter.associatedHealthBar.UpdateHealthBar();
			secondCharacter.currentHP = (int)((float)secondCharacter.maxHP * currentHP_Percentage);
			secondCharacter.associatedHealthBar.UpdateHealthBar();
			yield return new WaitForSeconds(0.01f);
		}
		firstCharacter.currentHP = firstCharacter.maxHP;
		firstCharacter.associatedHealthBar.UpdateHealthBar();
		secondCharacter.currentHP = secondCharacter.maxHP;
		secondCharacter.associatedHealthBar.UpdateHealthBar();
		firstCharacter.canAct = true;
		secondCharacter.canAct = true;
		yield return null;
	}

	public void InitializeCharacter(Blueprint fighterDesign, bool firstPlayer)
	{
		GameObject gameObject = null;
		string fightingStyle = fighterDesign.fightingStyle;
		gameObject = ((fightingStyle == "Fat") ? fatFighterObject : ((!(fightingStyle == "Speedy")) ? normalFighterObject : swiftFighterObject));
		Character component = Object.Instantiate(gameObject, new Vector3(0f, 0f, 0f), Quaternion.identity).GetComponent<Character>();
		component.characterName = fighterDesign.fighterName;
		GameObject gameObject2 = Object.Instantiate(fighterDesign.characterVoice.gameObject, Vector3.zero, Quaternion.identity, component.transform);
		component.characterVoice = gameObject2.GetComponentInChildren<Voice>();
		component.characterBody.headSprite.sprite = fighterDesign.chosenHead;
		component.characterBody.torsoSprite.sprite = fighterDesign.chosenTorso;
		component.characterBody.armFSprite.sprite = fighterDesign.chosenArm_F;
		component.characterBody.armBSprite.sprite = fighterDesign.chosenArm_B;
		component.characterBody.legFSprite.sprite = fighterDesign.chosenLeg_F;
		component.characterBody.legBSprite.sprite = fighterDesign.chosenLeg_B;
		component.characterBody.headSprite.color = fighterDesign.chosenHeadColor;
		component.characterBody.torsoSprite.color = fighterDesign.chosenTorsoColor;
		component.characterBody.armFSprite.color = fighterDesign.chosenTorsoColor;
		component.characterBody.armBSprite.color = fighterDesign.chosenTorsoColor;
		component.characterBody.legFSprite.color = fighterDesign.chosenLegColor;
		component.characterBody.legBSprite.color = fighterDesign.chosenLegColor;
		(component.specialAbility = Object.Instantiate(fighterDesign.chosenSpecialMove, Vector3.zero, Quaternion.identity, component.transform)).abilityOwner = component;
		component.currentHP = 1;
		GameObject gameObject3 = Object.Instantiate(shadowObject, Vector3.zero, Quaternion.identity);
		component.characterShadow = gameObject3.transform;
		gameObject3.transform.position = new Vector3(component.transform.position.x, -3.8f, 0f);
		if (firstPlayer)
		{
			component.associatedHealthBar = firstHealthBar;
			firstHealthBar.InitializeHealthBar(component);
			component.transform.position = new Vector3(-5f, -3.7f, 0f);
			component.characterBody.headSprite.sortingOrder += 20;
			component.characterBody.torsoSprite.sortingOrder += 20;
			component.characterBody.armFSprite.sortingOrder += 20;
			component.characterBody.armBSprite.sortingOrder += 20;
			component.characterBody.legFSprite.sortingOrder += 20;
			component.characterBody.legBSprite.sortingOrder += 20;
			firstCharacter = component;
			SetKeys(component, firstPlayer: true);
			component.FlipUnit();
		}
		else
		{
			component.associatedHealthBar = secondHealthBar;
			secondHealthBar.InitializeHealthBar(component);
			component.transform.position = new Vector3(5f, -3.7f, 0f);
			SetKeys(component, firstPlayer: false);
			secondCharacter = component;
		}
		component.speedMult = 2.5f;
		component.jumpHeight = -1.5f;
		component.maxHP = 200;
		if ((bool)component.GetComponent<FatHero>())
		{
			component.maxHP += 40;
			component.speedMult -= 0.75f;
			component.jumpHeight -= 1f;
		}
		else if ((bool)component.GetComponent<SwiftHero>())
		{
			component.maxHP -= 40;
			component.speedMult += 0.5f;
			component.jumpHeight += 0.1f;
		}
	}

	private void SetKeys(Character c, bool firstPlayer)
	{
		if (firstPlayer)
		{
			c.rightKey = KeyCode.D;
			c.leftKey = KeyCode.A;
			c.upKey = KeyCode.W;
			c.downKey = KeyCode.S;
			c.attackKey = KeyCode.C;
			c.specialKey = KeyCode.V;
		}
		else
		{
			c.rightKey = KeyCode.RightArrow;
			c.leftKey = KeyCode.LeftArrow;
			c.upKey = KeyCode.UpArrow;
			c.downKey = KeyCode.DownArrow;
			c.attackKey = KeyCode.O;
			c.specialKey = KeyCode.P;
		}
	}
}

// HeadSpawner
using UnityEngine;

public class HeadSpawner : MonoBehaviour
{
	public GameObject headPrefab;

	public Sprite[] allSprites;

	private float waitForSpawnTimer;

	private void Update()
	{
		if (waitForSpawnTimer > 0f)
		{
			waitForSpawnTimer -= Time.deltaTime;
			return;
		}
		waitForSpawnTimer = Random.Range(0.25f, 0.5f);
		Object.Instantiate(headPrefab, new Vector3(Random.Range(-7.5f, 7.5f), 7f, 0f), Quaternion.identity).GetComponentInChildren<SpriteRenderer>().sprite = allSprites[Random.Range(0, allSprites.Length)];
	}
}

// HealthBar
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
	private Character owner;

	public Slider healthSlider;

	public Image[] crowns;

	public FrisbeeCounter frisbeeCounter;

	private void Start()
	{
		crowns[0].color = Color.clear;
		crowns[1].color = Color.clear;
	}

	public void InitializeHealthBar(Character newOwner)
	{
		owner = newOwner;
		if (owner.specialAbility.abilityName == "Disc Golf")
		{
			frisbeeCounter.gameObject.SetActive(value: true);
			frisbeeCounter.inventory = owner.specialAbility.GetComponent<FrisbeeToss>();
		}
		else
		{
			frisbeeCounter.gameObject.SetActive(value: false);
		}
		GetComponentInChildren<TextMeshProUGUI>().SetText(owner.characterName);
		UpdateHealthBar();
	}

	public void UpdateHealthBar()
	{
		float value = (float)owner.currentHP / (float)owner.maxHP;
		healthSlider.value = value;
		if (healthSlider.value > 0.66f)
		{
			healthSlider.fillRect.GetComponentInChildren<Image>().color = new Color32(50, 230, 50, byte.MaxValue);
		}
		else if (healthSlider.value > 0.33f)
		{
			healthSlider.fillRect.GetComponentInChildren<Image>().color = new Color32(byte.MaxValue, 225, 0, byte.MaxValue);
		}
		else
		{
			healthSlider.fillRect.GetComponentInChildren<Image>().color = new Color32(240, 65, 75, byte.MaxValue);
		}
		GetComponent<Animator>().SetTrigger("TakeDamage");
	}

	public void AddCrown()
	{
		crowns[owner.crowns - 1].color = Color.white;
	}
}

// HeroBuilder
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HeroBuilder : MonoBehaviour
{
	public GameObject blueprintObject;

	public Blueprint currentBlueprint;

	public Body displayUnit;

	public string inputtedName;

	public int chosenStyle;

	public int chosenHead;

	public int chosenTorso;

	public int chosenLegs;

	public int headColor;

	public int torsoColor;

	public int legColor;

	public int chosenAbility;

	public Ability[] allSpecialMoves;

	public Color[] allColors;

	public string[] styles;

	public Sprite[] heads;

	public Sprite[] torsos;

	public Sprite[] frontArms;

	public Sprite[] backArms;

	public Sprite[] frontLegs;

	public Sprite[] backLegs;

	public Voice[] voices;

	public TextMeshProUGUI styleText;

	public TextMeshProUGUI specialMoveText;

	public Image[] colorButtons;

	public InputField nameInput;

	public Button saveButton;

	public HeroPlate chosenPlate;

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Alpha0))
		{
			InitializeDisplay();
		}
	}

	public void InitializeDisplay()
	{
		inputtedName = "";
		nameInput.SetTextWithoutNotify(inputtedName);
		chosenHead = Random.Range(0, heads.Length);
		chosenTorso = Random.Range(0, torsos.Length);
		chosenLegs = Random.Range(0, frontLegs.Length);
		chosenStyle = Random.Range(0, styles.Length);
		chosenAbility = Random.Range(0, allSpecialMoves.Length);
		headColor = 0;
		torsoColor = 0;
		legColor = 0;
		for (int i = 0; i < colorButtons.Length; i++)
		{
			colorButtons[i].color = Color.white;
		}
		GameObject gameObject = Object.Instantiate(blueprintObject, Vector3.zero, Quaternion.identity);
		currentBlueprint = gameObject.GetComponent<Blueprint>();
		Object.DontDestroyOnLoad(currentBlueprint.gameObject);
		UpdateBlueprint();
	}

	public void LoadHeroForEditing(Blueprint thisBlueprint)
	{
		currentBlueprint = thisBlueprint;
		inputtedName = currentBlueprint.fighterName;
		nameInput.SetTextWithoutNotify(inputtedName);
		chosenHead = FindSpriteIndex(currentBlueprint.chosenHead, heads);
		chosenTorso = FindSpriteIndex(currentBlueprint.chosenTorso, torsos);
		chosenLegs = FindSpriteIndex(currentBlueprint.chosenLeg_F, frontLegs);
		switch (currentBlueprint.fightingStyle)
		{
		case "Normal":
			chosenStyle = 0;
			break;
		case "Fat":
			chosenStyle = 1;
			break;
		case "Speedy":
			chosenStyle = 2;
			break;
		}
		chosenAbility = FindAbilityIndex(currentBlueprint.chosenSpecialMove, allSpecialMoves);
		headColor = FindColorIndex(currentBlueprint.chosenHeadColor, allColors);
		torsoColor = FindColorIndex(currentBlueprint.chosenTorsoColor, allColors);
		legColor = FindColorIndex(currentBlueprint.chosenLegColor, allColors);
		colorButtons[0].color = allColors[FindColorIndex(currentBlueprint.chosenHeadColor, allColors)];
		colorButtons[1].color = allColors[FindColorIndex(currentBlueprint.chosenTorsoColor, allColors)];
		colorButtons[2].color = allColors[FindColorIndex(currentBlueprint.chosenLegColor, allColors)];
		UpdateBlueprint();
	}

	private int FindSpriteIndex(Sprite findSprite, Sprite[] searchIndex)
	{
		for (int i = 0; i < searchIndex.Length; i++)
		{
			if (searchIndex[i] == findSprite)
			{
				return i;
			}
		}
		return 0;
	}

	private int FindAbilityIndex(Ability findAbility, Ability[] searchIndex)
	{
		for (int i = 0; i < searchIndex.Length; i++)
		{
			if (searchIndex[i] == findAbility)
			{
				return i;
			}
		}
		return 0;
	}

	private int FindColorIndex(Color findColor, Color[] searchIndex)
	{
		for (int i = 0; i < searchIndex.Length; i++)
		{
			if (searchIndex[i] == findColor)
			{
				return i;
			}
		}
		return 0;
	}

	private void RefreshCreatorPage(Blueprint b)
	{
		displayUnit.headSprite.sprite = b.chosenHead;
		displayUnit.torsoSprite.sprite = b.chosenTorso;
		displayUnit.armFSprite.sprite = b.chosenArm_F;
		displayUnit.armBSprite.sprite = b.chosenArm_B;
		displayUnit.legFSprite.sprite = b.chosenLeg_F;
		displayUnit.legBSprite.sprite = b.chosenLeg_B;
		displayUnit.headSprite.color = b.chosenHeadColor;
		displayUnit.torsoSprite.color = b.chosenTorsoColor;
		displayUnit.armFSprite.color = b.chosenTorsoColor;
		displayUnit.armBSprite.color = b.chosenTorsoColor;
		displayUnit.legFSprite.color = b.chosenLegColor;
		displayUnit.legBSprite.color = b.chosenLegColor;
		styleText.SetText(styles[chosenStyle]);
		specialMoveText.SetText(allSpecialMoves[chosenAbility].abilityName);
	}

	public void SelectNextStyle(bool forward)
	{
		if (forward)
		{
			chosenStyle++;
			if (chosenStyle >= styles.Length)
			{
				chosenStyle = 0;
			}
		}
		else
		{
			chosenStyle--;
			if (chosenStyle < 0)
			{
				chosenStyle = styles.Length - 1;
			}
		}
		UpdateBlueprint();
	}

	public void SelectNextMove(bool forward)
	{
		if (forward)
		{
			chosenAbility++;
			if (chosenAbility >= allSpecialMoves.Length)
			{
				chosenAbility = 0;
			}
		}
		else
		{
			chosenAbility--;
			if (chosenAbility < 0)
			{
				chosenAbility = allSpecialMoves.Length - 1;
			}
		}
		UpdateBlueprint();
	}

	public void SelectNextHead(bool forward)
	{
		if (forward)
		{
			chosenHead++;
			if (chosenHead >= heads.Length)
			{
				chosenHead = 0;
			}
		}
		else
		{
			chosenHead--;
			if (chosenHead < 0)
			{
				chosenHead = heads.Length - 1;
			}
		}
		UpdateBlueprint();
	}

	public void SelectNextTorso(bool forward)
	{
		if (forward)
		{
			chosenTorso++;
			if (chosenTorso >= torsos.Length)
			{
				chosenTorso = 0;
			}
		}
		else
		{
			chosenTorso--;
			if (chosenTorso < 0)
			{
				chosenTorso = torsos.Length - 1;
			}
		}
		UpdateBlueprint();
	}

	public void SelectNextLegs(bool forward)
	{
		if (forward)
		{
			chosenLegs++;
			if (chosenLegs >= frontLegs.Length)
			{
				chosenLegs = 0;
			}
		}
		else
		{
			chosenLegs--;
			if (chosenLegs < 0)
			{
				chosenLegs = frontLegs.Length - 1;
			}
		}
		UpdateBlueprint();
	}

	public void SelectNextColor(int chosenPart)
	{
		int num = 0;
		switch (chosenPart)
		{
		case 0:
			headColor++;
			if (headColor >= allColors.Length)
			{
				headColor = 0;
			}
			num = headColor;
			break;
		case 1:
			torsoColor++;
			if (torsoColor >= allColors.Length)
			{
				torsoColor = 0;
			}
			num = torsoColor;
			break;
		case 2:
			legColor++;
			if (legColor >= allColors.Length)
			{
				legColor = 0;
			}
			num = legColor;
			break;
		}
		colorButtons[chosenPart].color = allColors[num];
		UpdateBlueprint();
	}

	public void UpdateName()
	{
		inputtedName = nameInput.text;
		saveButton.interactable = inputtedName != "";
		UpdateBlueprint();
	}

	public void SaveCharacter()
	{
		if (inputtedName != "")
		{
			chosenPlate.heldCharacter = currentBlueprint;
			chosenPlate.UpdateHeroPlate();
			currentBlueprint.gameObject.name = inputtedName + "_BP";
			currentBlueprint.fighterCode = CreateCode();
			Object.FindObjectOfType<MainMenu>(includeInactive: true).SaveCodes();
			base.gameObject.SetActive(value: false);
		}
	}

	private void UpdateBlueprint()
	{
		currentBlueprint.fighterName = inputtedName;
		currentBlueprint.fightingStyle = styles[chosenStyle];
		currentBlueprint.chosenSpecialMove = allSpecialMoves[chosenAbility];
		currentBlueprint.chosenHead = heads[chosenHead];
		currentBlueprint.characterVoice = voices[chosenHead];
		currentBlueprint.chosenTorso = torsos[chosenTorso];
		currentBlueprint.chosenArm_F = frontArms[chosenTorso];
		currentBlueprint.chosenArm_B = backArms[chosenTorso];
		currentBlueprint.chosenLeg_F = frontLegs[chosenLegs];
		currentBlueprint.chosenLeg_B = backLegs[chosenLegs];
		currentBlueprint.chosenHeadColor = allColors[headColor];
		currentBlueprint.chosenTorsoColor = allColors[torsoColor];
		currentBlueprint.chosenLegColor = allColors[legColor];
		RefreshCreatorPage(currentBlueprint);
	}

	private string CreateCode()
	{
		string text = "";
		for (int i = 0; i < 12; i++)
		{
			text = ((i >= inputtedName.Length) ? (text + "0") : (text + inputtedName[i]));
		}
		switch (currentBlueprint.fightingStyle)
		{
		case "Normal":
			text += "N";
			break;
		case "Fat":
			text += "F";
			break;
		case "Speedy":
			text += "S";
			break;
		}
		text += headColor;
		text += torsoColor;
		text += legColor;
		text = ((chosenHead >= 10) ? (text + chosenHead) : (text + "0" + chosenHead));
		text = ((chosenTorso >= 10) ? (text + chosenTorso) : (text + "0" + chosenTorso));
		text = ((chosenLegs >= 10) ? (text + chosenLegs) : (text + "0" + chosenLegs));
		if (chosenAbility < 10)
		{
			return text + "0" + chosenAbility;
		}
		return text + chosenAbility;
	}
}

// HeroPlate
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HeroPlate : MonoBehaviour
{
	public Blueprint heldCharacter;

	public TextMeshProUGUI namePlate;

	public Image characterHead;

	public Sprite emptyImage;

	public void UpdateHeroPlate()
	{
		if (heldCharacter != null)
		{
			namePlate.SetText(heldCharacter.fighterName);
			characterHead.sprite = heldCharacter.chosenHead;
			characterHead.color = heldCharacter.chosenHeadColor;
		}
		else
		{
			EmptyPlate();
		}
	}

	public void EmptyPlate()
	{
		namePlate.SetText("EMPTY");
		characterHead.sprite = emptyImage;
		characterHead.color = Color.white;
	}

	public void EditHero()
	{
		Object.FindObjectOfType<HeroBuilder>(includeInactive: true).gameObject.SetActive(value: true);
		Object.FindObjectOfType<HeroBuilder>(includeInactive: true).chosenPlate = this;
		if (heldCharacter != null)
		{
			Object.FindObjectOfType<HeroBuilder>(includeInactive: true).LoadHeroForEditing(heldCharacter);
		}
		else
		{
			Object.FindObjectOfType<HeroBuilder>(includeInactive: true).InitializeDisplay();
		}
	}
}

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

// MainMenu
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
	public Blueprint[] allFighters;

	public HeroPlate[] allPlates;

	public GameObject emptyBlueprint;

	public Blueprint selectedFighter1;

	public Blueprint selectedFighter2;

	private int fighterIndex1;

	private int fighterIndex2;

	public Body displayBody1;

	public Body displayBody2;

	public TextMeshProUGUI displayName1;

	public TextMeshProUGUI displayName2;

	public Button fightButton;

	private void Start()
	{
		if (PlayerPrefs.HasKey("Codes"))
		{
			LoadAllCodes();
		}
		Object.FindObjectOfType<AudioManager>().SetMusic("MusicMenu");
	}

	private void LoadAllCodes()
	{
		fightButton.interactable = false;
		Blueprint[] array = Object.FindObjectsOfType<Blueprint>();
		for (int i = 0; i < array.Length; i++)
		{
			Object.Destroy(array[i].gameObject);
		}
		string @string = PlayerPrefs.GetString("Codes");
		int num = @string.Length / 22;
		Debug.Log("Loading " + num + " from code " + @string);
		for (int j = 0; j < num; j++)
		{
			int num2 = j * 24;
			Blueprint component = Object.Instantiate(emptyBlueprint, Vector3.zero, Quaternion.identity).GetComponent<Blueprint>();
			string text = "";
			for (int k = num2; k < num2 + 12; k++)
			{
				if (@string[k] != '0')
				{
					text += @string[k];
				}
			}
			component.fighterName = text;
			switch (@string[num2 + 12])
			{
			case 'N':
				component.fightingStyle = "Normal";
				break;
			case 'F':
				component.fightingStyle = "Fat";
				break;
			case 'S':
				component.fightingStyle = "Speedy";
				break;
			}
			component.chosenHeadColor = Object.FindObjectOfType<HeroBuilder>(includeInactive: true).allColors[(int)char.GetNumericValue(@string[num2 + 13])];
			component.chosenTorsoColor = Object.FindObjectOfType<HeroBuilder>(includeInactive: true).allColors[(int)char.GetNumericValue(@string[num2 + 14])];
			component.chosenLegColor = Object.FindObjectOfType<HeroBuilder>(includeInactive: true).allColors[(int)char.GetNumericValue(@string[num2 + 15])];
			if (@string[num2 + 16] == '0')
			{
				component.chosenHead = Object.FindObjectOfType<HeroBuilder>(includeInactive: true).heads[(int)char.GetNumericValue(@string[num2 + 17])];
				component.characterVoice = Object.FindObjectOfType<HeroBuilder>(includeInactive: true).voices[(int)char.GetNumericValue(@string[num2 + 17])];
			}
			else
			{
				component.chosenHead = Object.FindObjectOfType<HeroBuilder>(includeInactive: true).heads[10 + (int)char.GetNumericValue(@string[num2 + 17])];
				component.characterVoice = Object.FindObjectOfType<HeroBuilder>(includeInactive: true).voices[10 + (int)char.GetNumericValue(@string[num2 + 17])];
			}
			if (@string[num2 + 18] == '0')
			{
				component.chosenTorso = Object.FindObjectOfType<HeroBuilder>(includeInactive: true).torsos[(int)char.GetNumericValue(@string[num2 + 19])];
				component.chosenArm_F = Object.FindObjectOfType<HeroBuilder>(includeInactive: true).frontArms[(int)char.GetNumericValue(@string[num2 + 19])];
				component.chosenArm_B = Object.FindObjectOfType<HeroBuilder>(includeInactive: true).backArms[(int)char.GetNumericValue(@string[num2 + 19])];
			}
			else
			{
				component.chosenTorso = Object.FindObjectOfType<HeroBuilder>(includeInactive: true).torsos[10 + (int)char.GetNumericValue(@string[num2 + 19])];
				component.chosenArm_F = Object.FindObjectOfType<HeroBuilder>(includeInactive: true).frontArms[10 + (int)char.GetNumericValue(@string[num2 + 19])];
				component.chosenArm_B = Object.FindObjectOfType<HeroBuilder>(includeInactive: true).backArms[10 + (int)char.GetNumericValue(@string[num2 + 19])];
			}
			if (@string[num2 + 20] == '0')
			{
				component.chosenLeg_F = Object.FindObjectOfType<HeroBuilder>(includeInactive: true).frontLegs[(int)char.GetNumericValue(@string[num2 + 21])];
				component.chosenLeg_B = Object.FindObjectOfType<HeroBuilder>(includeInactive: true).backLegs[(int)char.GetNumericValue(@string[num2 + 21])];
			}
			else
			{
				component.chosenLeg_F = Object.FindObjectOfType<HeroBuilder>(includeInactive: true).frontLegs[10 + (int)char.GetNumericValue(@string[num2 + 21])];
				component.chosenLeg_B = Object.FindObjectOfType<HeroBuilder>(includeInactive: true).backLegs[10 + (int)char.GetNumericValue(@string[num2 + 21])];
			}
			if (@string[num2 + 22] == '0')
			{
				component.chosenSpecialMove = Object.FindObjectOfType<HeroBuilder>(includeInactive: true).allSpecialMoves[(int)char.GetNumericValue(@string[num2 + 23])];
			}
			else
			{
				component.chosenSpecialMove = Object.FindObjectOfType<HeroBuilder>(includeInactive: true).allSpecialMoves[10 + (int)char.GetNumericValue(@string[num2 + 23])];
			}
			string text2 = "";
			for (int l = num2; l < num2 + 24; l++)
			{
				text2 += @string[l];
			}
			component.fighterCode = text2;
			allPlates[j].heldCharacter = component;
			allPlates[j].heldCharacter.gameObject.name = component.fighterName + "_BP";
			allPlates[j].UpdateHeroPlate();
			Object.DontDestroyOnLoad(component.gameObject);
		}
		fightButton.interactable = num > 0;
		PlayerPrefs.Save();
	}

	public void SaveCodes()
	{
		string text = "";
		bool interactable = false;
		for (int i = 0; i < allPlates.Length; i++)
		{
			if (allPlates[i].heldCharacter != null)
			{
				text += allPlates[i].heldCharacter.fighterCode;
				interactable = true;
			}
			fightButton.interactable = interactable;
		}
		PlayerPrefs.SetString("Codes", text);
		PlayerPrefs.Save();
	}

	public void DeleteAllCodes()
	{
		Blueprint[] array = Object.FindObjectsOfType<Blueprint>();
		for (int i = 0; i < array.Length; i++)
		{
			Object.Destroy(array[i].gameObject);
		}
		HeroPlate[] array2 = allPlates;
		foreach (HeroPlate obj in array2)
		{
			obj.heldCharacter = null;
			obj.UpdateHeroPlate();
		}
		fightButton.interactable = false;
		PlayerPrefs.SetString("Codes", "");
		PlayerPrefs.Save();
	}

	public void OpenBattleSetup()
	{
		if (Object.FindObjectsOfType<Blueprint>().Length != 0)
		{
			fighterIndex1 = Random.Range(0, Object.FindObjectsOfType<Blueprint>().Length);
			fighterIndex2 = Random.Range(0, Object.FindObjectsOfType<Blueprint>().Length);
			selectedFighter1 = Object.FindObjectsOfType<Blueprint>()[fighterIndex1];
			selectedFighter2 = Object.FindObjectsOfType<Blueprint>()[fighterIndex2];
			RefreshSetupBodies();
		}
	}

	public void SelectNextFighter(bool isFirstPlayer)
	{
		if (isFirstPlayer)
		{
			fighterIndex1++;
			if (fighterIndex1 > Object.FindObjectsOfType<Blueprint>().Length - 1)
			{
				fighterIndex1 = 0;
			}
			selectedFighter1 = Object.FindObjectsOfType<Blueprint>()[fighterIndex1];
		}
		else
		{
			fighterIndex2++;
			if (fighterIndex2 > Object.FindObjectsOfType<Blueprint>().Length - 1)
			{
				fighterIndex2 = 0;
			}
			selectedFighter2 = Object.FindObjectsOfType<Blueprint>()[fighterIndex2];
		}
		RefreshSetupBodies();
	}

	public void SelectPreviousFighter(bool isFirstPlayer)
	{
		if (isFirstPlayer)
		{
			fighterIndex1--;
			if (fighterIndex1 < 0)
			{
				fighterIndex1 = Object.FindObjectsOfType<Blueprint>().Length - 1;
			}
			selectedFighter1 = Object.FindObjectsOfType<Blueprint>()[fighterIndex1];
		}
		else
		{
			fighterIndex2--;
			if (fighterIndex2 < 0)
			{
				fighterIndex2 = Object.FindObjectsOfType<Blueprint>().Length - 1;
			}
			selectedFighter2 = Object.FindObjectsOfType<Blueprint>()[fighterIndex2];
		}
		RefreshSetupBodies();
	}

	public void RefreshSetupBodies()
	{
		displayBody1.headSprite.sprite = selectedFighter1.chosenHead;
		displayBody1.torsoSprite.sprite = selectedFighter1.chosenTorso;
		displayBody1.armFSprite.sprite = selectedFighter1.chosenArm_F;
		displayBody1.armBSprite.sprite = selectedFighter1.chosenArm_B;
		displayBody1.legFSprite.sprite = selectedFighter1.chosenLeg_F;
		displayBody1.legBSprite.sprite = selectedFighter1.chosenLeg_B;
		displayBody1.headSprite.color = selectedFighter1.chosenHeadColor;
		displayBody1.torsoSprite.color = selectedFighter1.chosenTorsoColor;
		displayBody1.armFSprite.color = selectedFighter1.chosenTorsoColor;
		displayBody1.armBSprite.color = selectedFighter1.chosenTorsoColor;
		displayBody1.legFSprite.color = selectedFighter1.chosenLegColor;
		displayBody1.legBSprite.color = selectedFighter1.chosenLegColor;
		displayBody2.headSprite.sprite = selectedFighter2.chosenHead;
		displayBody2.torsoSprite.sprite = selectedFighter2.chosenTorso;
		displayBody2.armFSprite.sprite = selectedFighter2.chosenArm_F;
		displayBody2.armBSprite.sprite = selectedFighter2.chosenArm_B;
		displayBody2.legFSprite.sprite = selectedFighter2.chosenLeg_F;
		displayBody2.legBSprite.sprite = selectedFighter2.chosenLeg_B;
		displayBody2.headSprite.color = selectedFighter2.chosenHeadColor;
		displayBody2.torsoSprite.color = selectedFighter2.chosenTorsoColor;
		displayBody2.armFSprite.color = selectedFighter2.chosenTorsoColor;
		displayBody2.armBSprite.color = selectedFighter2.chosenTorsoColor;
		displayBody2.legFSprite.color = selectedFighter2.chosenLegColor;
		displayBody2.legBSprite.color = selectedFighter2.chosenLegColor;
		displayName1.SetText(selectedFighter1.fighterName);
		displayName2.SetText(selectedFighter2.fighterName);
	}

	public void BeginBattle()
	{
		StartCoroutine(StartFighting());
	}

	public IEnumerator StartFighting()
	{
		Object.FindObjectOfType<BattleHeroes>().fighter1 = selectedFighter1;
		Object.FindObjectOfType<BattleHeroes>().fighter2 = selectedFighter2;
		yield return new WaitForSeconds(1f);
		SceneManager.LoadSceneAsync(1);
		yield return null;
	}
}

// MenuHead
using UnityEngine;

public class MenuHead : MonoBehaviour
{
	private float currentSpeed;

	private void Start()
	{
		Object.Destroy(base.gameObject, 3f);
		currentSpeed = -3f;
	}

	private void Update()
	{
		base.transform.position = new Vector3(base.transform.position.x, base.transform.position.y + currentSpeed * Time.deltaTime, 0f);
		currentSpeed -= 2f * Time.deltaTime;
	}
}

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

// Sound
using System;
using UnityEngine;

[Serializable]
public class Sound
{
	public string name;

	public AudioClip clip;

	[HideInInspector]
	public AudioSource source;

	[Range(0f, 1f)]
	public float volume;

	[Range(0.1f, 3f)]
	public float pitch;

	public bool looping;
}

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
