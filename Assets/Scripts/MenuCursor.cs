
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuCursor : MonoBehaviour
{
    private void Update()
    {
        string sceneName = SceneManager.GetActiveScene().name;

        if (sceneName == "EndScene" || sceneName == "StartMenu" || sceneName == "EndSceneFail")
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
