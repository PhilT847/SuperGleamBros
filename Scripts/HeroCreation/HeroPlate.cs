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
