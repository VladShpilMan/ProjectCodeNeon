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
        public GameObject[] enemyPrefabs;
        public int minEnemies = 6;
        public int maxEnemies = 10;
        public void AddEnemy()
        {
            EnemiesCount++;
            Debug.Log(EnemiesCount);
        }
        
        public void RemoveEnemy()
        {
            EnemiesCount--;
            Debug.Log(EnemiesCount);
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

        void Start()
        {
            SpawnEnemies();
        }

        void SpawnEnemies()
        {
            int numberOfEnemies = Random.Range(minEnemies, maxEnemies + 1);

            for (int i = 0; i < numberOfEnemies; i++)
            {
                GameObject enemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];

                Vector3 spawnPosition = new Vector3(Random.Range(-10f, 10f), 0f, Random.Range(-10f, 10f));

                Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
            }
        }

    }
}