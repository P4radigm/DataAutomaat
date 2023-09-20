using UnityEngine;

[System.Serializable]
public class Activity
{
    public string name;
    public Sprite visual;
    public string[] stockReceiptLines;
    public Option[] options;
    public bool multiplePossible;
}
