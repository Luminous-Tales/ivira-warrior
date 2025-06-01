using System.IO;
using UnityEngine;

[SerializeField]
public class DadosDoJogo
{
    public int scoreTotal = 0;
}

public class SaveManager : MonoBehaviour
{
    private static string _fileName = "save.json";

    private static string GetSavePath()
    {
        return Application.persistentDataPath + "/" + _fileName;
    }

    public static void SavePoints(RunData data)
    {
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(GetSavePath(), json);
        Debug.Log("Save realizado em: " + GetSavePath());
    }

    public static void Save(SaveData data)
    {
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(GetSavePath(), json);
        Debug.Log("Save realizado em: " + GetSavePath());
    }

    public static SaveData Load()
    {
        string path = GetSavePath();
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            return JsonUtility.FromJson<SaveData>(json);
        }
        else
        {
            Debug.Log("Nenhum save encontrado. Criando novo.");
            return new SaveData();
        }
    }
    public static RunData LoadPoints()
    {
        string path = GetSavePath();
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            return JsonUtility.FromJson<RunData>(json);
        }
        else
        {
            Debug.Log("Nenhum save encontrado. Criando novo.");
            return new RunData();
        }
    }

    public static void DeleteSave()
    {
        string path = GetSavePath();
        if (File.Exists(path))
            File.Delete(path);
    }

    // Métodos de compra de itens:
    public static bool ComprarArma(int id, int preco)
    {
        var data = Load();
        if (data.weaponsUnlocked.Contains(id)) return false;
        if (data.scoreTotal < preco) return false;

        data.weaponsUnlocked.Add(id);
        data.scoreTotal -= preco;
        data.currentWeapon = id;
        // marca no market
        if (id >= 0 && id < data.market.Count)
            data.market[id].unlocked = true;

        Save(data);
        return true;
    }

    public static bool ComprarVida(int preco, int quantidade)
    {
        var data = Load();
        if (data.scoreTotal < preco) return false;

        data.maxLife += quantidade;
        data.scoreTotal -= preco;
        Save(data);
        return true;
    }

    public static bool ComprarPersonagem(int id, int preco)
    {
        var data = Load();
        if (data.charactersUnlocked.Contains(id)) return false;
        if (data.scoreTotal < preco) return false;

        data.charactersUnlocked.Add(id);
        data.scoreTotal -= preco;
        data.currentCharacter = id;
        Save(data);
        return true;
    }
}