using System.Collections;
using UnityEngine;
using TMPro;

public class DialogTest : MonoBehaviour
{
	[SerializeField]
	private	DialogSystem dialogSystem01;
	[SerializeField]
	private	DialogSystem dialogSystem02;
	[SerializeField]
	private	DialogSystem dialogSystem03;
	[SerializeField]
	private	DialogSystem dialogSystem04;
	[SerializeField]
	private	DialogSystem dialogSystem05;
	public int dialogIndex = 0;

	private IEnumerator Start()
	{
		dialogIndex = PlayerPrefs.GetInt("DialogIndex");
		switch(dialogIndex)
		{
			case 0:
				yield return new WaitUntil(()=>dialogSystem01.UpdateDialog());
				break;
			case 1:
				yield return new WaitUntil(()=>dialogSystem02.UpdateDialog());
				break;
			case 2:
				yield return new WaitUntil(()=>dialogSystem03.UpdateDialog());
				break;
			case 3:
				yield return new WaitUntil(()=>dialogSystem04.UpdateDialog());
				break;
			case 4:
				yield return new WaitUntil(()=>dialogSystem05.UpdateDialog());
				break;
			default:
				yield return null;
				break;
		}
	}
}

