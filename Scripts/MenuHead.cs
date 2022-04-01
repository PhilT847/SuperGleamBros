// MenuHead
using UnityEngine;

public class MenuHead : MonoBehaviour
{
	private float currentSpeed;

	private void Start()
	{
		Object.Destroy(base.gameObject, 3f);
		currentSpeed = -3f;
	}

	private void Update()
	{
		base.transform.position = new Vector3(base.transform.position.x, base.transform.position.y + currentSpeed * Time.deltaTime, 0f);
		currentSpeed -= 2f * Time.deltaTime;
	}
}
