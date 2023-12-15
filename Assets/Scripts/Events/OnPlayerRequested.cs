using System;

public struct OnPlayerRequested
{
	public Action<Player> Player;

	public OnPlayerRequested(Action<Player> player)
	{
		Player = player;
	}
}
