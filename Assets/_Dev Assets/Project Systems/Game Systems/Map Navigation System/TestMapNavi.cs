using EntityData;
using GameLoadingSystem;
using MapNavigationSystem;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestMapNavi : MonoBehaviour
{
    [SerializeField]
    private ActiveProjectFile activeProjectFile;

    [SerializeField]
    private MapNavigator mapNavigator;

    public InputAction inputAction;

    public void MovePlayer(Vector3Int destCoords)
    {
        mapNavigator.ExtractPlayerInfo();
        int returnValue = mapNavigator.TryMoveToTile(destCoords, out WorldMapData.Tile tile);

        if (returnValue == MapNavigator.NO_ISSUES)
        {
            Player currPlayer = activeProjectFile.Data.PlayerData.GetCurrentPlayer();
            currPlayer.WorldTileCoords = DataConversionHelper.TruncateVector3ToInt(tile.MapCoords);
            EntityPlacement.PlaceEntity(currPlayer, currPlayer.WorldTileCoords);
        }

        switch (returnValue)
        {
            case MapNavigator.NO_ISSUES:
                Debug.Log("No issues detected.");
            break;

            case MapNavigator.TILE_DOES_NOT_EXIST:
                Debug.Log("Destination tile does not exist!");
            break;

            case MapNavigator.INSUFFICIENT_MOVECOUNT:
                Debug.Log("Not enough moves accumalated as defined by the current tile!");
            break;

            case MapNavigator.CUSTOM_CONDITION_FAIL:
                Debug.Log("A custom condition is failing!");
            break;

            case MapNavigator.OUT_OF_RANGE:
                Debug.Log("The destTile is out of range from the max range values!");
            break;

            default:Debug.Log("This returned something other than a selected constant?");break;
        }
    }

    void OnEnable()
    {
        inputAction.Enable();
        inputAction.performed += OnDirSelect;
    }

    void OnDisable()
    {
        inputAction.Disable();
        inputAction.performed -= OnDirSelect;
    }

    void OnDirSelect(InputAction.CallbackContext context)
    {
        Vector2 vec2 = context.ReadValue<Vector2>();
        Vector3Int vec3Int = new((int)vec2.x, 0, (int)vec2.y);
        MovePlayer(vec3Int);
    }
}