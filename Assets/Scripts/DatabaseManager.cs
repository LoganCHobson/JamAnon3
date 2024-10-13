using Firebase.Database;
using System;
using System.Collections;
using UnityEngine;
using TMPro;

public class DatabaseManager : MonoBehaviour
{
    //Idk whats going on in this script so dont ask. Actually I sorta do but still dont ask.


    public TMP_InputField nameInput;
    public int playerScore;
    public TMP_Text scoreText;
    private string userID;
    private DatabaseReference dbReference;
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
            CreateUser(nameInput.text, ScoreManager.instance.score);
            dbReference.Child("users").Child(userID).Child("name").SetValueAsync(nameInput.text);
            dbReference.Child("users").Child(userID).Child("score").SetValueAsync(ScoreManager.instance.score);
            nameInput.text = "";
        }
        catch
        {
            Debug.Log("Firebase aint workin.");
        }
        
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
    

