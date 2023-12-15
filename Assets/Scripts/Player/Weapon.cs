using UnityEngine;

public class Weapon : MonoBehaviour
{
	private Enemy _enemy;
	[SerializeField] private float _attackSpeed;

	public void Init(Enemy enemy)
	{
		_enemy = enemy;
	}

	private void Update()
	{
		if (_enemy.IsAlive == false)
		{
			Destroy(gameObject);
		}
		Move();
	}

	private void Move()
	{
		transform.position += _attackSpeed * Time.deltaTime * (_enemy.transform.position - transform.position).normalized;
	}

	public void OnTriggerEnter(Collider other)
	{
		if (other.TryGetComponent(out Enemy enemy))
		{
			Destroy(gameObject);
		}
	}
}
