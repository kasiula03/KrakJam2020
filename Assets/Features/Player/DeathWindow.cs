using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class DeathWindow : MonoBehaviour
{
    [Inject] private WorldEditorLogic _worldLogic;


    private bool _isWindowActive = false;
    public void InitWindow()
    {
        ChangeWindowColors();
        _isWindowActive = true;
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
    private void OnGUI()
    {
        if (_isWindowActive)
        {
            GUI.Box(new Rect(Screen.width / 4, Screen.height / 4, Screen.width / 2, Screen.height / 4), "\nLOOOOL\n\nPress Space button to restart the game!");
        }
    }

    private void ChangeWindowColors()
    {
        var _watchProperty = _worldLogic.GetPropertyFor(WorldEditorLogic.Properties.ColorBlue);
        _watchProperty.Value = 0.1f;

        _watchProperty = _worldLogic.GetPropertyFor(WorldEditorLogic.Properties.ColorGreen);
        _watchProperty.Value = 0.1f;

        _watchProperty = _worldLogic.GetPropertyFor(WorldEditorLogic.Properties.ColorRed);
        _watchProperty.Value = 0.1f;
    }
}
