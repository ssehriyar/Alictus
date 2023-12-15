public struct OnPlayerDamaged
{
	public float CurrentHealth { get; private set; }
	public float MaxHealth { get; private set; }

	public OnPlayerDamaged(float currentHealth, float maxHealth)
	{
		CurrentHealth = currentHealth;
		MaxHealth = maxHealth;
	}
}
