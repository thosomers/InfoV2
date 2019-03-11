using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using TMPro;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class EditorPanel : MonoBehaviour
{

    public TMP_InputField TextEditor;
    public TMP_InputField NameField;

    public FileSelector selector;
    
    public string currentFile;


    public void Open(string currentMyFile)
    {
        currentFile = currentMyFile;
        
        NameField.text = new FileInfo(currentFile).Name;

        TextEditor.text = File.ReadAllText(currentFile);
        
        Debug.Log(new FileInfo(currentFile).DirectoryName);
    }

    public void OpenInEditor()
    {
        new Process
        {
            StartInfo = new ProcessStartInfo(currentFile)
            {
                UseShellExecute = true
            }
        }.Start();
    }

    public void Save()
    {
        File.Delete(currentFile);
        
        File.WriteAllText(Path.Combine(new FileInfo(currentFile).DirectoryName,NameField.text),TextEditor.text );
        selector.Refresh();
        
        
        
    }

    public void New()
    {
        var UnnamedName = "unnamed.lua";
        var UnnamedPath = Path.Combine(new FileInfo(currentFile).DirectoryName, UnnamedName);

        currentFile = UnnamedPath;
        
        TextEditor.text = @"
--Write Lua Text here



--The function that is run each turn
function run()


end
";
        
        File.WriteAllText(UnnamedPath,TextEditor.text );
        NameField.text = UnnamedName;
        
        selector.Refresh();
    }
    
    
    
    
    
    
    
    
    
    
    
    
    
    
}
