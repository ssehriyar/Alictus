using UnityEngine;

public class ProjectileSpawner : MonoBehaviour
{
	[SerializeField] private Transform _spawnPos;
	[SerializeField] private EnemyProjectile _enemyProjectile;

	public void SpawnProjectile()
	{
		Instantiate(_enemyProjectile, _spawnPos.position, Quaternion.identity);
	}
}
