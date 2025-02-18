using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ProlManager : MonoBehaviour
{
    
	[SerializeField]
	private	DialogSystem[] dialogSystems;
    
	[SerializeField]
	private	GameObject[] playFiles;
    int index_STD = 0;
    int index_PF = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartDialog(index_STD);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator StartDialog(int i)
	{
		yield return new WaitUntil(()=>dialogSystems[i].UpdateDialog());
        index_STD++;
        if(index_STD <= dialogSystems.Length && index_PF < playFiles.Length)
        {
            PlayFile(index_PF);
        }
        else
        {
            yield return null;
        }
	}

    private IEnumerator PlayFile(int i)
	{
		yield return new WaitForSeconds(5f); // 여기에 실제 그 파일 로드 및 시작ㄴㄴ
        index_PF++;
        if(index_STD < dialogSystems.Length && index_PF <= playFiles.Length)
        {
            StartDialog(index_STD);
        }
        else
        {
            yield return null;
        }
	}
}
