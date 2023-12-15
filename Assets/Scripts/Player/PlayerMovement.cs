using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	[SerializeField] private float _movementSpeed;
	[SerializeField] private Transform _model;
	[SerializeField] private Animator _animator;
	[SerializeField] private FloatingJoystick _joystick;

	public void OnEnable()
	{
		EventBus<OnPlayerDie>.AddListener(PlayerDieHandler);
	}

	private void PlayerDieHandler(object sender, OnPlayerDie e)
	{
		enabled = false;
		_animator.SetTrigger(Animations.Die);
	}

	private void Update()
	{
		MoveAndRotate();
	}

	private void MoveAndRotate()
	{
		_animator.SetFloat(Animations.PlayerRun, _joystick.Direction.magnitude);
		if (_joystick.Direction.magnitude > 0f)
		{
			Vector3 input = Vector3.forward * _joystick.Vertical + Vector3.right * _joystick.Horizontal;
			transform.position = transform.position + _movementSpeed * Time.deltaTime * input;
			_model.transform.rotation = Quaternion.LookRotation(input);
		}
	}

	public void OnDisable()
	{
		EventBus<OnPlayerDie>.RemoveListener(PlayerDieHandler);
	}
}
