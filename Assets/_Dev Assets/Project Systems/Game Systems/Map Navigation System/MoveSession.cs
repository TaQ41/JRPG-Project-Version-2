using System;
using UnityEngine;
using UnityEngine.InputSystem;
using WorldMapData;

namespace MapNavigationSystem
{

/// <summary>
/// Control the calls made within the game to move within maps and get the results.
/// Initialize the environment based on the current player.
/// </summary>
public class MoveSession: MonoBehaviour
{
    [SerializeField]
    private ActiveProjectFile activeProjectFile;

    [SerializeField]
    private GameLoadingSystem.GameLoader gameLoader;

    [SerializeField]
    private MapNavigator mapNavigator;

    [SerializeField]
    private BattleSystem.BattleHandler battleHandler;

    [SerializeField]
    private ProjectInputActionAsset inputActions;

    private EntityData.Player CurrentPlayer { get { return activeProjectFile.Data.PlayerData.GetCurrentPlayer(); } }
    private Tile CurrentTile { get; set; }

    #region MoveCount

    [Sirenix.OdinInspector.ShowInInspector]
    public int MoveCount { get; set; }

    /// <summary>
    /// Get the current chance to roll a zero without any player or environmental effects.
    /// </summary>
    /// <exception cref="DivideByZeroException"></exception>
    public float ChanceForZero
    {
        get
        {
            if (defaultMaxRange * defaultZeroSubRange == 0)
                throw new DivideByZeroException();

            return 100f / defaultMaxRange / defaultZeroSubRange;
        }
    }

    [Sirenix.OdinInspector.Title("Current Chance For Zero", "@ChanceForZero.ToString(\"F3\")")]

    [SerializeField]
    [Range (1, 100)]
    [Tooltip("The default max range the player can move to with no modifications. (exclusive)")]
    private int defaultMaxRange;

    [SerializeField]
    [Range (1, 100)]
    [Tooltip("When the defaultMaxRange gets a zero, it runs a random number again from 0 to this (exclusive) to assure that zero value.")]
    private int defaultZeroSubRange;

    public int GenerateMoveCount()
    {
        // Calculate player effects and game environment variables to modify this range.
        int moveCount = UnityEngine.Random.Range(0, defaultMaxRange);

        // 25% * 10% = 2.5% to get a 0
        // Later some effects may alter how this works.
        if (moveCount == 0 && UnityEngine.Random.Range(0, defaultZeroSubRange) != 0)
        {
            moveCount = UnityEngine.Random.Range(1, defaultMaxRange);
        }

        return moveCount;
    }

    #endregion

    /// <summary>
    /// Initializes for players that are inside of a World Map.
    /// </summary>
    public void InitializeForPlayerEnv()
    {
        mapNavigator.WorldMap = activeProjectFile.Data.WorldMapData.SearchForMap(CurrentPlayer.livingMapName);
        mapNavigator.MoveHistory = new() {CurrentPlayer.WorldTileCoords};
    }

    /// <summary>
    /// Initializes for entities contained in the battle map and uses the current entity that should have its turn.
    /// Includes players.
    /// </summary>
    public void InitializeForBattleEnv()
    {
        // Requires a battle container.
        throw new NotImplementedException();
    }

    public void EnableMoveInput()
    {
        inputActions = new();
        inputActions.MoveControls.Enable();

        inputActions.MoveControls.LandOnTile.performed += OnLandOnTile;
        inputActions.MoveControls.MovePlayerDirections.performed += OnMovePlayer;
        inputActions.MoveControls.MoveBack.performed += OnMoveBack;
    }

    [Sirenix.OdinInspector.Button]
    public void BeginMoveSession(bool isBattle = false)
    {
        CurrentTile = null;
        MoveCount = GenerateMoveCount();
        if (MoveCount == 0)
        {
            EndMoveSession(wasZero: true);
            return;
        }

        EnableMoveInput();
        if (isBattle)
        {
            InitializeForBattleEnv();
            return;
        }

        InitializeForPlayerEnv();
    }

    void OnDisable()
    {
        if (inputActions == null)
            return;

        inputActions.MoveControls.Disable();

        inputActions.MoveControls.LandOnTile.performed -= OnLandOnTile;
        inputActions.MoveControls.MovePlayerDirections.performed -= OnMovePlayer;
        inputActions.MoveControls.MoveBack.performed -= OnMoveBack;
    }

    private void OnLandOnTile(InputAction.CallbackContext context)
    {
        if (mapNavigator.MoveHistory.Count == 1)
        {
            Debug.Log("Can't land with no moves played!");
            return;
        }

        EndMoveSession();

        // "Battle" is a placeholder, the tile will decide this.
        int battleIndex = battleHandler.GenerateBattle(activeProjectFile.Data.WorldMapData.SearchForMap("Battle"));
        CurrentPlayer.JoinBattle(activeProjectFile.Data.CurrentBattles[battleIndex]);
        
        // PlaceHolder enemies, these are decided by later factors
        new EntityData.Goblin().JoinBattle(activeProjectFile.Data.CurrentBattles[battleIndex]);
        gameLoader.LoadGameWorld(); // Reload the GameWorld now.
    }

    private void OnMovePlayer(InputAction.CallbackContext context)
    {
        Vector2 fdir = context.ReadValue<Vector2>();
        mapNavigator.MoveEntity(DataConversionHelper.TruncateVector2ToInt(fdir));
    }

    private void OnMoveBack(InputAction.CallbackContext context)
    {
        mapNavigator.MoveBackward();
    }

    /// <summary>
    /// Called after successfully moving to another tile in a normal sequence.
    /// </summary>
    /// <param name="tile"></param>
    public void PassTile(Tile tile)
    {
        GameLoadingSystem.EntityPlacement.PlaceEntity(CurrentPlayer, tile.MapCoords);
        CurrentTile = tile;
    }

    /// <summary>
    /// Called after confirming the current tile to be the tile the player should be set to.
    /// </summary>
    public void EndMoveSession(bool wasZero = false)
    {
        OnDisable();

        if (wasZero)
            return;

        if (mapNavigator.WorldMap.IsMapTypeBattle)
        {
            CurrentPlayer.BattleTileCoords = CurrentTile.MapCoords;
        }
        else
        {
            CurrentPlayer.WorldTileCoords = CurrentTile.MapCoords;
        }
    }
}
}