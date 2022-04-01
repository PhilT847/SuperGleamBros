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
