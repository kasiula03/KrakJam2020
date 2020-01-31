using UnityEngine;
 
[ExecuteInEditMode]
public class CameraPostProcess : MonoBehaviour {
 
    public Material material;
    public static CameraPostProcess Instance { get; private set; }

    void Awake()
    {
        Instance = this;
    }
    
    void OnRenderImage (RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination, material);
    }
}