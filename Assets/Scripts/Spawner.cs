using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

internal class Spawner
{
    [ServerCallback]
    internal static void InitialSpawn(Scene scene)
    {
        Debug.Log("Spawner InitialSpawn");
        for (int i = 0; i < 10; i++)
            SpawnReward(scene);
    }

    [ServerCallback]
    internal static void SpawnReward(Scene scene)
    {
        Debug.Log("Spawner SpawnReward"+scene.name);
        Vector3 spawnPosition = new Vector3(Random.Range(-19, 20), 1, Random.Range(-19, 20));
        GameObject reward = Object.Instantiate(((MultiSceneNetManager)NetworkManager.singleton).rewardPrefab, spawnPosition, Quaternion.identity);
        SceneManager.MoveGameObjectToScene(reward, scene);
        NetworkServer.Spawn(reward);
    }
}
