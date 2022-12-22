using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenShot : MonoBehaviour
{

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.A))
		{
			ScreenCapture.CaptureScreenshot("Assets/ScreenShot.png");
		}
	}
}