using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class GameManager : Singleton<GameManager>
{
    [SerializeField] private List<DungeonLevelSO> dungeonLevelList;
    [SerializeField] private int currentDungeonLevelListIndex = 0;
    private Room currentRoom;
    private Room previousRoom;
    private PlayerDetailsSO playerDetails;
    private Player player;

    [HideInInspector] public GameState gameState;

    protected override void Awake()
    {
        base.Awake();

        playerDetails = GameResources.Instance.currentPlayer.playerDetails;

        InstantiatePlayer();
    }

    private void InstantiatePlayer()
    {
        GameObject playerGameObject = Instantiate(playerDetails.playerPrefab);

        player = playerGameObject.GetComponent<Player>();

        player.Initialize(playerDetails);
    }

    private void OnEnable()
    {
        StaticEventHandler.OnRoomChanged += StaticEventHandler_OnRoomChanged;
    }

    private void OnDisable()
    {
        StaticEventHandler.OnRoomChanged -= StaticEventHandler_OnRoomChanged;
    }

    private void StaticEventHandler_OnRoomChanged(RoomChangedEventArgs roomChangedEventArgs)
    {
        SetCurrentRoom(roomChangedEventArgs.room);
    }

    private void Start()
    {
        gameState = GameState.gameStarted;

        HandleGameState();
    }

    private void Update()
    {
        HandleGameState();

        
    }

    private void HandleGameState()
    {
        switch(gameState)
        {
            case GameState.gameStarted:
                PlayDungeonLevel(currentDungeonLevelListIndex);

                gameState = GameState.playingLevel;

                break;
        }
    }

    public void SetCurrentRoom(Room room)
    {
        previousRoom = currentRoom;
        currentRoom = room;
    }

    private void PlayDungeonLevel(int currentDungeonLevelListIndex)
    {
        bool dungeonBuilderSuccesfully = DungeonBuilder.Instance.GenerateDungeon(dungeonLevelList[currentDungeonLevelListIndex]);

        if(!dungeonBuilderSuccesfully)
        {
            Debug.LogError("������ ������ �� �����ϴ�.");
        }

        StaticEventHandler.CallRoomChangedEvent(currentRoom);

        player.gameObject.transform.position = new Vector3((currentRoom.lowerBounds.x + currentRoom.upperBounds.x) / 2f, (currentRoom.lowerBounds.y + currentRoom.upperBounds.y) / 2f, 0f);

        player.gameObject.transform.position = HelperUtilities.GetSpawnPositionNearestToPlayer(player.gameObject.transform.position);
    }

    public Player GetPlayer()
    {
        return player;  
    }

    public Room GetCurrentRoom()
    {
        return currentRoom;
    }

    public DungeonLevelSO GetCurrentDungeonLevel()
    {
        return dungeonLevelList[currentDungeonLevelListIndex];
    }

    #region ��ȿ���˻�
#if UNITY_EDITOR
    private void OnValidate()
    {
        HelperUtilities.ValidateCheckEnumerableValues(this, nameof(dungeonLevelList), dungeonLevelList);
    }
#endif

    #endregion
}
