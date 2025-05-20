using Proc;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FigureGenerator
{
    private Transform _mainParent;
    private Dictionary<ShapeType, Pool<Figure>> _poolsFigureByShape;

    public Dictionary<ShapeType, Pool<Figure>> PoolsFigureByShape => _poolsFigureByShape;

    public FigureGenerator(Transform mainParent)
    {
        _mainParent = mainParent;

        InitPools();

        Figure.OnDisabled += (figure) => { _poolsFigureByShape[figure.Data.ShapeType].Return(figure); };
    }

    private void InitPools()
    {
        _poolsFigureByShape = new Dictionary<ShapeType, Pool<Figure>>();
        foreach (ShapeType type in System.Enum.GetValues(typeof(ShapeType)))
        {
            _poolsFigureByShape.Add(type, FigureBuilder.CreatePoolFiguresByShapes(type, _mainParent, 10));
        }
    }

    public List<Figure> GenerateFigures(int count)
    {
        List<FigureData> datas = GenerateDatas(count);
        List<Figure> figures = new List<Figure>();

        Figure newFigure;
        foreach (var data in datas)
        {
            newFigure = _poolsFigureByShape[data.ShapeType].Take();
            newFigure.SetData(data);

            figures.Add(newFigure);
        }

        return figures;
    }

    public List<FigureData> GenerateDatas(int count)
    {
        List<FigureData> datas = new List<FigureData>();

        FigureData randData;
        int remaining = count;
        while (remaining > 0)
        {
            randData = FigureBuilder.CreateRandomData();

            if (remaining >= 3)
            {
                for (int i = 0; i < 3; i++)
                    datas.Add(randData);

                remaining -= 3;
            }
        }

        datas = datas.OrderBy(_ => Random.value).ToList();

        return datas;
    }
}
