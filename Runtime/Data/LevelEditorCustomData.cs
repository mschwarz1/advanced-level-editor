﻿using System;
using System.Collections.Generic;

namespace Hertzole.ALE
{
    public struct LevelEditorCustomData : IEquatable<LevelEditorCustomData>
    {
        public string type;
        public object value;

        public LevelEditorCustomData(string type, object value)
        {
            this.type = type;
            this.value = value;
        }

        public override bool Equals(object obj)
        {
            return obj is LevelEditorCustomData data && Equals(data);
        }

        public bool Equals(LevelEditorCustomData other)
        {
            return type == other.type && EqualityComparer<object>.Default.Equals(value, other.value);
        }

        public override int GetHashCode()
        {
            int hashCode = 1148455455;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(type);
            hashCode = hashCode * -1521134295 + EqualityComparer<object>.Default.GetHashCode(value);
            return hashCode;
        }

        public static bool operator ==(LevelEditorCustomData left, LevelEditorCustomData right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(LevelEditorCustomData left, LevelEditorCustomData right)
        {
            return !(left == right);
        }
    }
}