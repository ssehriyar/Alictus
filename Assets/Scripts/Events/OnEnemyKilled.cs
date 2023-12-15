public struct OnEnemyKilled
{
	public Enemy Enemy { get; private set; }

	public OnEnemyKilled(Enemy enemy)
	{
		Enemy = enemy;
	}
}
