using System.IO;
using TMPro;
using UnityEngine;

namespace DefaultNamespace.Scenes
{
    public class SelectableFile : MonoBehaviour
    {

        public string MyFile;

        public TMP_Text text;        
        
        public void Setup(string file)
        {
            MyFile = file;
            text.text = new FileInfo(file).Name;
        }

        public void OnClick()
        {
            this.gameObject.GetComponentInParent<FileSelector>().setCurrent(this);

        }
        
    }
}