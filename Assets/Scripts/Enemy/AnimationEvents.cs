using System;
using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
	public event Action OnAttackAnimationCompleted;
	public event Action OnDieAnimationCompleted;

	public void AttackAnimationEnd()
	{
		OnAttackAnimationCompleted?.Invoke();
	}

	public void DieAnimationEnd()
	{
		OnDieAnimationCompleted?.Invoke();
	}
}
