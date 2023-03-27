using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MenuController : MonoBehaviour
{
    private UIDocument _document;
    private Button _playButton;
    private Button _quitButton;

    private void Awake()
    {
        _document = GetComponent<UIDocument>();
        _playButton = _document.rootVisualElement.Q<Button>("PlayButton");
        _playButton.clicked += PlayButtonClicked;
        _quitButton = _document.rootVisualElement.Q<Button>("QuitButton");
        _quitButton.clicked += QuitButtonClicked;
    }

    private void PlayButtonClicked()
    {
        GameManager.Instance.LoadGame();
    }

    private void QuitButtonClicked()
    {
        Application.Quit();
    }
}