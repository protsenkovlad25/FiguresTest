using System.Linq;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(AssetPathFilterAttribute))]
public class AssetPathFilterDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        var attr = (AssetPathFilterAttribute)attribute;
        var path = attr.Path;

        EditorGUI.BeginProperty(position, label, property);

        float fieldWidth = position.width - 65;
        Rect fieldRect = new Rect(position.x, position.y, fieldWidth, position.height);
        Rect buttonRect = new Rect(position.x + fieldWidth + 5, position.y, 60, position.height);

        EditorGUI.ObjectField(fieldRect, property, fieldInfo.FieldType, label);

        if (GUI.Button(buttonRect, "Select"))
        {
            string typeName = fieldInfo.FieldType.Name;

            if (typeof(Component).IsAssignableFrom(fieldInfo.FieldType))
            {
                string[] guids = AssetDatabase.FindAssets("t:GameObject", new[] { path });
                var objs = guids
                    .Select(guid => AssetDatabase.LoadAssetAtPath<GameObject>(AssetDatabase.GUIDToAssetPath(guid)))
                    .Where(go => go != null && go.GetComponent(fieldInfo.FieldType) != null)
                    .ToArray();

                ShowMenu(objs.Select(go => go.GetComponent(fieldInfo.FieldType) as Object).ToArray(), property);
            }
            else
            {
                string[] guids = AssetDatabase.FindAssets($"t:{typeName}", new[] { path });
                var objs = guids
                    .Select(guid => AssetDatabase.LoadAssetAtPath<Object>(AssetDatabase.GUIDToAssetPath(guid)))
                    .Where(obj => obj != null && fieldInfo.FieldType.IsAssignableFrom(obj.GetType()))
                    .ToArray();

                ShowMenu(objs, property);
            }
        }

        EditorGUI.EndProperty();
    }

    private void ShowMenu(Object[] objects, SerializedProperty property)
    {
        var menu = new GenericMenu();

        if (objects.Length == 0)
        {
            menu.AddDisabledItem(new GUIContent("Нет подходящих объектов"));
        }
        else
        {
            foreach (var obj in objects)
            {
                string path = AssetDatabase.GetAssetPath(obj);
                menu.AddItem(new GUIContent(obj.name), false, () =>
                {
                    property.objectReferenceValue = obj;
                    property.serializedObject.ApplyModifiedProperties();
                });
            }
        }

        menu.ShowAsContext();
    }
}


