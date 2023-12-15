using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
	[SerializeField] private float _barDuration;
	[SerializeField] private Image _barImage;

	public void OnEnable()
	{
		EventBus<OnPlayerDamaged>.AddListener(PlayerDamagedHandler);
	}

	private void UpdateHealthBar(float value, float maxValue)
	{
		_barImage.DOFillAmount(value / maxValue, _barDuration);
	}

	private void PlayerDamagedHandler(object sender, OnPlayerDamaged e)
	{
		UpdateHealthBar(e.CurrentHealth, e.MaxHealth);
	}

	public void OnDisable()
	{
		EventBus<OnPlayerDamaged>.RemoveListener(PlayerDamagedHandler);
	}
}
