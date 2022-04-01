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
