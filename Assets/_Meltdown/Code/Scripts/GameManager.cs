using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public static GameManager Instance
    {
        get
        {
            if (!_instance)
            {
                _instance = FindObjectOfType<GameManager>();
            }

            if (_instance) return _instance;

            GameObject newInstanceObject = (GameObject) Instantiate(Resources.Load("GameManager"));
            newInstanceObject.name = "GameManager";
            GameObject.DontDestroyOnLoad(newInstanceObject);
            _instance = newInstanceObject.GetComponent<GameManager>();

            return _instance;
        }
    }

    private void Awake()
    {
        LoadMainMenu();
    }
    
    public void LoadMainMenu()
    {

        SceneManager.LoadScene("MainMenu", LoadSceneMode.Additive);
    }

    public void UnloadMeltdown()
    {
        SceneManager.UnloadSceneAsync("Meltdown");
    }

    public void LoadGame()
    {
        SceneManager.LoadScene("Meltdown", LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync("MainMenu");
    }
}