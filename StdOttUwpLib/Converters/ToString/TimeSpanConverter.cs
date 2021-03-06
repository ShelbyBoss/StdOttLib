﻿using System;

namespace StdOttUwp.Converters
{
    public class TimeSpanConverter : ToStringTwoWayConverter<TimeSpan>
    {
        protected override bool TryParse(string newText, Type targetType, object parameter, string language, out TimeSpan newValue)
        {
            return TimeSpan.TryParse(newText, out newValue);
        }
    }
}
