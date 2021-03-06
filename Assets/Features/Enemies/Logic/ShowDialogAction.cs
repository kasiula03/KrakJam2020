﻿using UnityEngine;
using Zenject;

public class ShowDialogAction : ActionOnPlayer
{
    [SerializeField] private DialogBox _dialogTextBoxPrefab;
    [SerializeField] private string _text;
    [SerializeField] private string _speakerName;
    [SerializeField] private Vector3 _offset;
    [SerializeField] private float _hideTime;

    [Inject(Id = "DialogCanvas")] private Canvas _mainCanvas;

    private DialogBox _dialogBox;

    private void Update()
    {
        if(_dialogBox != null && transform.hasChanged)
        {
            _dialogBox.transform.position = transform.position + _offset;
            transform.hasChanged = false;
        }
    }

    private void OnDestroy()
    {
        if (_dialogBox != null)
        {
            Destroy(_dialogBox.gameObject);
        }
    }

    public override void DoAction(Vector3 target)
    {
        if(_isOneTimeAction && executed)
        {
            return;
        }
        _dialogBox = Instantiate(_dialogTextBoxPrefab, transform.position + _offset, Quaternion.identity, _mainCanvas.transform);
        _dialogBox.Initialize();
        _dialogBox.Show(_speakerName, _text);
        executed = true;
        if(_hideTime > 0)
        {
            Invoke("HideWindow", _hideTime);
        }
    }

    public void HideWindow()
    {
        _dialogBox.Hide(() => {
            executed = false;
            Destroy(_dialogBox.gameObject); 
        });
    }
}
