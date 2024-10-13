using Firebase.Database;
using System;
using System.Collections;
using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class DatabaseManager : MonoBehaviour
{
    //Idk whats going on in this script so dont ask. Actually I sorta do but still dont ask.


    public TMP_InputField nameInput;
    public int playerScore;
    public TMP_Text scoreText;
    private string userID;
    private DatabaseReference dbReference;

    private List<string> bannedWords = new List<string> { "ass", "cum", "fag", "gay", "jew", "sex", "dck", "dic", "cok",
    "kkk", "tit", "pns", "vag", "fck", "nig", "ngr", "jiz", "wtf", "fgt",
    "phk", "sht", "cnt", "bch", "dyk", "fuk", "fux", "fuc", "pis", "pus",
    "coc", "jzz", "prn", "fap", "fuq", "jew", "cvm", "kys"}; //Yup. Theres bad people out there.


    private void Start()
    {
        userID = Guid.NewGuid().ToString();
        dbReference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    private void Update()
    {
        scoreText.text = "Score: " + ScoreManager.instance.score.ToString("N0");
    }


    private void CreateUser(string name, int score)
    {
        User newUser = new User(name, score);
        string json = JsonUtility.ToJson(newUser);

        dbReference.Child("users").Child(userID).SetRawJsonValueAsync(json);
    }

    

    public void SubmitScore()
    {
        try
        {
            if(IsValidName(nameInput.text))
            {
                CreateUser(nameInput.text, ScoreManager.instance.score);
                dbReference.Child("users").Child(userID).Child("name").SetValueAsync(nameInput.text);
                dbReference.Child("users").Child(userID).Child("score").SetValueAsync(ScoreManager.instance.score);
                nameInput.text = "";
            }
            else
            {
                nameInput.text = "***";
            }
           
        }
        catch
        {
            nameInput.text = "Err";
            Debug.Log("Firebase aint workin.");
        }
        
    }

    public bool IsValidName(string playerName)
    {
        string sanitizedInput = playerName.ToLower().Trim();
        
        if (!Regex.IsMatch(sanitizedInput, @"^[a-zA-Z]+$"))
        {
            return false; 
        }
        
        foreach (string bannedWord in bannedWords)
        {
            if (sanitizedInput.Contains(bannedWord))
            {
                return false; 
            }
        }

        return true; 
    }


}

    public class User
    {
        public string name;
        public int score;

        public User(string name, int score)
        {
            this.name = name;
            this.score = score;
        }
    }
    

