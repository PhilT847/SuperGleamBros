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
