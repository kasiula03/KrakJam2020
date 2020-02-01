using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private Vector2 mulInterpolation;
    [SerializeField] private Vector2 _offset;

    private void LateUpdate()
    {
        float interpolationX = mulInterpolation.x * Time.deltaTime;
        float interpolationY = mulInterpolation.y * Time.deltaTime;

        Vector3 position = this.transform.position;
        position.y = Mathf.Lerp(this.transform.position.y, _target.position.y + _offset.y, interpolationY);
        position.x = Mathf.Lerp(this.transform.position.x, _target.position.x + _offset.x, interpolationX);

        this.transform.position = position;
    }
}
