using System;
using UnityEngine;

public class AttackRange : MonoBehaviour
{
	public event Action<bool> OnTargetInRange;

	private void OnTriggerEnter(Collider other)
	{
		if (other.transform.root.TryGetComponent(out Player player))
		{
			OnTargetInRange?.Invoke(true);
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.transform.root.TryGetComponent(out Player player))
		{
			OnTargetInRange?.Invoke(false);
		}
	}
}
