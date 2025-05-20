using System;
using UnityEngine;

[AttributeUsage(AttributeTargets.Field)]
public class AssetPathFilterAttribute : PropertyAttribute
{
    public string Path;
    public AssetPathFilterAttribute(string path)
    {
        Path = path;
    }
}

