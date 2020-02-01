using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float mulInterpolation;

    private void LateUpdate()
    {
        float interpolation = mulInterpolation * Time.deltaTime;

        Vector3 position = this.transform.position;
        position.y = Mathf.Lerp(this.transform.position.y, _target.position.y, interpolation);
        position.x = Mathf.Lerp(this.transform.position.x, _target.position.x, interpolation);

        this.transform.position = position;
    }
}
