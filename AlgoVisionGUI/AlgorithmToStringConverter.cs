﻿using System;
using System.Globalization;
using System.Windows.Data;
using ViewModels;

namespace AlgoVisionGUI
{
    class AlgorithmToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;

            if (!(value is ObservableSortingAlgorithm alg))
                throw new InvalidCastException(
                    "AlgorithmToStringConverter received a value that was not of type ObservableSortingAlgorithm");

            return alg.Name;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
