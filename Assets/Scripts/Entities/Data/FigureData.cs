using UnityEngine;

[System.Serializable]
public struct FigureData
{
    [SerializeField] private ShapeType _shapeType;
    [SerializeField] private FoodType _foodType;
    [SerializeField] private ColorType _colorType;

    public ShapeType ShapeType => _shapeType;
    public FoodType FoodType => _foodType;
    public ColorType ColorType => _colorType;

    public FigureData(ShapeType shape, FoodType food, ColorType color)
    {
        _shapeType = shape;
        _foodType = food;
        _colorType = color;
    }

    public override bool Equals(object obj)
    {
        if (obj is not FigureData other)
            return false;
        
        return _shapeType == other._shapeType &&
               _foodType == other._foodType &&
               _colorType == other._colorType;
    }

    public override int GetHashCode()
    {
        return System.HashCode.Combine(_shapeType, _foodType, _colorType);
    }
}
