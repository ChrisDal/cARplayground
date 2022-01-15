public static class GameVariables
{
    public static int allowedTime = 65; // in seconds 
    public static int currentTime = GameVariables.allowedTime;

    public static int nCoinsMax = 30; 
    public static int ncoins = 0;  // Created coins

    public static bool isRunning = false; 

    // Difficulty 
    public static int difficulty; 
    public static float[] percentGoodCoins = {0.8f, 0.7f, 0.6f}; 
    public static float[] spawnBallRate = {3.0f, 2.0f, 1.5f}; 

}
