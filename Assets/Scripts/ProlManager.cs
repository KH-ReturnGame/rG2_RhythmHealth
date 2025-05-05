using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ProlManager : MonoBehaviour
{
	[SerializeField]
	private	DialogSystem[] dialogSystems;
    
	[SerializeField]
	private	TextAsset[] playRoutineFiles;
    
	public AudioClip[] playSongFiles;
    int index_STD = 0;
    public int index_PF = 0;
    public NoteVerdict noteVerdict;
    public ReadLoadGame readLoadGame;
    public GameObject readLoadGameObj;
    public GameObject Printer;
    public GameObject BackToStageButton;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private IEnumerator Start()
    {
        yield return new WaitUntil(()=>dialogSystems[index_STD].UpdateDialog());
        index_STD++;
        if(index_STD <= dialogSystems.Length && index_PF < playRoutineFiles.Length)
        {
            StartCoroutine(PlayFile(index_PF));
        }
        else
        {
            yield return null;
        }
    }

    private IEnumerator StartDialog(int i)
	{
        yield return new WaitForSeconds(2.5f);
		yield return new WaitUntil(()=>dialogSystems[i].UpdateDialog());
        index_STD++;
        if(index_STD <= dialogSystems.Length && index_PF < playRoutineFiles.Length)
        {
            readLoadGameObj.SetActive(false);
            Printer.SetActive(false);
            StartCoroutine(PlayFile(index_PF));
        }
        else
        {
            BackToStageButton.SetActive(true);
            yield return null;
        }
	}

    private IEnumerator PlayFile(int i)
	{
		yield return new WaitForSeconds(2.5f); // 여기에 실제 그 파일 로드 및 시작ㄴㄴ

        noteVerdict.currentNoteIndex = -1;
        noteVerdict.isSuccess = null;
        readLoadGame.WorkIndex = 0;
        readLoadGame.jsonFile = playRoutineFiles[i];

        readLoadGameObj.SetActive(true);

        PrintResult printResult = Printer.GetComponent<PrintResult>();
        yield return new WaitUntil(()=>printResult.UpdateResult());
        index_PF++;
        if(index_STD < dialogSystems.Length && index_PF <= playRoutineFiles.Length)
        {
            StartCoroutine(StartDialog(index_STD));
        }
        else
        {
            BackToStageButton.SetActive(true);
            yield return null;
        }
	}
     
    public void BackToStage() // 버튼에 넣을거
    {
        SceneManager.LoadScene("Stage");
    }
}
