using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpeachAction : MonoBehaviour
{
    [SerializeField] private List<ShowDialogAction> _showDialogActions;
    [SerializeField] private float _frequency;

    private int currentDialogNumber;
    private ShowDialogAction _currentDialog;

    private void Start()
    {
        InvokeRepeating("ShowDialog", _frequency, _frequency);
    }

    private void ShowDialog()
    {
        if(_currentDialog != null)
        {
            _currentDialog.HideWindow();
        }
        _currentDialog = _showDialogActions[currentDialogNumber];
        _currentDialog.DoAction(transform.position);
        currentDialogNumber++;
        if(currentDialogNumber >= _showDialogActions.Count)
        {
            currentDialogNumber = 0;
        }

    }
}
