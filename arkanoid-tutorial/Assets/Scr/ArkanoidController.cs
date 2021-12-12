 using System.Collections;
 using System.Collections.Generic;
 using UnityEngine;
 
 public class ArkanoidController : MonoBehaviour
 {
     [SerializeField]
     private GridController _gridController;
 
     [Space(20)]
     [SerializeField]
     private List<LevelData> _levels = new List<LevelData>();
 
     private int _currentLevel = 0;
     
     private void Update()
     {
         if (Input.GetKeyDown(KeyCode.Space))
         {
            InitGame();
         }
     }
     
     private void InitGame()
     {
         _currentLevel = 0;
         _gridController.BuildGrid(_levels[0]);
     }
 }