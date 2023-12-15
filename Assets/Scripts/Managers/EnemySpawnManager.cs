using ToolBox.Pools;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
	private int _currentEnemyInPlay;
	private float _timer;
	[SerializeField] private int _spawnEnemyAtStart;
	[SerializeField] private int _maxEnemyAmountInPlay;
	[SerializeField] private float _enemySpawnCooldown;
	[SerializeField] private float _minXSpawnDistance;
	[SerializeField] private float _maxXSpawnDistance;
	[SerializeField] private float _minZSpawnDistance;
	[SerializeField] private float _maxZSpawnDistance;
	[SerializeField] private GameObject _enemyPrefab;
	[SerializeField] private Transform _player;

	public void OnEnable()
	{
		EventBus<OnEnemyKilled>.AddListener(EnemyKilledHandler);
		EventBus<OnPlayerDie>.AddListener(PlayerDieHandler);
	}

	private void Start()
	{
		_currentEnemyInPlay = 0;
		_timer = -1f;
		for (int i = 0; i < _spawnEnemyAtStart; i++)
		{
			Spawn();
		}
		ResetTimer();
	}

	private void Update()
	{
		SpawnCooldown();
		Spawn();
	}

	private void SpawnCooldown()
	{
		if (_timer > 0f)
		{
			_timer -= Time.deltaTime;
		}
	}

	private void Spawn()
	{
		if (_timer <= 0f && _currentEnemyInPlay < _maxEnemyAmountInPlay)
		{
			ResetTimer();
			_currentEnemyInPlay++;
			Vector3 spawnPos = transform.SpawnAroundCircle(_player, _minXSpawnDistance, _maxXSpawnDistance, _minZSpawnDistance, _maxZSpawnDistance);
			_enemyPrefab.Reuse<Enemy>(spawnPos, Quaternion.identity);
		}
	}

	private void ResetTimer()
	{
		_timer = _enemySpawnCooldown;
	}

	private void EnemyKilledHandler(object sender, OnEnemyKilled e)
	{
		_currentEnemyInPlay--;
	}

	private void PlayerDieHandler(object sender, OnPlayerDie e)
	{
		enabled = false;
		gameObject.SetActive(false);
	}

	public void OnDisable()
	{
		EventBus<OnEnemyKilled>.RemoveListener(EnemyKilledHandler);
		EventBus<OnPlayerDie>.RemoveListener(PlayerDieHandler);
	}
}
