using TMPro;
using UnityEngine;

public class GoldCounter : MonoBehaviour
{
	private int _currentGold;
	[SerializeField] private TextMeshProUGUI _text;

	public void OnEnable()
	{
		EventBus<OnGoldCollected>.AddListener(GoldCollectedHandler);
	}

	public void Start()
	{
		_currentGold = PlayerPrefs.GetInt(Strings.Gold, 0);
		_text.text = _currentGold.ToString();
	}

	private void GoldCollectedHandler(object sender, OnGoldCollected e)
	{
		_currentGold++;
		_text.text = _currentGold.ToString();
		PlayerPrefs.SetInt(Strings.Gold, _currentGold);
	}

	public void OnDisable()
	{
		EventBus<OnGoldCollected>.RemoveListener(GoldCollectedHandler);
	}
}
