public class EmptyState : IState
{
	private Enemy _enemy;

	public EmptyState(Enemy enemy)
	{
		_enemy = enemy;
	}

	public void OnEnter()
	{

	}

	public void Tick()
	{

	}

	public void OnExit()
	{

	}
}
