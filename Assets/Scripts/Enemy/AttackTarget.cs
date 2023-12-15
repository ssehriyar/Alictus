public class AttackTarget : IState
{
	private Enemy _enemy;

	public AttackTarget(Enemy enemy)
	{
		_enemy = enemy;
	}

	public void OnEnter()
	{
		_enemy.TriggerAnimation(Animations.Attack);
	}

	public void Tick()
	{

	}

	public void OnExit()
	{
		_enemy.AttackCompleted = false;
	}
}
