using System.Collections;
using UnityEngine;
using TMPro;

public class DialogTest : MonoBehaviour
{
	[SerializeField]
	private	DialogSystem dialogSystem01;

	private IEnumerator Start()
	{
		yield return new WaitUntil(()=>dialogSystem01.UpdateDialog());
	}
}

