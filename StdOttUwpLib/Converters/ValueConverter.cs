﻿using System;
using Windows.UI.Xaml.Data;

namespace StdOttUwp.Converters
{
    public delegate object ConvertEventHandler(object value, Type targetType, object parameter, string language);
    public delegate object ConvertBackEventHandler(object value, Type targetType, object parameter, string language);

    public class ValueConverter : IValueConverter
    {
        public event ConvertEventHandler ConvertEvent;
        public event ConvertBackEventHandler ConvertBackEvent;

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return ConvertEvent(value, targetType, parameter, language);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return ConvertBackEvent(value, targetType, parameter, language);
        }
    }
}
