using UnityEngine;

public static class ScreenUtils
{
    #region Fields
    static float screenLeft;
    static float screenRight;
    static float screenTop;
    static float screenBottom;
    #endregion

    #region Properties
    public static float ScreenLeft
    {
        get { return screenLeft; }
    }
    public static float ScreenRight
    {
        get { return screenRight; }
    }
    public static float ScreenTop
    {
        get { return screenTop; }
    }
    public static float ScreenBottom
    {
        get { return screenBottom; }
    }

    #endregion

    #region Methods
    public static void Initialize()
    {
        Vector3 lowerLeftCornerScreen = new Vector3(0, 0, 0);
        Vector3 upperRightCornerScreen = new Vector3(Screen.width, Screen.height, 0);

        Vector3 lowerLeftCornerWorld = Camera.main.ScreenToWorldPoint(lowerLeftCornerScreen);
        Vector3 upperRightCornerWorld = Camera.main.ScreenToWorldPoint(upperRightCornerScreen);

        screenLeft = lowerLeftCornerWorld.x;
        screenRight = upperRightCornerWorld.x;
        screenTop = upperRightCornerWorld.y;
        screenBottom = lowerLeftCornerWorld.y;

    }

    public static void ShowBoundingBox()
    {
        Debug.Log("Lower Left: " + ScreenLeft + ", " + ScreenBottom);
        Debug.Log("Upper Right: " + ScreenRight + ", " + ScreenTop);
    }
    #endregion
}
