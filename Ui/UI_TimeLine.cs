using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[System.Serializable]
public struct TimeLineSlotData
{
    public int id;
    public Sprite sprite;
    public Material material;
}
public struct SlotUnit
{
    public int instanceID;
    public int agility;
    public TimeLineSlotData data;
}

public class UI_TimeLine : MonoBehaviour
{

    [SerializeField]
    private Sprite emptySprite;

    [SerializeField]
    private Transform timeline;

    private UI_TimeLineSlot[] slot;
 
    [SerializeField]
    private TimeLineSlotData[] slotData;

    List<SlotUnit> sequenceData = new List<SlotUnit>();

    [SerializeField]
    private Color playerColor;
    [SerializeField]
    private Color enemyColor;

    private int sequceMaxSize;
    private int currentSequce;
    private int tailCount;
    //acclocate
    private void Awake()
    {
        slot = timeline.GetComponentsInChildren<UI_TimeLineSlot>();
    }

    private void Start()
    {
        CallBack.turn.end_listener += TurnEnd;
        UI_TimeLineSlot.enemyColor = enemyColor;
        UI_TimeLineSlot.playerColor = playerColor;
    }

    private void SetSlot(int skipCount)
    {
        if (sequenceData.Count == 0) return;
        for(int i = skipCount; i < slot.Length;)
        {
            for(int j = 0; j < sequenceData.Count; j++)
            {
                if (i >= slot.Length) return;
                slot[i].SetSlot(sequenceData[j]);
                tailCount = j;
                i++;
            }
        }
    }
    private void TurnEnd(float d)
    {
        for(int i = 0; i < slot.Length-1; i++)
        {
            slot[i].SetSlot(slot[i + 1].GetSlot());
        }
        currentSequce = currentSequce >= sequceMaxSize - 1 ? 0 : currentSequce + 1;
        tailCount = tailCount >= sequenceData.Count - 1 ? 0 : tailCount + 1;
        slot[slot.Length - 1].SetSlot(sequenceData[tailCount]);
    }
    public void AddUnit(GameObject obj)
    {
        for (int i = 0; i < sequenceData.Count; i++)
        {
            if (sequenceData[i].agility < obj.GetComponent<CharacterModel>().getAgility())
            {
                sequenceData.Insert(i, InstanceSlotUnit(obj));             
                break;
            }
        }
        SetSlot(sequceMaxSize - currentSequce);
        sequceMaxSize++;
    }
    public void DeleteUnit(int instanceID)
    {
        for(int i = 0; i < sequenceData.Count; i++)
        {
            if (sequenceData[i].instanceID.Equals(instanceID))
            {
                sequenceData.RemoveAt(i);       
                break;
            }
        }
        int skipCount = 0;
        for(int i = 0; i < sequceMaxSize - currentSequce; i++)
        {
            if (skipCount >= slot.Length) break;
            if (slot[skipCount].GetSlot().instanceID.Equals(instanceID))
            {
                slot[skipCount].SetSlot(slot[skipCount + 1].GetSlot());
            }
            else
            {
                skipCount++;
            }
        }
        SetSlot(skipCount);
        sequceMaxSize--;

    }

    public void TimeLineCreate(List<GameObject> sequence)
    {
        sequenceData = new List<SlotUnit>();
        for (int i = 0; i < sequence.Count; i++)
        {
            SlotUnit unit = InstanceSlotUnit(sequence[i]);
            sequenceData.Add(unit);
        }
        sequceMaxSize = sequenceData.Count;
        currentSequce = 0;
        SetSlot(0);
    }
    private SlotUnit InstanceSlotUnit(GameObject obj)
    {
        CharacterModel model = obj.GetComponent<CharacterModel>();
        SlotUnit slot = new SlotUnit();
        slot.agility = model.getAgility();
        slot.instanceID = obj.GetInstanceID();
        slot.data = SlotDataFind(model.getId());
        return slot;
    }
    private TimeLineSlotData SlotDataFind(int id)
    {
        for (int i = 0; i < slotData.Length; i++)
        {
            if (slotData[i].id.Equals(id))
            {
                return slotData[i];
            }
        }
        return new TimeLineSlotData();
    }

    IEnumerator CreateAction(Image image)
    {
        RectTransform tr = image.GetComponent<RectTransform>();
        Vector3 addSize = tr.localScale / 25f;
        tr.localScale -= addSize * 5f;
        for (int i = 0; i < 10; i++)
        {
            tr.localScale += addSize;
            yield return new WaitForSeconds(0.01f);
        }
        for (int i = 0; i < 5; i++)
        {
            tr.localScale -= addSize;
            yield return new WaitForSeconds(0.01f);
        }
    }

}
