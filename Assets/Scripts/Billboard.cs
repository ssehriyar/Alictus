using UnityEngine;

public class Billboard : MonoBehaviour
{
	private Camera _cam;

	private void Start()
	{
		_cam = Camera.main;
	}

	private void Update()
	{
		transform.rotation = Quaternion.LookRotation(transform.position - _cam.transform.position);
	}
}
