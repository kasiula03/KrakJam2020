using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class ShowObjectWhenPlayerInTrigger : MonoBehaviour
{
    [SerializeField] private CodeCanvas _object;

    void Reset()
    {
        GetComponent<Collider2D>().isTrigger = true;
    }

    void Start()
    {
        _object.Hide();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            _object.Show();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            _object.Hide();
        }
    }
}
