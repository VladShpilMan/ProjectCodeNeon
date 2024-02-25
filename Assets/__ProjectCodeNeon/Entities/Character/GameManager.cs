using __ProjectCodeNeon.Entities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace __ProjectCodeNeon.Entities
{
    public class GameManager : MonoBehaviour
    {
        private static GameManager instance;

        public static GameManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<GameManager>();

                    if (instance == null)
                    {
                        GameObject obj = new GameObject("GameManager");
                        instance = obj.AddComponent<GameManager>();
                    }
                }

                return instance;
            }
        }

        public int EnemiesCount;
        public GameObject AbilityChoose;
        public GameObject CharacterObject;
        
        public void AddEnemy()
        {
            EnemiesCount++;
        }
        
        public void RemoveEnemy()
        {
            EnemiesCount--;
            
            if(EnemiesCount <= 0)
            {
                CharacterObject.SetActive(false);
                AbilityChoose.SetActive(true);
            }
        }

        public void SetPlayer(CharacterGameController player)
        {
            IInputController inputController;

            if (Application.isMobilePlatform)
            {
                inputController = new MobileInputController();
            }
            else
            {
                inputController = new MouseKeyboardInputController();
            }

            if (player != null)
            {
                player.InputController = inputController;
            }
            else
            {
                Debug.LogError("PlayerController is not set. Make sure to set it before calling SetPlayer.");
            }

            //SceneManager.LoadScene("GraphicsTest");
        }
    }
}