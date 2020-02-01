using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpecialAction : MonoBehaviour
{
    [SerializeField] private ShowDialogAction _showDialogAction;

    public abstract void Perform();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController player = collision.gameObject.GetComponent<PlayerController>();
        if(player != null)
        {
            Debug.Log("Player in range");
            player.SetupSpecialAction(this);
            DisplayTooltip(collision.transform.position);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        PlayerController player = collision.gameObject.GetComponent<PlayerController>();
        if (player != null)
        {
            Debug.Log("Player exit range");
            player.ClearSpecialAction();
            _showDialogAction.HideWindow();
        }
    }

    private void DisplayTooltip(Vector3 position)
    {
        _showDialogAction.DoAction(position);
    }
}
