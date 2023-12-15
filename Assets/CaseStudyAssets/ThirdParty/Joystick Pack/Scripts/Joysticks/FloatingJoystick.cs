using UnityEngine.EventSystems;

public class FloatingJoystick : Joystick
{
	public void OnEnable()
	{
		EventBus<OnPlayerDie>.AddListener(PlayerDieHandler);
	}

	protected override void Start()
	{
		base.Start();
		background.gameObject.SetActive(false);
	}

	public override void OnPointerDown(PointerEventData eventData)
	{
		background.anchoredPosition = ScreenPointToAnchoredPosition(eventData.position);
		background.gameObject.SetActive(true);
		base.OnPointerDown(eventData);
	}

	public override void OnPointerUp(PointerEventData eventData)
	{
		background.gameObject.SetActive(false);
		base.OnPointerUp(eventData);
	}
	private void PlayerDieHandler(object sender, OnPlayerDie e)
	{
		gameObject.SetActive(false);
	}

	public void OnDisable()
	{
		EventBus<OnPlayerDie>.RemoveListener(PlayerDieHandler);
	}
}