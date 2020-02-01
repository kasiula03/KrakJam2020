using UnityEngine;


public class ShowDialogAction : ActionOnPlayer
{
    [SerializeField] private DialogBox _dialogTextBoxPrefab;
    [SerializeField] private string _text;
    [SerializeField] private string _speakerName;
    [SerializeField] private Canvas _mainCanvas;
    [SerializeField] private Vector3 _offset;

    private DialogBox _dialogBox;

    private void Update()
    {
        if(_dialogBox != null && transform.hasChanged)
        {
            _dialogBox.transform.position = transform.position + _offset;
            transform.hasChanged = false;
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
    }

    public void HideWindow()
    {
        _dialogBox.Hide(() => { Destroy(_dialogBox.gameObject); });
    }
}
