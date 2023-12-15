using DG.Tweening;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
	private Tween _tween;
	private float _timer;
	private Vector3 _targetPos;
	[SerializeField] private float _speed;
	[SerializeField] private float _selfDestroyTime;
	[field: SerializeField] public float Damage { get; private set; }

	[Header("Tweeen Settings")]
	[SerializeField] private float _scaleDownDuration;

	public void Start()
	{
		ResetTimer();
		EventBus<OnPlayerRequested>.Emit(this, new OnPlayerRequested(GetPlayer));
	}

	public void Update()
	{
		Move();
		DestroySelfTimer();
	}

	private void Move()
	{
		transform.position += _speed * Time.deltaTime * _targetPos;
	}

	private void DestroySelfTimer()
	{
		_timer -= Time.deltaTime;
		if (_timer <= 0f)
		{
			_tween = transform.DOScale(Vector3.zero, _scaleDownDuration).OnComplete(() => Destroy(gameObject));
		}
	}

	private void GetPlayer(Player player)
	{
		_targetPos = (player.transform.position - transform.position).normalized;
		_targetPos.y = 0f;
	}

	private void ResetTimer()
	{
		_timer = _selfDestroyTime;
	}

	public void OnDestroy()
	{
		if (_tween != null)
		{
			_tween.Kill();
		}
	}
}
