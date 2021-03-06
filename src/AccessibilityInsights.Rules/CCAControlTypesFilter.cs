﻿// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
using AccessibilityInsights.Rules.PropertyConditions;
using System.Collections.Generic;
using System.Reflection;

namespace AccessibilityInsights.Rules
{
    public class CCAControlTypesFilter
    {
        private static CCAControlTypesFilter DefaultInstance;
        private readonly HashSet<int> MainTypes = new HashSet<int>();

        /// <summary>
        /// GetDefaultInstance
        /// </summary>
        /// <returns></returns>
        public static CCAControlTypesFilter GetDefaultInstance()
        {
            if (DefaultInstance == null)
            {
                DefaultInstance = new CCAControlTypesFilter();
                DefaultInstance.Filter();
            }

            return DefaultInstance;
        }

        private void Filter() {
            var type = typeof(ControlType);
            var fields = type.GetFields(BindingFlags.GetField | BindingFlags.Public | BindingFlags.Static);

            foreach (FieldInfo field in fields)
            {
                    MainTypes.Add(((ControlTypeCondition)field.GetValue(field)).ControlType);
            }
        }

        public bool Contains(int typeId)
        {
            return MainTypes.Contains(typeId);
        }
}


}
