using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int MaxHealthy = 3;
    public int CurrentHealthy;
    public float MarginBetweenIcons = 0.1f;


    public Transform IconsPosition;
    public GameObject FullHealthIcon;
    public GameObject EmptyHealthIcon;


    private List<GameObject> _currentIcons= new List<GameObject>();

    void Start()
    {
        CurrentHealthy = MaxHealthy;
        RenderIcons();
    }

    public void SubHealth (int damage)
    {
        CurrentHealthy -= damage;
        if (CurrentHealthy < 0) 
            CurrentHealthy = 0;

        RenderIcons();
    }
    public void AddHealth(int heath)
    {
        CurrentHealthy += heath;
        if (CurrentHealthy > MaxHealthy)
            CurrentHealthy = MaxHealthy;

        RenderIcons();
    }
    public void RestartHealth()
    {
        CurrentHealthy = MaxHealthy;
        RenderIcons();
    }

    private void RenderIcons()
    {
        foreach (var icon in _currentIcons)
            Destroy(icon);
        _currentIcons.Clear();
        var width = FullHealthIcon.GetComponent<SpriteRenderer>().bounds.size.x + MarginBetweenIcons;
        for (int k = 0; k < MaxHealthy; k++)
        {
            GameObject newIcon;
            if (k < CurrentHealthy)
                newIcon = Instantiate(FullHealthIcon);
            else
                newIcon = Instantiate(EmptyHealthIcon);

            newIcon.transform.position = new Vector3(IconsPosition.position.x + k* width, IconsPosition.position.y, IconsPosition.position.z);
            newIcon.transform.parent = IconsPosition;
            _currentIcons.Add(newIcon);
        }
    }

}
