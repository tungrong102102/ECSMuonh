﻿namespace Obvious.Soap.Editor
{
    using UnityEngine;
    using UnityEditor;

    [CustomPropertyDrawer(typeof(ScriptableVariableBase), true)]
    public class ScriptableVariablePropertyDrawer : ScriptableBasePropertyDrawer
    {
        private SerializedObject _serializedObject;
        private ScriptableVariableBase _scriptableVariable;
        private float? _propertyWidthRatio;

        protected override string GetFieldName()
        {
            //fieldInfo.Name does not work for VariableReferences so we have to make an edge case for that.
            var isAbstract = fieldInfo.DeclaringType?.IsAbstract == true;
            var fieldName = isAbstract ? fieldInfo.FieldType.Name : fieldInfo.Name;
            return fieldName;
        }

        protected override void DrawUnExpanded(Rect position, SerializedProperty property, GUIContent label,
            Object targetObject)
        {
            if (_serializedObject == null || _serializedObject.targetObject != targetObject)
                _serializedObject = new SerializedObject(targetObject);

            _serializedObject.UpdateIfRequiredOrScript();
            base.DrawUnExpanded(position, property, label, targetObject);
            if (_serializedObject.targetObject != null) //can be destroyed when using sub assets
                _serializedObject.ApplyModifiedProperties();
        }

        protected override void DrawShortcut(Rect position, SerializedProperty property, Object targetObject)
        {
            if (_scriptableVariable == null)
                _scriptableVariable = _serializedObject.targetObject as ScriptableVariableBase;

            //can be destroyed when using sub assets
            if (targetObject == null)
                return;

            var genericType = _scriptableVariable.GetGenericType;
            var canBeSerialized = SoapTypeUtils.IsUnityType(genericType) || SoapTypeUtils.IsSerializable(genericType);
            if (!canBeSerialized)
            {
                SoapEditorUtils.DrawSerializationError(genericType, position);
                return;
            }

            var value = _serializedObject.FindProperty("_value");
            EditorGUI.PropertyField(position, value, GUIContent.none);
        }

        protected override float WidthRatio
        {
            get
            {
                if (_scriptableVariable == null)
                {
                    _propertyWidthRatio = null;
                    return 0.82f;
                }

                if (_propertyWidthRatio.HasValue)
                    return _propertyWidthRatio.Value;

                var genericType = _scriptableVariable.GetGenericType;
                if (genericType == typeof(Vector2))
                    _propertyWidthRatio = 0.72f;
                else if (genericType == typeof(Vector3))
                    _propertyWidthRatio = 0.62f;
                else
                    _propertyWidthRatio = 0.82f;
                return _propertyWidthRatio.Value;
            }
        }
    }
}