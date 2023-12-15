public class Die : IState
{
	private Enemy _enemy;

	public Die(Enemy enemy)
	{
		_enemy = enemy;
	}

	public void OnEnter()
	{
		_enemy.TriggerAnimation(Animations.Die);
		EventBus<OnEnemyKilled>.Emit(_enemy, new OnEnemyKilled(_enemy));
	}

	public void Tick()
	{

	}

	public void OnExit()
	{

	}
}
