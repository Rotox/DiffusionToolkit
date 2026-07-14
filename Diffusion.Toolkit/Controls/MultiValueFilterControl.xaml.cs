using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using Diffusion.Common.Query;

namespace Diffusion.Toolkit.Controls
{
    public partial class MultiValueFilterControl : UserControl
    {
        public static readonly DependencyProperty RowsProperty =
            DependencyProperty.Register(
                name: nameof(Rows),
                propertyType: typeof(ObservableCollection<MultiValueFilterRow>),
                ownerType: typeof(MultiValueFilterControl),
                typeMetadata: new UIPropertyMetadata(null)
            );

        public ObservableCollection<MultiValueFilterRow> Rows
        {
            get => (ObservableCollection<MultiValueFilterRow>)GetValue(RowsProperty);
            set => SetValue(RowsProperty, value);
        }

        public static readonly DependencyProperty ValueOptionsProperty =
            DependencyProperty.Register(
                name: nameof(ValueOptions),
                propertyType: typeof(IEnumerable<string>),
                ownerType: typeof(MultiValueFilterControl),
                typeMetadata: new UIPropertyMetadata(null)
            );

        public IEnumerable<string> ValueOptions
        {
            get => (IEnumerable<string>)GetValue(ValueOptionsProperty);
            set => SetValue(ValueOptionsProperty, value);
        }

        public static readonly DependencyProperty OperatorOptionsProperty =
            DependencyProperty.Register(
                name: nameof(OperatorOptions),
                propertyType: typeof(IEnumerable<NameValue<NodeOperation>>),
                ownerType: typeof(MultiValueFilterControl),
                typeMetadata: new UIPropertyMetadata(null)
            );

        public IEnumerable<NameValue<NodeOperation>> OperatorOptions
        {
            get => (IEnumerable<NameValue<NodeOperation>>)GetValue(OperatorOptionsProperty);
            set => SetValue(OperatorOptionsProperty, value);
        }

        public static readonly DependencyProperty ComparisonOptionsProperty =
            DependencyProperty.Register(
                name: nameof(ComparisonOptions),
                propertyType: typeof(IEnumerable<NameValue<NodeComparison>>),
                ownerType: typeof(MultiValueFilterControl),
                typeMetadata: new UIPropertyMetadata(null)
            );

        public IEnumerable<NameValue<NodeComparison>> ComparisonOptions
        {
            get => (IEnumerable<NameValue<NodeComparison>>)GetValue(ComparisonOptionsProperty);
            set => SetValue(ComparisonOptionsProperty, value);
        }

        public event EventHandler AddRowRequested;

        public MultiValueFilterControl()
        {
            InitializeComponent();
        }

        private void AddButton_OnClick(object sender, RoutedEventArgs e)
        {
            AddRowRequested?.Invoke(this, EventArgs.Empty);
        }
    }
}
