using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    Queue<string> quedSentences;
    string currentSentence;
    UIController uiController;

    private void Start() {
        quedSentences = new Queue<string>();
        uiController = GetComponent<UIController>();
    }

    public void startDialogue(Dialogue dialogue){
        //Pause Game
        Time.timeScale = 0;
        uiController.dialogueName = dialogue.NPCname;
        //init Dialogue;
        quedSentences.Clear();
        foreach (string sentence in dialogue.sentences)
        {
            quedSentences.Enqueue(sentence);
        }
        next();
        uiController.startDialogue();
    }


    public void next(){
        if(quedSentences.Count != 0){
            uiController.dialogue = quedSentences.Dequeue();
        }
        else endDialogue();
    }

    void endDialogue(){
        uiController.endDialogue();
        Time.timeScale = 1;
    }
}
