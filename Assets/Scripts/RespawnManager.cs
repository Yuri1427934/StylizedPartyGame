using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnManager : MonoBehaviour
{
    public string PlayerTag="Player";
    public string respawnPointId;
    [SerializeField]
    private List<Transform> SpawnPoints=new List<Transform>();

    private void Start()
    {
        if (GameEventManager.instance) GameEventManager.instance.PlayerRespawn.AddListener(GetPlayerSpawn);
    }

    void GetPlayerSpawn(string i_id, GameObject i_player)
    {
        if (!string.IsNullOrEmpty(i_id) && respawnPointId.Equals(i_id)) SetPlayerRespawn(i_player);
    }
    public void SetPlayerRespawn(GameObject i_player)
    {
        //Set player position to random points or this obejct position
        Transform _spawnLoc = SpawnPoints.Count > 0 ? SpawnPoints[Random.Range(0, SpawnPoints.Count)] : this.transform;
        if (_spawnLoc == null)
        {
            Debug.LogError("The spawn point is null");
            return;
        }
        i_player.transform.position = _spawnLoc.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (PlayerTag.Equals(other.tag) && other.GetComponent<PlayerScript>())
        {
            other.GetComponent<PlayerScript>().SetRespawnPointId(this.respawnPointId);
        }
    }
}
