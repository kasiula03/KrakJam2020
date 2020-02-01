using UnityEngine;

public class PrefabRotate : MonoBehaviour
{
    void Update()
    {
        float f = transform.localRotation.eulerAngles.z;
        f += Time.deltaTime * 120f;
        transform.localRotation = Quaternion.Euler(0f, 0f, f);
    }
}
