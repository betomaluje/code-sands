using System;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private MenuInput menuInput;
    [SerializeField] private bool waitForComplete = true;
    [SerializeField] private SceneLoader sceneLoader;
    [SerializeField] Slider loadingProgbar;
    [SerializeField] TMPro.TextMeshProUGUI loadingText;
    [SerializeField] List<MenuPage> pages;

    private void Awake()
    {
        ShowPage(typeof(MainPage));
    }

    private void OnEnable()
    {
        sceneLoader.OnLoadingStart += OnLoadingStart;
        sceneLoader.OnLoadingProgress += OnLoadingProgress;
        sceneLoader.OnLoadingComplete += OnLoadingComplete;
    }

    private void OnDisable()
    {
        sceneLoader.OnLoadingStart -= OnLoadingStart;
        sceneLoader.OnLoadingProgress -= OnLoadingProgress;
        sceneLoader.OnLoadingComplete -= OnLoadingComplete;
    }

    private void OnLoadingStart()
    {
        ShowPage(typeof(LoadingPage));
        loadingText.text = "LOADING...";
    }

    private void OnLoadingProgress(float progress)
    {
        loadingProgbar.value = progress;
    }

    private void OnLoadingComplete(AsyncOperation sceneAO)
    {
        loadingProgbar.value = 1f;

        if (waitForComplete)
        {
            loadingText.text = "PRESS SELECT TO CONTINUE";
            if (menuInput.IsSelectPressed)
            {
                sceneAO.allowSceneActivation = true;
            }
        }
        else
        {
            sceneAO.allowSceneActivation = true;
        }
    }

    public void Click_Start() { sceneLoader.ChangeScene("SampleScene"); }

    public void Click_Options() { ShowPage(typeof(OptionsPage)); }

    public void Click_Credits() { }

    public void Click_Back() { ShowPage(typeof(MainPage)); }

    public void Click_Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }

    public void ShowPage(Type page)
    {
        foreach (MenuPage item in pages)
        {
            if (page.Equals(item.GetType()))
            {
                item.PageObject.gameObject.SetActive(true);
            }
            else
            {
                item.PageObject.gameObject.SetActive(false);
            }
        }
    }
}
