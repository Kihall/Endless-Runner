using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    private static LevelGenerator Instance;

    private const float PLAYER_DISTANCE_SPAWN_LEVEL_PART = 200f;

    [SerializeField] private Transform pfTestingPlatform;
    [SerializeField] private Transform levelPart_Start;
    [SerializeField] private List<Transform> levelPartEasyList;
    [SerializeField] private List<Transform> levelPartMediumList;
    [SerializeField] private List<Transform> levelPartHardList;
    [SerializeField] private List<Transform> levelPartImpossibleList;
    [SerializeField] private PlayerController player;

    private enum Difficulty
    {
        Easy,
        Medium,
        Hard,
        Impossible
    }

    private Vector3 lastEndPosition;
    private int levelPartsSpawned;

    private void Awake()
    {
        Instance = this;
        lastEndPosition = levelPart_Start.Find("EndPosition").position;

        if (pfTestingPlatform != null)
        {
            Debug.Log("Using Debug Testing Platform!");
        }

        int startingSpawnLevelParts = 5;
        for (int i = 0; i < startingSpawnLevelParts; i++)
        {
            SpawnLevelPart();
        }
    }

    private void Update()
    {
        if (Vector3.Distance(player.GetPosition(), lastEndPosition) < PLAYER_DISTANCE_SPAWN_LEVEL_PART)
        {
            // Spawn another level part
            SpawnLevelPart();
        }
    }

    private void SpawnLevelPart()
    {
        List<Transform> difficultyLevelPartList;
        switch (GetDifficulty())
        {
            default:
            case Difficulty.Easy: difficultyLevelPartList = levelPartEasyList; break;
            case Difficulty.Medium: difficultyLevelPartList = levelPartMediumList; break;
            case Difficulty.Hard: difficultyLevelPartList = levelPartHardList; break;
            case Difficulty.Impossible: difficultyLevelPartList = levelPartImpossibleList; break;
        }

        Transform chosenLevelPart = difficultyLevelPartList[Random.Range(0, difficultyLevelPartList.Count)];

        if (pfTestingPlatform != null)
        {
            chosenLevelPart = pfTestingPlatform;
        }

        Transform lastLevelPartTransform = SpawnLevelPart(chosenLevelPart, lastEndPosition);
        lastEndPosition = lastLevelPartTransform.Find("EndPosition").position;
        levelPartsSpawned++;
    }

    private Transform SpawnLevelPart(Transform levelPart, Vector3 spawnPosition)
    {
        Transform levelPartTransform = Instantiate(levelPart, spawnPosition, Quaternion.identity);
        return levelPartTransform;
    }

    private Difficulty GetDifficulty()
    {
        if (levelPartsSpawned >= 19) return Difficulty.Impossible;
        if (levelPartsSpawned >= 14) return Difficulty.Hard;
        if (levelPartsSpawned >= 7) return Difficulty.Medium;
        return Difficulty.Easy;
    }

    public static int GetLevelPartsSpawned()
    {
        return Instance.levelPartsSpawned;
    }
}
