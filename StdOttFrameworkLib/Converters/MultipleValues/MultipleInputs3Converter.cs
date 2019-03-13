using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace StdOttFramework.Converters
{
    public delegate object ConvertInputs3EventHandler(object input0, object input1, object input2, int changedInput);
    public delegate object ConvertInputs3RefEventHandler(ref object input0, ref object input1, ref object input2, int changedInput);

    public class MultipleInputs3Converter : FrameworkElement
    {
        public static readonly DependencyProperty OutputProperty = DependencyProperty.Register("Output",
            typeof(object), typeof(MultipleInputs3Converter), new PropertyMetadata(null));

        public static readonly DependencyProperty Input0Property =
            DependencyProperty.Register("Input0", typeof(object), typeof(MultipleInputs3Converter),
                new PropertyMetadata(null, new PropertyChangedCallback(OnInput0PropertyChanged)));


        private static void OnInput0PropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            ((MultipleInputs3Converter)sender).SetOutput(0);
        }

        public static readonly DependencyProperty Input1Property =
            DependencyProperty.Register("Input1", typeof(object), typeof(MultipleInputs3Converter),
                new PropertyMetadata(null, new PropertyChangedCallback(OnInput1PropertyChanged)));


        private static void OnInput1PropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            ((MultipleInputs3Converter)sender).SetOutput(1);
        }

        public static readonly DependencyProperty Input2Property =
            DependencyProperty.Register("Input2", typeof(object), typeof(MultipleInputs3Converter),
                new PropertyMetadata(null, new PropertyChangedCallback(OnInput2PropertyChanged)));


        private static void OnInput2PropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            ((MultipleInputs3Converter)sender).SetOutput(2);
        }

        private bool isUpdating;
        private List<ConvertInputs3EventHandler> converts = new List<ConvertInputs3EventHandler>();
        private List<ConvertInputs3RefEventHandler> convertRefs = new List<ConvertInputs3RefEventHandler>();

        public event ConvertInputs3EventHandler Convert
        {
            add
            {
                converts.Add(value);
                SetOutput(-1);
            }
            remove
            {
                converts.Remove(value);
                SetOutput(-1);
            }
        }

        public event ConvertInputs3RefEventHandler ConvertRef
        {
            add
            {
                convertRefs.Add(value);
                SetOutput(-1);
            }
            remove
            {
                convertRefs.Remove(value);
                SetOutput(-1);
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

        private void SetOutput(int changedIndex)
        {
            if (converts.Count > 0) SetOutputNonRef(changedIndex);
            else if (convertRefs.Count > 0) SetOutputRef(changedIndex);
            else Output = null;
        }

        private void SetOutputNonRef(int changedIndex)
        {
            Output = converts.Last()(Input0, Input1, Input2, changedIndex);
        }

        private void SetOutputRef(int changedIndex)
        {
            if (isUpdating) return;
            isUpdating = true;

            object input0 = Input0, input1 = Input1, input2 = Input2;

            Output = convertRefs.Last()(ref input0, ref input1, ref input2, changedIndex);

            Input0 = input0;
            Input1 = input1;
            Input2 = input2;

            isUpdating = false;
        }
    }
}