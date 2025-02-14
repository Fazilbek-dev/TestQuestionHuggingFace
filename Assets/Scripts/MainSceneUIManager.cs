using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainSceneUIManager : MonoBehaviour
{
    private enum MenuState
    {
        MainMenu,
        Settings
    }

    [SerializeField] private MenuState _menuState = MenuState.MainMenu;

    [SerializeField] private SceneLoader _loader;

    [SerializeField] private Button _openChatButton;
    [SerializeField] private Button _settingsButton;
    [SerializeField] private Button _backToMainButton;
    [SerializeField] private Button _saveUIColorButton;
    [SerializeField] private Button _defaultUIColorButton;

    [SerializeField] private GameObject _mainMenuUI;
    [SerializeField] private GameObject _settingsMenuUI;

    [SerializeField] private TextMeshProUGUI _userColor;
    [SerializeField] private Color _defaulUserColor = Color.white;
    [SerializeField] private Slider _userRed;
    [SerializeField] private Slider _userGreen;
    [SerializeField] private Slider _userBlue;

    [SerializeField] private TextMeshProUGUI _botColor;
    [SerializeField] private Color _defaultBotColor = new Color(1f, 0.6f, 0f);
    [SerializeField] private Slider _botRed;
    [SerializeField] private Slider _botGreen;
    [SerializeField] private Slider _botBlue;

    private void Awake()
    {
        _openChatButton.onClick.AddListener(ChatScene);
        _settingsButton.onClick.AddListener(ChangeStateMenu);
        _backToMainButton.onClick.AddListener(ChangeStateMenu);
        _saveUIColorButton.onClick.AddListener(SaveButton);
        _defaultUIColorButton.onClick.AddListener(DefaultUIColor);

        _userRed.onValueChanged.AddListener(delegate { ChangeColorRealTimeUI(); });
        _userGreen.onValueChanged.AddListener(delegate { ChangeColorRealTimeUI(); });
        _userBlue.onValueChanged.AddListener(delegate { ChangeColorRealTimeUI(); });

        _botRed.onValueChanged.AddListener(delegate { ChangeColorRealTimeUI(); });
        _botGreen.onValueChanged.AddListener(delegate { ChangeColorRealTimeUI(); });
        _botBlue.onValueChanged.AddListener(delegate { ChangeColorRealTimeUI(); });

        if (CheckColorToDidntChange())
        {
            DefaultUIColor();
        }
        else
        {
            SetColorsFromSave();
        }
    }

    private void ChangeColorRealTimeUI()
    {
        _userColor.color = new Color(_userRed.value, _userGreen.value, _userBlue.value);
        _botColor.color = new Color(_botRed.value, _botGreen.value, _botBlue.value);
        if (!CheckColorSaveToChange()) _saveUIColorButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Saved";
        else _saveUIColorButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Save";
    }

    public void SaveButton()
    {
        _saveUIColorButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Saved";
        SaveUIColor();
    }

    private void SaveUIColor()
    {
        PlayerPrefs.SetFloat("userR", _userRed.value);
        PlayerPrefs.SetFloat("userG", _userGreen.value);
        PlayerPrefs.SetFloat("userB", _userBlue.value);

        PlayerPrefs.SetFloat("botR", _botRed.value);
        PlayerPrefs.SetFloat("botG", _botGreen.value);
        PlayerPrefs.SetFloat("botB", _botBlue.value);

        PlayerPrefs.Save();
    }

    private void DefaultUIColor()
    {
        _userRed.value = _defaulUserColor.r;
        _userGreen.value = _defaulUserColor.g;
        _userBlue.value = _defaulUserColor.b;

        _botRed.value = _defaultBotColor.r;
        _botGreen.value = _defaultBotColor.g;
        _botBlue.value = _defaultBotColor.b;

        SaveUIColor();
    }

    private void SetColorsFromSave()
    {
        _userRed.value = PlayerPrefs.GetFloat("userR");
        _userGreen.value = PlayerPrefs.GetFloat("userG");
        _userBlue.value = PlayerPrefs.GetFloat("userB");

        _botRed.value = PlayerPrefs.GetFloat("botR");
        _botGreen.value = PlayerPrefs.GetFloat("botG");
        _botBlue.value = PlayerPrefs.GetFloat("botB");

        ChangeColorRealTimeUI();
    }

    private bool CheckColorSaveToChange()
    {
        if(PlayerPrefs.GetFloat("userR") == _userRed.value &&
            PlayerPrefs.GetFloat("userG") == _userGreen.value &&
            PlayerPrefs.GetFloat("userB") == _userBlue.value &&
            PlayerPrefs.GetFloat("botR") == _botRed.value &&
            PlayerPrefs.GetFloat("botG") == _botGreen.value &&
            PlayerPrefs.GetFloat("botB") == _botBlue.value)
        {
            return false;
        }
        else
            return true;
    }

    private bool CheckColorToDidntChange()
    {
        if (PlayerPrefs.GetFloat("userR") == 0f &&
            PlayerPrefs.GetFloat("userG") == 0f &&
            PlayerPrefs.GetFloat("userB") == 0f &&
            PlayerPrefs.GetFloat("botR") == 0f &&
            PlayerPrefs.GetFloat("botG") == 0f &&
            PlayerPrefs.GetFloat("botB") == 0f)
        {
            return true;
        }
        else
            return false;
    }

    public void ChatScene()
    {
        _loader.LoadSceneAsync(SceneData.SceneType.Chat);
    }


    public void ChangeStateMenu()
    {
        if(_menuState == MenuState.MainMenu)
        {
            _menuState = MenuState.Settings;
            _mainMenuUI.SetActive(false);
            _settingsMenuUI.SetActive(true);
        }
        else
        {
            _menuState = MenuState.MainMenu;
            _mainMenuUI.SetActive(true);
            _settingsMenuUI.SetActive(false);
        }
    }
}
