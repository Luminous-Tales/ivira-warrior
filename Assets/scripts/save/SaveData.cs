using System.Collections.Generic;

[System.Serializable]
public class RunData
{
    public int? score;
    public int? time;
    public int? meters;
    public int? obstacles;
}

[System.Serializable]
public class SaveData
{
    public int scoreTotal;
    public int maxLife;

    public int currentWeapon;
    public List<int> weaponsUnlocked;

    public int currentCharacter;
    public List<int> charactersUnlocked;

    public List<MarketItem> market;
    public List<RunData> historic;

    // Inicializa valores padrão
    public SaveData()
    {
        scoreTotal = 0;
        maxLife = 3;
        currentWeapon = 0;
        weaponsUnlocked = new List<int> { 0 };
        currentCharacter = 0;
        charactersUnlocked = new List<int> { 0 };
        market = new List<MarketItem>();
        historic = new List<RunData>();
    }
}