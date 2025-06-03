using System.Collections.Generic;

[System.Serializable]
public class SaveData
{
    public int currentScore;
    public int bestScore;
    public List<int> unlockedItems;
    public int extraLives;
    public int equippedWeaponId;

    public SaveData()
    {
        equippedWeaponId = 0;
        extraLives = 0;
        currentScore = 0;
        bestScore = 0;
        unlockedItems = new List<int>();
    }
}