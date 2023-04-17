using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace PowerPoint {
public class QueuePPT : MonoBehaviour {
    #region Variables and OnGUI

    public bool showGUI = false;
    public TextMeshProUGUI labelTmp;

    [Header("Prefabs")]
    public Character[] charactersPr;

    [Header("GUI Stuff")]
    public ButtonGUI enqueueBtn;
    public ButtonGUI dequeueBtn;
    public ButtonGUI peekBtn;
    
    [Header("Transform Points")]
    public Transform spawnPtTr;
    public Transform ridePtTr;
    public Transform[ ] queuePtTrs;
    public float timeToReach = 1;
    private int index;

    [Header("Dialogue Stuffs")]
    public GameObject soldierDialogue, firstQueueDialogue;
    
    private void OnGUI() {
        if (!showGUI) return;

        enqueueBtn = new ButtonGUI(enqueueBtn.RectSize, enqueueBtn.Title);
        dequeueBtn = new ButtonGUI(dequeueBtn.RectSize, dequeueBtn.Title);
        peekBtn = new ButtonGUI(peekBtn.RectSize, peekBtn.Title);
        
        if (enqueueBtn.Click())
            EnqueuePresentation();
        if (dequeueBtn.Click())
            DequeuePresentation();        
        if (peekBtn.Click())
            PeekCharacter();
    }

    #endregion

    private Queue<Character> charQueue = new Queue<Character>();

    void EnqueuePresentation() {
        labelTmp.text = "Enqueue";
        EnqueueCharacters();
    }

     Character GetCharacter() {
            int randIdx = Random.Range(0, charactersPr.Length);
            var character = Instantiate(charactersPr[randIdx],
                                        spawnPtTr.position,
                                        Quaternion.identity);
            return character;
     }

    // ENQUEUE PPT CODE
    async void EnqueueCharacters() {
        while (index < queuePtTrs.Length) {
            var character = GetCharacter();
            charQueue.Enqueue(character); 
            StartCoroutine(Mobilize(character, 
                       queuePtTrs[index].position)); 
            index++;
            await Task.Delay(1000);
        }
        index = 0;
        labelTmp.text = "";
    }

    IEnumerator Mobilize(Character entity, Vector3 destination, bool isRiding = false) {
        yield return new WaitForSeconds(0.5f);
        entity.ChangeAnimState(Data.RUN_ANIM);
        Vector3 lastPos = entity.transform.position;
        for (float t = 0; t < 1.0f; t += Time.deltaTime / timeToReach) {
            entity.transform.position = Vector3.Lerp(lastPos, destination, t);
            yield return new WaitForEndOfFrame();
        }
        entity.ChangeAnimState(Data.IDLE_ANIM);
        if(isRiding)
            Destroy(entity.gameObject);
    }

    // DEQUEUE PPT CODE
    async void DequeueCharacters() {
        while (charQueue.Count > 0) {
            var character = charQueue.Dequeue();
            StartCoroutine(Mobilize(character, 
                                    ridePtTr.position, 
                                    isRiding: true));
            await Task.Delay(500);
        }
        labelTmp.text = "";
    }

    void DequeuePresentation() {
        labelTmp.text = "Dequeue";
        DequeueCharacters();
    }

    // PEEK PPT CODE
    void PeekCharacter() {
        labelTmp.text = "Peek";
        var firstCharacter = charQueue.Peek();
        StartSoldierConvo();
    }

    void StartSoldierConvo() {
        StartCoroutine(Talk(soldierDialogue));
        Invoke("StartCivilianConvo", 1f);
    }

    void StartCivilianConvo() {
        StartCoroutine(Talk(firstQueueDialogue));
    }

    IEnumerator Talk(GameObject convo) {
        convo.SetActive(true);
        yield return new WaitForSeconds(2f);
        convo.SetActive(false);
    }
}
}


#region GUI Stuff

public abstract class GUILayout {
    public string Title;
    public Rect RectSize;

    public GUILayout(Rect rectSize, string title) {
        RectSize = rectSize;
        Title = title;
    }
    public virtual void Display() { }

    public virtual bool Click() {
        return default;
    }
}

[Serializable]
public class LabelGUI : GUILayout{
    public LabelGUI(Rect rectSize, string title) : base(rectSize, title) { }

    public override void Display() {
        GUI.Label(RectSize, Title);
    }
}

[Serializable]
public class ButtonGUI : GUILayout {
    public ButtonGUI(Rect rectSize, string title) : base(rectSize, title) { }

    public override bool Click() {
        return GUI.Button(RectSize, Title);
    }
}

#endregion