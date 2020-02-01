using UnityEngine;


public class ShowDialogAction : ActionOnPlayer
{
    [SerializeField] private DialogBox _dialogTextBoxPrefab;
    [SerializeField] private string _text;
    [SerializeField] private string _speakerName;
    [SerializeField] private Canvas _mainCanvas;
    [SerializeField] private Vector3 _offset;

    public override void DoAction(Vector3 target)
    {
        if(_isOneTimeAction && executed)
        {
            return;
        }
        DialogBox dialog = Instantiate(_dialogTextBoxPrefab, transform.position + _offset, Quaternion.identity, _mainCanvas.transform);
        dialog.Show(_speakerName, _text);
        executed = true;
    }
}
