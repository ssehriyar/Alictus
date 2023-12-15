using ToolBox.Pools;
using UnityEngine;

public class GoldSpawnManager : MonoBehaviour
{
	private int _currentGoldInPlay;
	[SerializeField] private int _maxGoldAmountInPlay;
	[SerializeField] private float _minXSpawnDistance;
	[SerializeField] private float _maxXSpawnDistance;
	[SerializeField] private float _minZSpawnDistance;
	[SerializeField] private float _maxZSpawnDistance;
	[SerializeField] private GameObject _goldPrefab;
	[SerializeField] private Transform _player;

	public void Start()
	{
		for (int i = 0; i < _maxGoldAmountInPlay; i++)
		{
			Spawn();
		}
	}

	private void Spawn()
	{
		if (_currentGoldInPlay < _maxGoldAmountInPlay)
		{
			_currentGoldInPlay++;

			Vector3 spawnPos = transform.SpawnAroundCircle(_player, _minXSpawnDistance, _maxXSpawnDistance, _minZSpawnDistance, _maxZSpawnDistance);

			_goldPrefab.Reuse(spawnPos, Quaternion.identity);
		}
	}
}
