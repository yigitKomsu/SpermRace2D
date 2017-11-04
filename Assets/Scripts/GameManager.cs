using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Text EndGameText;
    [SerializeField]
    private Button Start, Exit, ShakeIt;
    [SerializeField]
    private Button[] PushButtons;
    private static GameObject MyObject;

    public bool GameStarted = false;

    private void Awake()
    {
        MyObject = gameObject;
    }

    public static GameObject GetManager()
    {
        return MyObject;
    }

    public void StartGame()
    {
        ShakeIt.gameObject.SetActive(false);
        foreach (var item in PushButtons)
        {
            item.gameObject.SetActive(true);
        }
    }

    public void InitGame()
    {

    }

    public void EndGame()
    {
        EndGameText.gameObject.SetActive(true);
        Exit.gameObject.SetActive(true);
        Start.gameObject.SetActive(true);
        foreach (var item in PushButtons)
        {
            item.gameObject.SetActive(false);
        }
    }

    public void StartButton()
    {
        SceneManager.LoadScene(1);
    }

    public void ExitButton()
    {
        Application.Quit();
    }
}
