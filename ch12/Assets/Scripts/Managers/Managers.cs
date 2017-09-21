using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(DataManager))]
[RequireComponent(typeof(PlayerManager))]
[RequireComponent(typeof(InventoryManager))]
[RequireComponent(typeof(MissionManager))]

public class Managers : MonoBehaviour {
	public static DataManager Data {get; private set;}
	public static PlayerManager Player {get; private set;}
	public static InventoryManager Inventory {get; private set;}
	public static MissionManager Mission {get; private set;}

	private List<IGameManager> _startSequence;
	
	void Awake() {
		DontDestroyOnLoad(gameObject);

		Data = GetComponent<DataManager>();
		Player = GetComponent<PlayerManager>();
		Inventory = GetComponent<InventoryManager>();
		Mission = GetComponent<MissionManager>();

		_startSequence = new List<IGameManager>();
		_startSequence.Add(Player);
		_startSequence.Add(Inventory);
		_startSequence.Add(Mission);
		_startSequence.Add(Data);

		StartCoroutine(StartupManagers());
	}

	private IEnumerator StartupManagers() {
		NetworkService network = new NetworkService();
		
		foreach (IGameManager manager in _startSequence) {
			manager.Startup(network);
		}

		yield return null;

		int numModules = _startSequence.Count;
		int numReady = 0;
		
		while (numReady < numModules) {
			int lastReady = numReady;
			numReady = 0;
			
			foreach (IGameManager manager in _startSequence) {
				if (manager.status == ManagerStatus.Started) {
					numReady++;
				}
			}
			
			if (numReady > lastReady) {
				Debug.Log("Progress: " + numReady + "/" + numModules);
				Messenger<int, int>.Broadcast(StartupEvent.MANAGERS_PROGRESS, numReady, numModules);
			}
			
			yield return null;
		}

		Debug.Log("All managers started up");
		Messenger.Broadcast(StartupEvent.MANAGERS_STARTED);
	}
}
