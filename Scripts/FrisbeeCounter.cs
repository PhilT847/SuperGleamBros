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
