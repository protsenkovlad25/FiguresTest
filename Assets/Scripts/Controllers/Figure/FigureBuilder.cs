using Proc;
using UnityEngine;

public static class FigureBuilder
{
    public static Pool<Figure> CreatePoolFiguresByShapes(ShapeType type, Transform parent, int count = 0)
    {
        GameObject newParent = new GameObject();
        newParent.name = type.ToString() + "Parent";
        newParent.transform.parent = parent;

        Pool<Figure> pool = new Pool<Figure>(Configs.FiguresConfig.GetPrefab(type), newParent.transform, count);

        return pool;
    }

    public static FigureData CreateRandomData()
    {
        ShapeType shapeType = Configs.FiguresConfig.GetRandShapeType();
        FoodType foodType = Configs.FiguresConfig.GetRandFoodType();
        ColorType colorType = Configs.FiguresConfig.GetRandColorType();
        
        FigureData newData = new FigureData(shapeType, foodType, colorType);

        return newData;
    }
}
