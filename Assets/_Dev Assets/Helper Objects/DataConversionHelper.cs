using UnityEngine;

public static class DataConversionHelper
{
    public static Vector3Int TruncateVector3ToInt(Vector3 vec3)
    {
        return new Vector3Int((int)vec3.x, (int)vec3.y, (int)vec3.z);
    }
}