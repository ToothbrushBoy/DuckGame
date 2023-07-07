using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraGizmos : MonoBehaviour
{
    [SerializeField, HideInInspector]
    new private Camera camera;

    [SerializeField, HideInInspector]
    private Texture2D viewportTexture;

    [SerializeField]
    private Color frustumColor = Color.red;

    [SerializeField]
    private Color viewportColor = Color.red;

    [SerializeField]
    private bool alwaysDrawGizmos = true;


    private Vector3[] frustumCorners = new Vector3[4];
    private Rect viewport = new Rect(0, 0, 1, 1);

    public Camera Camera
    {
        get
        {
            if (camera == null)
                camera = GetComponent<Camera>();
            return camera;
        }
    }
    public Texture2D ViewportTexture
    {
        get
        {
            if (viewportTexture == null)
                viewportTexture = new Texture2D(1, 1);

            return viewportTexture;
        }
    }

    void OnDrawGizmos()
    {
        if (alwaysDrawGizmos)
            DrawCameraGizmos(Camera);
    }

    void OnDrawGizmosSelected()
    {
        if (!alwaysDrawGizmos)
            DrawCameraGizmos(Camera);
    }

    public void DrawCameraGizmos(Camera camera)
    {
        // Save gizmos settings
        Color color = Gizmos.color;
        Matrix4x4 matrix = Gizmos.matrix;

        // Set gizmos matrix
        Gizmos.matrix = Matrix4x4.TRS(camera.transform.position, camera.transform.rotation, Vector3.one);

        // Compute viewport dimensions
        Gizmos.color = viewportColor;
        camera.CalculateFrustumCorners(viewport, camera.farClipPlane, Camera.MonoOrStereoscopicEye.Mono, frustumCorners);

        float width = Vector3.Distance(frustumCorners[2], frustumCorners[1]);
        float height = Vector3.Distance(frustumCorners[1], frustumCorners[0]);

        // Draw viewport using texture
        ViewportTexture.SetPixel(0, 0, viewportColor);
        ViewportTexture.Apply();

        Gizmos.DrawGUITexture(new Rect(transform.position.x - width * 0.5f, transform.position.y - height * 0.5f, width, height), ViewportTexture);

        // Draw frustum
        Gizmos.color = frustumColor;
        Gizmos.DrawFrustum(Vector3.forward * camera.nearClipPlane, camera.fieldOfView, camera.farClipPlane, camera.nearClipPlane, camera.aspect);


        // Restore gizmos settings
        Gizmos.color = color;
        Gizmos.matrix = matrix;
    }
}