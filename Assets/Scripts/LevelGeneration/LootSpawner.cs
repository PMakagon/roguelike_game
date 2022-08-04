using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

namespace LevelGeneration
{
    public class LootSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject questItem;


        public void SpawnQuestItem(List<Room> spawnedRooms)
        {
            var randRotation = Random.rotation;
            
            var random = Random.Range(1, spawnedRooms.Count);
            var newQuestItem = Instantiate(questItem, spawnedRooms[random].LightExtended[0].transform.position - Vector3.up*2,Random.rotation);
            var direction = spawnedRooms[random - 1].transform.position;
            newQuestItem.GetComponent<Rigidbody>().AddForce(Vector3.left*50 - direction,ForceMode.Impulse);
            Debug.Log("QUEST ITEM SPAWNED AT " + spawnedRooms[random].gameObject.gameObject);
        }

    }
}