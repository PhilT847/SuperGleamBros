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
