using UnityEngine;
using System;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;

public static class GameData
{
    public static Firebase.Auth.FirebaseAuth Auth { get; set; }
    public static Firebase.Auth.FirebaseUser User { get; set; }
}