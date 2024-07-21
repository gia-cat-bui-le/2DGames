using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class MainMenuManager : MonoBehaviour
    {
        public List<GameObject> listPages;
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        public void OpenPageAtIndex(int pageIndex)
        {
            for (int i = 0; i < listPages.Count; ++i)
            {
                listPages[i].SetActive(i == pageIndex);
            }
        }
        
        
        public void QuitGame()
        {
            Application.Quit();
            Debug.Log("Quit game");
        }

        public void GoToPage(int pageIndex)
        {
            OpenPageAtIndex(pageIndex);
        }

        public void OpenLevel(string levelName)
        {
            SceneManager.LoadScene(levelName, LoadSceneMode.Single);
        }
    }
}
