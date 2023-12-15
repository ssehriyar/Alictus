using UnityEngine;

public class Player : MonoBehaviour
{
	private float _currentHealth;
	[SerializeField] private float _maxHealth;

	public void Start()
	{
		_currentHealth = _maxHealth;
		EventBus<OnPlayerDamaged>.Emit(this, new OnPlayerDamaged(_currentHealth, _maxHealth));
	}

	public void OnEnable()
	{
		EventBus<OnPlayerRequested>.AddListener(PlayerRequestedHandler);
	}

	public void OnTriggerEnter(Collider other)
	{
		if (other.TryGetComponent(out EnemyProjectile enemyProjectile))
		{
			DecreaseHealth(enemyProjectile.Damage);
		}
		else if (other.TryGetComponent(out Enemy enemy) && enemy.IsAlive)
		{
			Die();
		}
		else if (other.TryGetComponent(out ICollectable collectable))
		{
			collectable.Collect();
		}
	}

	private void DecreaseHealth(float damage)
	{
		_currentHealth -= damage;
		if (_currentHealth <= 0f)
		{
			Die();
		}
		else
		{
			EventBus<OnPlayerDamaged>.Emit(this, new OnPlayerDamaged(_currentHealth, _maxHealth));
		}
	}

	private void Die()
	{
		_currentHealth = 0f;
		EventBus<OnPlayerDamaged>.Emit(this, new OnPlayerDamaged(_currentHealth, _maxHealth));
		EventBus<OnPlayerDie>.Emit(this, new OnPlayerDie());
		enabled = false;
	}

	private void PlayerRequestedHandler(object sender, OnPlayerRequested e)
	{
		e.Player(this);
	}

	public void OnDisable()
	{
		EventBus<OnPlayerRequested>.RemoveListener(PlayerRequestedHandler);
	}
}
