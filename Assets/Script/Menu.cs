using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] string _noConnectionMessage = "No connection to the server";
    [SerializeField] private GameObject _mainMenuView;
    [SerializeField] private GameObject _loginView;
    [SerializeField] private GameObject _registerView;
    [SerializeField] private GameObject _messageBox;
    [SerializeField] private GameObject _userListView;
    [SerializeField] private GameObject _userContainer;
    [SerializeField] private GameObject _userNamePrefab;
    [SerializeField] private GameObject _optionsView;
    [SerializeField] private GameObject _videoOptionsView;

    private List<GameObject> _views;
    private readonly API _api = new("http://localhost:5000/");

    void Start()
    {
        _views = new List<GameObject> { _mainMenuView, _loginView, _registerView, _userListView, _optionsView, _videoOptionsView };
        Debug.Log(PlayerPrefs.GetString("cookies"));
    }

    public void NewGame()
    {
        SceneManager.LoadScene("Map");
    }

    public async void SetActiveView(GameObject targetView)
    {
        if (targetView == _loginView)
        {
            try
            {
                IEnumerable<string> cookies = LoadCookies();
                await _api.Authenticate(cookies);
                string[] cookiesArray = cookies.ToArray();
                if (cookiesArray.Length != 1 && cookiesArray[0] != "")
                {
                    SetActiveView(_userListView);
                    return;
                }
            }
            catch (HttpRequestException)
            {
                ShowMessage(_noConnectionMessage);
                SetActiveView(_mainMenuView);
                return;
            }
            catch (APIException)
            {
                IEnumerable<string> cookies = LoadCookies();
                string[] cookiesArray = cookies.ToArray();
                if (cookiesArray.Length != 1 && cookiesArray[0] != "")
                {
                    SetActiveView(_userListView);
                    return;
                }
            }
        }

        if (targetView == _userListView)
        {
            LoadUsers();
        }
        _views.ForEach(view => view.SetActive(view == targetView));
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void Fullscreen()
    {
        Screen.fullScreen = !Screen.fullScreen;
    }

    public async void Login()
    {
        TMP_InputField loginField = GameObject.FindGameObjectWithTag("login").GetComponent<TMP_InputField>();
        TMP_InputField passwordField = GameObject.FindGameObjectWithTag("password").GetComponent<TMP_InputField>();
        try
        {
            string login = loginField.text;
            string password = passwordField.text;
            IEnumerable<string> cookies = await _api.Login(login, password);
            SaveCookies(cookies, login, password);
            SetActiveView(_userListView);
        }
        catch (APIException ex)
        {
            ShowMessage(ex.Message);
        }
        catch (HttpRequestException)
        {
            ShowMessage(_noConnectionMessage);
        }
        finally
        {
            loginField.text = "";
            passwordField.text = "";
        }
    }

    public void Logout()
    {
        try
        {
            IEnumerable<string> cookies = LoadCookies();
            // await _api.Logout(cookies);
            PlayerPrefs.DeleteKey("cookies");
            PlayerPrefs.DeleteKey("username");
            PlayerPrefs.DeleteKey("password");
            SetActiveView(_mainMenuView);
        }
        catch (APIException ex)
        {
            ShowMessage(ex.Message);
        }
        catch (HttpRequestException)
        {
            ShowMessage(_noConnectionMessage);
        }
    }

    public async void Register()
    {
        TMP_InputField loginField = GameObject.FindGameObjectWithTag("reglogin").GetComponent<TMP_InputField>();
        TMP_InputField passwordField = GameObject.FindGameObjectWithTag("regpassword").GetComponent<TMP_InputField>();
        try
        {
            string login = loginField.text;
            string password = passwordField.text;
            if (string.IsNullOrEmpty(login))
            {
                // Handle empty name error
                ShowMessage("Empty name");
                return;
            }
            else if (string.IsNullOrEmpty(password))
            {
                // Handle empty password error
                ShowMessage("Empty password");
                return;
            }
            await _api.Register(login, password);
            ShowMessage("User registered");
            SetActiveView(_loginView);
        }
        catch (APIException ex)
        {
            ShowMessage(ex.Message);
        }
        catch (HttpRequestException)
        {
            ShowMessage(_noConnectionMessage);
        }
        finally
        {
            loginField.text = "";
            passwordField.text = "";
        }
    }

    public async void LoadUsers()
    {
        try
        {
            foreach (Transform child in _userContainer.transform)
            {
                Destroy(child.gameObject);
            }
            string login = PlayerPrefs.GetString("username");
            string password = PlayerPrefs.GetString("password");
            IEnumerable<string> cookies = await _api.Login(login, password);
            List<ReadUser> users = await _api.Users(cookies);
            foreach (ReadUser user in users)
            {
                GameObject userName = Instantiate(_userNamePrefab, _userContainer.transform);
                userName.GetComponent<TextMeshProUGUI>().text = user.username;
            }
        }
        catch (APIException ex)
        {
            ShowMessage(ex.Message);
        }
        catch (HttpRequestException)
        {
            ShowMessage(_noConnectionMessage);
        }
    }

    private void SaveCookies(IEnumerable<string> cookies, string login, string password)
    {
        // Save cookies for future requests in player prefs
        PlayerPrefs.SetString("cookies", string.Join(";", cookies));
        PlayerPrefs.SetString("username", login);
        PlayerPrefs.SetString("password", password);
        Debug.Log(PlayerPrefs.GetString("cookies"));
    }

    private IEnumerable<string> LoadCookies()
    {
        // Load cookies for future requests from player prefs
        string cookies = PlayerPrefs.GetString("cookies");
        return cookies.Split(';');
    }

    private void ShowMessage(string message)
    {
        _messageBox.GetComponentInChildren<TextMeshProUGUI>().text = message;
        _messageBox.SetActive(true);
    }
}