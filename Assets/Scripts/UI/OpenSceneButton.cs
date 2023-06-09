using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OpenSceneButton : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private string _sceneName;
    
    private void OnEnable()
    {
        _button.onClick.AddListener(OnButtonClick);
    }

    private void OnButtonClick()
    {
        SceneManager.LoadScene(_sceneName);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnButtonClick);
    }
}
