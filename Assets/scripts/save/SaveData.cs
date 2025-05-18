using System.Collections.Generic;

[System.Serializable]
public class SaveData
{
    public int scoreTotal;

    public int vidaMaxima;

    public int armaAtual;
    public List<int> armasDesbloqueadas;

    public int personagemAtual;
    public List<int> personagensDesbloqueados;

    public Dictionary<string, RunData> historic = new();

    [System.Serializable]
    public class RunData
    {
        public int score;
        public int time;
        public int meters;
        public int obstacles;
    }
}

