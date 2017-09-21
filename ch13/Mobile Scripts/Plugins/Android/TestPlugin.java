package com.testcompany.testplugin;

/* an approach that creates an Activity to list in the android.manifest
import android.app.Activity;
import android.os.Bundle;
import android.util.Log;

import com.unity3d.player.UnityPlayer;

public class TestPlugin extends Activity {
	protected static TestPlugin _instance;
	
	public static void startActivity() {
		Activity unityActivity = UnityPlayer.currentActivity;
		Intent intent = new Intent(unityActivity, TestPlugin.class);
		unityActivity.startActivity(intent);
	}
	
	protected void onCreate(Bundle savedInstanceState) {
		Log.d("TestPlugin", "onCreate called");		// print debug message to logcat
		super.onCreate(savedInstanceState);
		
		_instance = this;
	}
}
*/

public class TestPlugin {
	private static int number = 0;
	
	public static int getNumber() {
		number++;
		return number;
	}
	
	public static String getString(String message) {
		return message.toLowerCase();
	}
}