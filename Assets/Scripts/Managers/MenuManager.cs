using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
	[SerializeField] private GameObject _menu;

	public void Start()
	{
		Application.targetFrameRate = 60;
		_menu.SetActive(false);
	}

	public void OnEnable()
	{
		EventBus<OnPlayerDie>.AddListener(PlayerDieHandler);
	}

	private void PlayerDieHandler(object sender, OnPlayerDie e)
	{
		_menu.SetActive(true);
	}

	public void Restart()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	public void OnDisable()
	{
		EventBus<OnPlayerDie>.RemoveListener(PlayerDieHandler);
	}
}
