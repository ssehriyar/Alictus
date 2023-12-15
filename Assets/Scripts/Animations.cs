using UnityEngine;

public static class Animations
{
	public static readonly int Idle = Animator.StringToHash("Idle");
	public static readonly int Run = Animator.StringToHash("Run");
	public static readonly int PlayerRun = Animator.StringToHash("PlayerRun");
	public static readonly int Attack = Animator.StringToHash("Attack");
	public static readonly int Die = Animator.StringToHash("Die");
}

public static class Strings
{
	public static readonly string Gold = "CollectedGold";
}
