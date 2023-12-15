using UnityEngine;

public class MoveTarget : IState
{
	private Enemy _enemy;
	private float _moveSpeed;

	public MoveTarget(Enemy enemy, float moveSpeed)
	{
		_enemy = enemy;
		_moveSpeed = moveSpeed;
	}

	public void OnEnter()
	{
		_enemy.TriggerAnimation(Animations.Run);
	}

	public void Tick()
	{
		Move();
	}

	private void Move()
	{
		Vector3 input = (_enemy.Target.transform.position - _enemy.transform.position).normalized;
		_enemy.transform.SetPositionAndRotation
		(
			_enemy.transform.position + _moveSpeed * Time.deltaTime * input,
			Quaternion.LookRotation(input)
		);
	}

	public void OnExit()
	{
		Move();
	}
}
