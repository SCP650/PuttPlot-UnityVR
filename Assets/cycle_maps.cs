using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cycle_maps : MonoBehaviour
{
    [SerializeField]
    FrameRef current_frame;

    [SerializeField]
    List<UnitEvent> boards;

    [SerializeField]
    IntEvent holeChoosen;

    [SerializeField]
    UnitEvent dialogue_proceed;

    private int active_board = -1;
    // Start is called before the first frame update
    void Start()
    {

        for (int i = 0; i < boards.Count; i++)
        {
            var j = i;
            boards[i].AddListener(_ => setboard(j));
        }
        holeChoosen.AddListener(ClearBoard);
        dialogue_proceed.AddListener(_ => ClearBoard(-1));

    }

    void ClearBoard(int holeNum)
    {
        for(int i = 0; i < transform.GetChild(active_board).childCount;i++)
        {
            if(i != holeNum)   
                transform.GetChild(active_board).GetChild(i).GetComponent<TMPro.TMP_Text>().text = "";
        }
    }

    

    void setboard(int i)
    {
        if (i >= transform.childCount) return;
        active_board = i;
        for(int j = 0; j < transform.childCount; j++)
        {
            transform.GetChild(j).GetComponent<UnityEngine.UI.Image>().enabled = (i == j);    
            foreach(Transform t in transform.GetChild(j))
            {
                t.GetComponent<TMPro.TMP_Text>().text = "";
            }
        }
        for(int l = 0; l < current_frame.val.visible_choices.Count;l++)
        {
            print($"i: {i} j {l}");
            transform.GetChild(i).GetChild(l).GetComponent<TMPro.TMP_Text>().text = current_frame.val.visible_choices[l].three_words;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
