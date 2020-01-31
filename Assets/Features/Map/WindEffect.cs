using UnityEngine;

public class WindEffect : MonoBehaviour
{
    [SerializeField] private Vector3 Direction;
    [SerializeField] private float Power;

    private Vector2 _startCorner;

    private void Start()
    {
        Bounds effectBounds = GetComponent<Collider2D>().bounds;
        _startCorner = new Vector2(effectBounds.center.x + (Direction.x * effectBounds.extents.x), effectBounds.center.y + (Direction.y * effectBounds.extents.y));
    }


    private void OnTriggerStay2D(Collider2D other)
    {
        float distanceFromSource = Vector2.Distance(_startCorner, other.transform.position);
        float forcePercent = 1;
        if(Direction.x != 0)
        {
            forcePercent = distanceFromSource / transform.localPosition.x;
        }
        else if(Direction.y != 0)
        {
            forcePercent = distanceFromSource / transform.localPosition.y;
        }

        other.GetComponent<Rigidbody2D>().AddForce(Direction * forcePercent * Power * Time.deltaTime);
    }
}
