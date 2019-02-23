﻿using System.Windows;

namespace StdOttFramework.Converters
{
    public delegate object ConvertInputs5EventHandler(object input0, object input1, object input2, object input3, object input4);
    public delegate object ConvertInputs5RefEventHandler(ref object input0,
        ref object input1, ref object input2, ref object input3, ref object input4);

    public class MultipleInputs5Converter : FrameworkElement
    {
        public static readonly DependencyProperty OutputProperty = DependencyProperty.Register("Output",
            typeof(object), typeof(MultipleInputs10Converter), new PropertyMetadata(null));

        public static readonly DependencyProperty Input0Property =
            DependencyProperty.Register("Input0", typeof(object), typeof(MultipleInputs5Converter),
                new PropertyMetadata(null, new PropertyChangedCallback(OnInputXPropertyChanged)));


        public static readonly DependencyProperty Input1Property =
            DependencyProperty.Register("Input1", typeof(object), typeof(MultipleInputs5Converter),
                new PropertyMetadata(null, new PropertyChangedCallback(OnInputXPropertyChanged)));


        public static readonly DependencyProperty Input2Property =
            DependencyProperty.Register("Input2", typeof(object), typeof(MultipleInputs5Converter),
                new PropertyMetadata(null, new PropertyChangedCallback(OnInputXPropertyChanged)));


        public static readonly DependencyProperty Input3Property =
            DependencyProperty.Register("Input3", typeof(object), typeof(MultipleInputs5Converter),
                new PropertyMetadata(null, new PropertyChangedCallback(OnInputXPropertyChanged)));


        public static readonly DependencyProperty Input4Property =
            DependencyProperty.Register("Input4", typeof(object), typeof(MultipleInputs5Converter),
                new PropertyMetadata(null, new PropertyChangedCallback(OnInputXPropertyChanged)));


        private static void OnInputXPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            ((MultipleInputs5Converter)sender).SetOutput();
        }

        private bool isUpdating;
        private ConvertInputs5EventHandler convert;
        private ConvertInputs5RefEventHandler convertRef;

        public ConvertInputs5EventHandler Convert
        {
            get { return convert; }
            set
            {
                if (value == convert) return;

                convert = value;
                SetOutput();
            }
        }

        public ConvertInputs5RefEventHandler ConvertRef
        {
            get { return convertRef; }
            set
            {
                if (value == convertRef) return;

                convertRef = value;
                SetOutput();
            }
        }

        public object Output
        {
            get { return GetValue(OutputProperty); }
            set { SetValue(OutputProperty, value); }
        }

        public object Input0
        {
            get { return GetValue(Input0Property); }
            set { SetValue(Input0Property, value); }
        }

        public object Input1
        {
            get { return GetValue(Input1Property); }
            set { SetValue(Input1Property, value); }
        }

        public object Input2
        {
            get { return GetValue(Input2Property); }
            set { SetValue(Input2Property, value); }
        }

        public object Input3
        {
            get { return GetValue(Input3Property); }
            set { SetValue(Input3Property, value); }
        }

        public object Input4
        {
            get { return GetValue(Input4Property); }
            set { SetValue(Input4Property, value); }
        }

        private void SetOutput()
        {
            if (Convert != null) SetOutputNonRef();
            else if (ConvertRef != null) SetOutputRef();
            else Output = null;
        }

        private void SetOutputNonRef()
        {
            Output = Convert(Input0, Input1, Input2, Input3, Input4);
        }

        private void SetOutputRef()
        {
            if (isUpdating) return;
            isUpdating = true;

            object input0 = Input0, input1 = Input1, input2 = Input2, input3 = Input3, input4 = Input4;

            Output = ConvertRef(ref input0, ref input1, ref input2, ref input3, ref input4);

            Input0 = input0;
            Input1 = input1;
            Input2 = input2;
            Input3 = input3;
            Input4 = input4;

            isUpdating = false;
        }
    }
}
