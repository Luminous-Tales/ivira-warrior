using System.IO;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    private static readonly string _fileName = "gamedata.json";
    private static SaveData _cachedData;
    private const int MaxExtraLives = 3;

    private static string GetSavePath()
    {
        return Path.Combine(Application.persistentDataPath, _fileName);
    }

    public static SaveData GetData()
    {
        _cachedData ??= Load();
        return _cachedData;
    }

    public static void SaveScore(int score)
    {
        var data = GetData();
        data.currentScore = score;
        if (score > data.bestScore)
            data.bestScore = score;
        Save(data);
    }

    public static bool TryBuyItem(int itemId, int price)
    {
        var data = GetData();

        if (data.unlockedItems.Contains(itemId) || data.currentScore < price)
            return false;

        data.unlockedItems.Add(itemId);
        data.currentScore -= price;
        Save(data);
        return true;
    }

    public static bool TryBuyLife(int price)
    {
        var data = GetData();

        if (data.currentScore < price)
            return false;

        if (data.extraLives >= MaxExtraLives)
            return false;

        data.currentScore -= price;
        data.extraLives++;
        Save(data);
        return true;
    }


    public static int GetExtraLives() => GetData().extraLives;

    public static void SetEquippedWeapon(int weaponId)
    {
        var data = GetData();
        data.equippedWeaponId = weaponId;
        Save(data);
    }

    public static int GetEquippedWeapon()
    {
        return GetData().equippedWeaponId;
    }

    public static bool IsItemUnlocked(int itemId)
    {
        return GetData().unlockedItems.Contains(itemId);
    }

    private static void Save(SaveData data)
    {
        try
        {
            string json = JsonUtility.ToJson(data, true);
            File.WriteAllText(GetSavePath(), json);
            _cachedData = data; // Atualiza cache
            Debug.Log($"Dados salvos: Score={data.currentScore}, Best={data.bestScore}");
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Erro ao salvar: {e.Message}");
        }
    }

    private static SaveData Load()
    {
        string path = GetSavePath();
        if (File.Exists(path))
        {
            try
            {
                string json = File.ReadAllText(path);
                return JsonUtility.FromJson<SaveData>(json);
            }
            catch (System.Exception e)
            {
                Debug.LogError($"Erro ao carregar save: {e.Message}");
            }
        }

        Debug.Log("Criando novo arquivo de save");
        return new SaveData();
    }

    public static void DeleteSave()
    {
        string path = GetSavePath();
        if (File.Exists(path))
        {
            File.Delete(path);
            _cachedData = null;
            Debug.Log("Save deletado");
        }
    }
}