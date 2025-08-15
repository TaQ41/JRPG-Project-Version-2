using UnityEngine;

namespace MapNavigationSystem
{

/// <summary>
/// Key player inputs to actions in the Map Navigator when enabled and no messages are being displayed.
/// </summary>
public class InputKeys : MonoBehaviour
{
    [SerializeField]
    private ActiveProjectFile activeProjectFile;

    [Sirenix.OdinInspector.Button]
    public void Init()
    {
        //MapNavigator.ExtractForPlayerMap(activeProjectFile.Data.PlayerData.GetCurrentPlayer(), activeProjectFile);
    }

    [Sirenix.OdinInspector.Button]
    public void Move(Vector3Int dir)
    {
        //if (MapNavigator.TryMoveToTile(dir, moveCount: 2, out Tile tile))
        //{
        //    
        //}
    }
}
}