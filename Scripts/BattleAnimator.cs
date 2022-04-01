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
