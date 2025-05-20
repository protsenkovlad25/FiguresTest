using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Configs/FiguresConfig", fileName = "FiguresConfig")]
public class FiguresConfig : ScriptableObject
{
    [System.Serializable]
    private class ShapeData
    {
        [SerializeField] private ShapeType _type;
        [SerializeField] private Sprite _sprite;

        [AssetPathFilter("Assets/Prefabs/Entities")]
        [SerializeField] private Figure _figurePrefab;

        public ShapeType Type => _type;
        public Sprite Sprite => _sprite;
        public Figure FigurePrefab => _figurePrefab;
    }

    [System.Serializable]
    private class FoodData
    {
        [SerializeField] private FoodType _type;
        [SerializeField] private Sprite _sprite;

        public FoodType Type => _type;
        public Sprite Sprite => _sprite;
    }

    [System.Serializable]
    private class ColorData
    {
        [SerializeField] private ColorType _type;
        [SerializeField] private Color _color;

        public ColorType Type => _type;
        public Color Color => _color;
    }

    [Range(3, 63)]
    [SerializeField] private int _count;
    
    [Header("Prefabs")]
    [SerializeField] private FigureUI _figureUIPrefab;
    
    [Header("Datas")]
    [SerializeField] private List<ShapeData> _shapeDatas;
    [SerializeField] private List<FoodData> _foodDatas;
    [SerializeField] private List<ColorData> _colorDatas;

    public int Count => _count;

    public FigureUI FigureUIPrefab => _figureUIPrefab;

    private void OnValidate()
    {
        _count = Mathf.RoundToInt(_count / 3f) * 3;
    }

    #region Getters Data
    public Figure GetPrefab(ShapeType type)
    {
        return _shapeDatas.Find(d => d.Type == type).FigurePrefab;
    }

    public Sprite GetShapeSprite(ShapeType type)
    {
        return _shapeDatas.Find(d => d.Type == type).Sprite;
    }
    public Sprite GetFoodSprite(FoodType type)
    {
        return _foodDatas.Find(d => d.Type == type).Sprite;
    }
    public Color GetColor(ColorType type)
    {
        return _colorDatas.Find(d => d.Type == type).Color;
    }
    #endregion

    #region Getters Random Data
    public ShapeType GetRandShapeType()
    {
        return GetRandListElement(_shapeDatas).Type;
    }
    public FoodType GetRandFoodType()
    {
        return GetRandListElement(_foodDatas).Type;
    }
    public ColorType GetRandColorType()
    {
        return GetRandListElement(_colorDatas).Type;
    }

    private T GetRandListElement<T>(List<T> list)
    {
        return list[Random.Range(0, list.Count)];
    }
    #endregion
}
