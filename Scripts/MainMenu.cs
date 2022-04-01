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
