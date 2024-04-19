using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Settings 
{
    #region 단위 세팅
    public const float pixelsPerUnit = 16f;
    public const float tileSizePixels = 16f;
    #endregion

    #region 던전 세팅

    public const int maxDungeonRebuildAttemptsForRoomGraph = 1000;
    public const int maxDungeonBuildAttempts = 10;

    #endregion

    #region 방 세팅

    public const float fadeInTime = 0.5f;
    public const int maxChildCorridors = 3;

    #endregion

    #region 애니메이터 파라미터
    public static int aimUp = Animator.StringToHash("aimUp");
    public static int aimDown = Animator.StringToHash("aimDown");
    public static int aimUpRight = Animator.StringToHash("aimUpRight");
    public static int aimUpLeft = Animator.StringToHash("aimUpLeft");
    public static int aimRight = Animator.StringToHash("aimRight");
    public static int aimLeft = Animator.StringToHash("aimLeft");
    public static int isIdle = Animator.StringToHash("isIdle");
    public static int isMoving = Animator.StringToHash("isMoving");
    public static int rollUp = Animator.StringToHash("rollUp");
    public static int rollRight = Animator.StringToHash("rollRight");
    public static int rollLeft = Animator.StringToHash("rollLeft");
    public static int rollDown = Animator.StringToHash("rollDown");
    public static float baseSpeedForPlayerAnimations = 8f;

    public static int open = Animator.StringToHash("open");

    #endregion

    #region 게임오브젝트 태그
    public const string playerTag = "Player";
    public const string playerWeapon = "playerWeapon";
    #endregion

    #region 발포
    public const float useAimAngleDistance = 3.5f;
    #endregion

    #region AStar알고리즘 파라미터
    public const int defaultAstarMovementPenalty = 40;
    public const int preferredPathAStarMovementPenalty = 1;
    public const float playerMoveDistanceToRebuildPath = 3f;
    public const float enemyPathRebuildCooldown = 2f;
    #endregion

    #region UI 파라미터
    public const float uiAmmoIconSpacing = 4f;
    #endregion
}
