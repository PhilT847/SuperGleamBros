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
