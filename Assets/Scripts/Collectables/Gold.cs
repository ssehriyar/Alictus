using DG.Tweening;
using UnityEngine;

public class Gold : MonoBehaviour, ICollectable
{
	private Tween[] _tweens;
	[SerializeField] private Transform _model;

	[Header("Tween Settings")]
	[SerializeField] private float _moveAmount;
	[SerializeField] private float _moveDuration;
	[SerializeField] private float _rotateAmount;
	[SerializeField] private float _rotateDuration;

	private void Start()
	{
		Vector3 newPos = transform.position;
		newPos.y += _moveAmount;
		_tweens = new Tween[2];
		_tweens[0] = transform.DOMove(newPos, _moveDuration).SetLoops(-1, LoopType.Yoyo);
		_tweens[1] = _model.DORotate(_rotateAmount * Vector3.up, _rotateDuration, RotateMode.WorldAxisAdd).SetLoops(-1, LoopType.Incremental);
	}

	public void Collect()
	{
		EventBus<OnGoldCollected>.Emit(this, new OnGoldCollected());
		Destroy(gameObject);
	}

	public void OnDestroy()
	{
		if (_tweens != null)
		{
			for (int i = 0; i < _tweens.Length; i++)
			{
				_tweens[i].Kill();
			}
		}
	}
}
