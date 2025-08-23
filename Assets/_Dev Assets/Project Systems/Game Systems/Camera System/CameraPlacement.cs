using System;
using UnityEditor.PackageManager;
using UnityEngine;

namespace GameLoadingSystem
{

/// <summary>
/// static class used to place the camera with tag 'MainCamera' at either an entity or a scene point.
/// </summary>
public static class CameraPlacement
{
    private const string CameraGameObjectTag = "MainCamera";

    private static readonly Vector3 posMod = new Vector3(-6, 8, -6);
    private static readonly Vector3 rotEulerMod = new Vector3(55, 45, 0);

    /// <summary>
    /// Link the camera to an entity that has an active GameObject in the scene.
    /// For an invalid cameraGo, this will try to resolve itself by finding the camera through the "MainCamera" tag.
    /// </summary>
    /// <returns>If it successfully was able to link with an entity.</returns>
    /// <exception cref="NullReferenceException">The entity is null</exception>
    public static bool TryLinkCameraToEntity(EntityData.Entity entity, GameObject cameraGo)
    {
        if (entity == null)
        {
            throw new NullReferenceException();
        }

        if (cameraGo == null)
        {
            if (!TryGetCameraGo(out GameObject cameraGo2))
            {
                return false;
            }

            cameraGo = cameraGo2;
        }

        if (!TryGetEntityGo(entity, out GameObject entityGo))
        {
            return false;
        }

        cameraGo.transform.SetParent(entityGo.transform);
        cameraGo.transform.SetLocalPositionAndRotation(posMod, Quaternion.Euler(rotEulerMod));
        return true;
    }

    private static bool TryGetEntityGo(EntityData.Entity entity, out GameObject entityGo)
    {
        entityGo = entity.ActiveGameObject;
        return entityGo != null;
    }

    private static bool TryGetCameraGo(out GameObject cameraGo)
    {
        cameraGo = GameObject.FindWithTag(CameraGameObjectTag);
        return cameraGo != null;
    }
}
}