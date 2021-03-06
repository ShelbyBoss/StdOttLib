﻿using System;
using Windows.UI.Xaml.Data;

namespace StdOttUwp.Converters
{
    public abstract class EnumConverter<T> : IValueConverter where T : struct, IComparable, IFormattable, IConvertible
    {
        private T currentValue;

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            currentValue = (T)value;

            return currentValue.Equals(GetValue(parameter.ToString()));
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if ((bool)value) return currentValue = GetValue(parameter.ToString());

            return currentValue;
        }

        protected virtual T GetValue(string name)
        {
            return (T)Enum.Parse(typeof(T), name);
        }
    }
}
