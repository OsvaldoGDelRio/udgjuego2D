using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Firestore;
using Firebase.Extensions;

public class Consumer : MonoBehaviour
{
    private  FirebaseApp _app;
    // Start is called before the first frame update
    void Start()
    {
        Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
        var dependencyStatus = task.Result;
        if (dependencyStatus == Firebase.DependencyStatus.Available) {
            // Create and hold a reference to your FirebaseApp,
            // where app is a Firebase.FirebaseApp property of your application class.
            _app = Firebase.FirebaseApp.DefaultInstance;

            Login();

            // Set a flag here to indicate whether Firebase is ready to use by your app.
        } else {
            UnityEngine.Debug.LogError(System.String.Format(
            "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
            // Firebase Unity SDK is not safe to use here.
        }
        });
    }

    private void AddData()
    {
        FirebaseFirestore db = FirebaseFirestore.DefaultInstance;
        Firebase.Auth.FirebaseAuth auth = Firebase.Auth.FirebaseAuth.DefaultInstance;

        DocumentReference docRef = db.Collection("users").Document(auth.CurrentUser.UserId);
        Dictionary<string, object> user = new Dictionary<string, object>
        {
                { "First", "Ada" },
                { "Last", "Lovelace" },
                { "Born", 1815 },
        };
        docRef.SetAsync(user).ContinueWithOnMainThread(task => {
                Debug.Log("Added data to the alovelace document in the users collection.");
                
                
                GetData();
        });
    }

    private void GetData()
    {
        FirebaseFirestore db = FirebaseFirestore.DefaultInstance;

        CollectionReference usersRef = db.Collection("users");
        usersRef.GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
            QuerySnapshot snapshot = task.Result;
            foreach (DocumentSnapshot document in snapshot.Documents)
            {
                Debug.Log($"User: {document.Id}");
                Dictionary<string, object> documentDictionary = document.ToDictionary();
                Debug.Log($"First: {documentDictionary["First"]}");
                if (documentDictionary.ContainsKey("Middle"))
                {
                    Debug.Log($"Middle: {documentDictionary["Middle"]}");
                }

                Debug.Log($"Last: {documentDictionary["Last"]}");
                Debug.Log($"Born: {documentDictionary["Born"]}");
            }

            Debug.Log("Read all data from the users collection.");
        });
    }

    private void Login()
    {
        Firebase.Auth.FirebaseAuth auth = Firebase.Auth.FirebaseAuth.DefaultInstance;

        if(auth.CurrentUser != null)
        {
            Debug.Log("Already logged in");
            AddData();
            return;
        }

        auth.SignInAnonymouslyAsync().ContinueWith(task => {
        if (task.IsCanceled) {
            Debug.LogError("SignInAnonymouslyAsync was canceled.");
            return;
        }
        if (task.IsFaulted) {
            Debug.LogError("SignInAnonymouslyAsync encountered an error: " + task.Exception);
            return;
        }

        Firebase.Auth.FirebaseUser newUser = task.Result;
        Debug.LogFormat("User signed in successfully: {0} ({1})",
            newUser.DisplayName, newUser.UserId);
        });

        AddData();
    }
}
