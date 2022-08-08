using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MapManager : MonoBehaviour
{
	[SerializeField]
	[Tooltip("navmesh가 bake될 땅에 해당되는 게임오브젝트 리스트입니다.")]
	private GameObject[] grounds;

	[SerializeField]
	[Tooltip("생성될 장애물의 부모가 될 게임오브젝트입니다.")]
	public Transform obstacle_parent;
    [SerializeField]
    [Tooltip("장애물이 되어 avatar의 진로를 방해할 Obstacle 클래스 리스트입니다.")]
    private  GameObject[] obstacle;

	[SerializeField]
	[Tooltip("플레이어에 해당하는 게임오브젝트입니다.")]
	private GameObject player;

	[SerializeField]
	[Tooltip("플레이어를 쫒아 다닐 아바타 오브젝트 입니다.")]
	private GameObject avatar;




	private void Awake()
	{
		GenerateNavObstacle();
		GenerateObstacleManager();
		GenerateNavmesh();
		GenerateNavMeshAgent();
	}

	private void GenerateNavmesh()
	{
		player.SetActive(false);
		avatar.SetActive(false);
		var col_objects = GameObject.FindObjectsOfType<Collider>();
		foreach(var col in col_objects)
        {
			col.gameObject.SetActive(false);
        }
		var rig_objects = GameObject.FindObjectsOfType<Rigidbody>();
		foreach(var rig in rig_objects)
        {
			rig.gameObject.SetActive(false);
        }
		foreach(var gd in grounds)
        {
			gd.AddComponent<NavMeshSurface>();
			var navmesh = gd.GetComponent<NavMeshSurface>();
			navmesh.RemoveData();
			navmesh.BuildNavMesh();
		}
		
		
		foreach (var col in col_objects)
		{
			col.gameObject.SetActive(true);
		}
		
		foreach (var rig in rig_objects)
		{
			rig.gameObject.SetActive(true);
		}

		player.SetActive(true);
		avatar.SetActive(true);
	}

	private void GenerateNavObstacle()
    {
		
		foreach (var fu in obstacle)
        {
			//var created_obs = Instantiate(fu, fu.transform.position, fu.transform.rotation);
			var obstacle_col = fu.AddComponent<BoxCollider>();
			obstacle_col.isTrigger = true;

			var obstacle_nav = fu.AddComponent<NavMeshObstacle>();
			obstacle_nav.carving = true;
			obstacle_nav.carvingMoveThreshold = 0.1f;
			obstacle_nav.carvingTimeToStationary = 0.1f;
			obstacle_nav.carveOnlyStationary = false;
		}

	}

	private void GenerateObstacleManager()
	{
		var obstacleManager = new GameObject("ObstacleManager");
		obstacleManager.transform.SetParent(this.transform);
		obstacleManager.AddComponent<FurnitureManager>();

	}

	private void GenerateNavMeshAgent()
    {
		avatar.AddComponent<NavMeshAgent>();
		var object_AI_chase = new GameObject("AI_chase");
		object_AI_chase.SetActive(false);
		object_AI_chase.transform.SetParent(this.transform);
		var script_AI_chase = object_AI_chase.AddComponent<AI_chase>();
		script_AI_chase.npc_trans = avatar.transform;
		script_AI_chase.target = player;
		script_AI_chase.max_speed = 12;
		script_AI_chase.acceleration = 100;
		script_AI_chase.extraRotationSpeed = 10;
		object_AI_chase.SetActive(true);

	}
}