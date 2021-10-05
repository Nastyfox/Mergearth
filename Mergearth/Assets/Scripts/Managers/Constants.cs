using UnityEngine;

public class Constants : MonoBehaviour
{
    #region Variables UI & Menus
    public const float FLIPSPEEDVALUE = 0.1f;
    public const string CHESTINTERACTION = "PRESS E TO OPEN CHEST";
    public const string NPCINTERACTION = "PRESS E TO TALK";
    public const string FIRSTLEVEL = "Level01";
    public const int MINITEMSHOP = 1;
    public const int MAXITEMSHOP = 6;
    #endregion

    #region Variables Environment
    public const int DEATHZONEDAMAGE = 10;
    public const float GRAVITYSCALE = 1;
    public const int STOPPEDTIMESCALE = 0;
    public const int NORMALTIMESCALE = 1;
    public const float MINDISTANCETOPOINT = 0.1f;
    public const float PLATFORMANGLEUP = 0;
    public const float PLATFORMANGLEDOWN = 180;
    #endregion

    #region Variables Player
    public const int PLAYERMINHEALTH = 0;
    public const int PLAYERMAXHEALTH = 100;
    public const int BASEPLAYERDAMAGE = 10;
    public const float BASEPLAYERMOVESPEED = 250;
    public const float BASEPLAYERCLIMBSPEED = 150;
    public const float BASEPLAYERJUMPFORCE = 200;
    #endregion
}
