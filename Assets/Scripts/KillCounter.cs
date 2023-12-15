using TMPro;
using UnityEngine;

public class KillCounter : MonoBehaviour
{
	private int _killCount;
	[SerializeField] private TextMeshProUGUI _text;

	public void OnEnable()
	{
		EventBus<OnEnemyKilled>.AddListener(EnemyKilledHandler);
	}

	public void Start()
	{
		_killCount = 0;
		_text.text = _killCount.ToString();
	}

	private void EnemyKilledHandler(object sender, OnEnemyKilled e)
	{
		_killCount++;
		_text.text = _killCount.ToString();
	}

	public void OnDisable()
	{
		EventBus<OnEnemyKilled>.RemoveListener(EnemyKilledHandler);
	}
}
