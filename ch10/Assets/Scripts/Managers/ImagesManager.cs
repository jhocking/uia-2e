using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class ImagesManager : MonoBehaviour, IGameManager {
	public ManagerStatus status {get; private set;}

	private NetworkService _network;

	private Texture2D _webImage;

	public void Startup(NetworkService service) {
		Debug.Log("Images manager starting...");

		_network = service;

		// any long-running startup tasks go here, and set status to 'Initializing' until those tasks are complete
		status = ManagerStatus.Started;
	}

	public void GetWebImage(Action<Texture2D> callback) {
		if (_webImage == null) {
			StartCoroutine(_network.DownloadImage((Texture2D image) => {
				_webImage = image;
				callback(_webImage);
			}));
		}
		else {
			callback(_webImage);
		}
	}
}
