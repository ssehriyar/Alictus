public class SearchTarget : IState
{
	private Enemy _enemy;

	public SearchTarget(Enemy enemy)
	{
		_enemy = enemy;
	}

	public void OnEnter()
	{
		_enemy.TriggerAnimation(Animations.Run);
		EventBus<OnPlayerRequested>.Emit(this, new OnPlayerRequested(GetPlayer));
	}

	public void Tick()
	{

	}

	private void GetPlayer(Player player)
	{
		_enemy.Target = player;
	}

	public void OnExit()
	{

	}
}
