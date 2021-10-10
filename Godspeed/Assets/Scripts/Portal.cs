using System;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] private UnityEngine.Camera MainCamera;
    [SerializeField] private UnityEngine.Camera PortalCamera;
    [SerializeField] private Transform Source;
    [SerializeField] private Transform Destination;

    private RenderTexture renderTex;
    [SerializeField] private Material portalMat;

    private void Awake()
    {
        renderTex = new RenderTexture(2000, 2000, 32, RenderTextureFormat.RGB111110Float, RenderTextureReadWrite.Linear);
        portalMat.mainTexture = renderTex;
        PortalCamera.targetTexture = renderTex;
        PortalCamera.enabled = true;
    }

    private void LateUpdate()
    {
        Matrix4x4 destinationFlipRotation =
            Matrix4x4.TRS(MathUtil.ZeroV3, Quaternion.AngleAxis(180.0f, Vector3.up), MathUtil.OneV3);
        Matrix4x4 sourceInvMat = destinationFlipRotation * Source.worldToLocalMatrix;
        Vector3 cameraPositionInSourceSpace =
            MathUtil.ToV3(sourceInvMat * MathUtil.PosToV4(MainCamera.transform.position));
        Quaternion cameraRotationInSourceSpace =
            MathUtil.QuaternionFromMatrix(sourceInvMat) * MainCamera.transform.rotation;
        PortalCamera.transform.position = Destination.TransformPoint(cameraPositionInSourceSpace);
        PortalCamera.transform.rotation = Destination.rotation * cameraRotationInSourceSpace;
        Vector4 clipPlaneWorldSpace =
            new Vector4(
                Destination.forward.x,
                Destination.forward.y,
                Destination.forward.z,
                Vector3.Dot(Destination.position, -Destination.forward));

        Vector4 clipPlaneCameraSpace =
            Matrix4x4.Transpose(Matrix4x4.Inverse(PortalCamera.worldToCameraMatrix)) * clipPlaneWorldSpace;
        PortalCamera.projectionMatrix = MainCamera.CalculateObliqueMatrix(-clipPlaneCameraSpace);
    }
}