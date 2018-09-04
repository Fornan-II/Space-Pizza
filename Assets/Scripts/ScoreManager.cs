using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class ScoreManager {

    private static string _scoreFilePath = Application.persistentDataPath + "/score.binary";
    private static ScoreHolder score = new ScoreHolder();
    public static int HighScore
    {
        get
        {
            return score.HighScore;
        }
        set
        {
            score.HighScore = value;
        }
    }

    public static void SaveScore()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream scoreFile = File.Create(_scoreFilePath);

        formatter.Serialize(scoreFile, score);

        scoreFile.Close();
    }

    public static bool LoadScore()
    {
        if (!File.Exists(_scoreFilePath)) { return false; }

        BinaryFormatter formatter = new BinaryFormatter();
        FileStream scoreFile = File.Open(_scoreFilePath, FileMode.Open);

        score = (ScoreHolder)formatter.Deserialize(scoreFile);
        scoreFile.Close();
        return true;
    }
}
