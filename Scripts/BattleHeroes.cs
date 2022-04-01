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
