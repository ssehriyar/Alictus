using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
	private float _timer;
	private List<Enemy> _enemies;
	[SerializeField] private float _attackCooldown;
	[SerializeField] private Weapon _weapon;
	[SerializeField] private Transform _weaponSpawnPos;

	public void OnEnable()
	{
		EventBus<OnPlayerDie>.AddListener(PlayerDieHandler);
		EventBus<OnEnemyKilled>.AddListener(EnemyKilledHandler);
	}

	private void Start()
	{
		_enemies = new List<Enemy>();
		ResetTimer();
	}

	private void Update()
	{
		AttackCooldown();
		AttackEnemy();
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.transform.root.TryGetComponent(out Enemy enemy) && enemy.IsAlive && _enemies.Contains(enemy) == false)
		{
			_enemies.Add(enemy);
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.transform.root.TryGetComponent(out Enemy enemy) && _enemies.Contains(enemy))
		{
			_enemies.Remove(enemy);
		}
	}

	private void AttackCooldown()
	{
		if (_timer > 0f)
		{
			_timer -= Time.deltaTime;
		}
	}

	private void AttackEnemy()
	{
		if (_timer <= 0f && _enemies.Count > 0)
		{
			ResetTimer();
			Weapon weapon = Instantiate(_weapon, _weaponSpawnPos.position, Quaternion.identity);
			weapon.Init(GetClosestEnemy());
		}
	}

	private void ResetTimer()
	{
		_timer = _attackCooldown;
	}

	private Enemy GetClosestEnemy()
	{
		return _enemies.OrderByDescending(e => (e.transform.position - transform.position).sqrMagnitude).First();
	}

	private void PlayerDieHandler(object sender, OnPlayerDie e)
	{
		enabled = false;
	}

	private void EnemyKilledHandler(object sender, OnEnemyKilled e)
	{
		if (_enemies.Contains(e.Enemy))
		{
			_enemies.Remove(e.Enemy);
		}
	}

	public void OnDisable()
	{
		EventBus<OnPlayerDie>.RemoveListener(PlayerDieHandler);
		EventBus<OnEnemyKilled>.RemoveListener(EnemyKilledHandler);
	}
}
