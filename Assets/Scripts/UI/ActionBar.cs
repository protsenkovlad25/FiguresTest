using System.Collections.Generic;
using UnityEngine;

public class ActionBar : MonoBehaviour
{
    [Header("Slots Data")]
    [SerializeField] private Transform _slotsParent;
    [SerializeField] private List<ActionSlot> _slots;

    public bool ExistFreeSlot => GetFreeSlot() != null;

    public void Init()
    {
        foreach (var slot in _slots)
            slot.Init();
    }

    public ActionSlot GetFreeSlot()
    {
        return _slots.Find(s => s.IsFree);
    }

    public void RemoveFigures(FigureData data)
    {
        foreach (var slot in _slots)
        {
            if (slot.Data.Equals(data))
            {
                slot.RemoveData();
                slot.ChangeActivity(false);
            }
        }
    }

    public void ClearSlots()
    {
        foreach (var slot in _slots)
        {
            slot.RemoveData();
            slot.ChangeActivity(false);
        }
    }
}
