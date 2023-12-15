using System;
using ToolBox.Pools;
using UnityEngine;

public class Enemy : MonoBehaviour, IPoolable
{
	private StateMachine _stateMachine;

	private bool _targetInRange;
	[SerializeField] private float _moveSpeed;
	[SerializeField] private Animator _animator;
	[SerializeField] private AttackRange _attackRange;
	[SerializeField] private AnimationEvents _animationEvents;
	public bool IsUsing { get; private set; }
	public bool IsAlive { get; private set; }
	public Player Target { get; set; }
	public bool AttackCompleted { get; set; }

	private void Awake()
	{
		_stateMachine = new StateMachine();
		void At(IState to, IState from, Func<bool> condition) => _stateMachine.AddTransition(to, from, condition);

		EmptyState empty = new EmptyState(this);
		MoveTarget moveAttackRange = new MoveTarget(this, _moveSpeed);
		AttackTarget attackTarget = new AttackTarget(this);
		Die die = new Die(this);

		At(empty, moveAttackRange, () => IsUsing);
		At(moveAttackRange, attackTarget, () => _targetInRange);
		At(attackTarget, moveAttackRange, () => AttackCompleted && _targetInRange == false);
		At(die, moveAttackRange, () => IsAlive);

		_stateMachine.AddAnyTransition(die, () => IsAlive == false);

		_stateMachine.SetState(empty);
	}

	public void OnEnable()
	{
		_attackRange.OnTargetInRange += TargetInRangeHandler;
		_animationEvents.OnAttackAnimationCompleted += AttackAnimationCompletedHandler;
		_animationEvents.OnDieAnimationCompleted += DieAnimationCompletedHandler;
	}

	public void Start()
	{
		EventBus<OnPlayerRequested>.Emit(this, new OnPlayerRequested(GetPlayer));
		IsAlive = true;
		IsUsing = true;
	}

	private void Update() => _stateMachine.Tick();

	public void OnTriggerEnter(Collider other)
	{
		if (other.TryGetComponent(out Weapon weapon))
		{
			IsAlive = false;
		}
	}

	private void GetPlayer(Player player)
	{
		Target = player;
	}

	public void TriggerAnimation(int animationId)
	{
		_animator.SetTrigger(animationId);
	}

	private void TargetInRangeHandler(bool b)
	{
		_targetInRange = b;
	}

	private void AttackAnimationCompletedHandler()
	{
		AttackCompleted = true;
	}

	private void DieAnimationCompletedHandler()
	{
		gameObject.Release();
	}

	public void OnRelease()
	{
		IsUsing = false;
	}

	public void OnReuse()
	{
		Debug.Log("ONREUSE");
		IsAlive = true;
	}

	public void OnDisable()
	{
		_attackRange.OnTargetInRange -= TargetInRangeHandler;
		_animationEvents.OnAttackAnimationCompleted -= AttackAnimationCompletedHandler;
		_animationEvents.OnDieAnimationCompleted -= DieAnimationCompletedHandler;
	}
}
