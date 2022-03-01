using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    [SerializeField] private Background _background;
    [SerializeField] private Background _startMenu;

    [SerializeField] private List<Button> _levels;

    [SerializeField] private Button _replayButton;
    [SerializeField] private Button _menuButton;

    [SerializeField] private Button _quitButton;
    void Start()
    {
        _quitButton.onClick.AddListener(() =>
        {
            Application.Quit();
        });

        _menuButton.onClick.AddListener(() => {
            ShowStartMenu();
            _menuButton.gameObject.SetActive(false);
        });

        _replayButton.onClick.AddListener(() =>
        {
            ShowStartMenu();
            _menuButton.gameObject.SetActive(false);
            _background.gameObject.SetActive(false);
        });

        print(_levels.Count);
        _levels.ForEach(level => level.onClick.AddListener(() => 
        {
            _menuButton.gameObject.SetActive(true);
            HideStartMenu(); 
        }));
    }

    public void ShowBackground()
    {
        _background.gameObject.SetActive(true);
    }
    public void ShowStartMenu()
    {
        _startMenu.gameObject.SetActive(true);
    }
    public void HideStartMenu()
    {
        _startMenu.gameObject.SetActive(false);
    }
}
