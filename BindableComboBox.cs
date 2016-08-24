using System;
using System.ComponentModel;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;

namespace ComboBoxBindingExample
{
    /// <summary>
    /// This is a hacky albeit maybe the simplest way to ensure that the bound SelectedItemValue
    /// gets updated in the combobox when the parameter updates on the bound SelectedItem
    /// </summary>
    public class BindableComboBox : ComboBox
    {
        private static readonly MethodInfo UpdateMethod;
        private object _oldSelectedItem;

        static BindableComboBox()
        {
            // Store the internal update method from the ComboBox once to use for all combo box controls
            const string methodName = "SelectedItemUpdated";
            UpdateMethod = typeof(ComboBox).GetMethod(methodName, BindingFlags.Instance | BindingFlags.NonPublic);
            if(UpdateMethod == null)
                throw new Exception($"Failed to find internal ComboBox.{methodName}().");
        }

        public BindableComboBox()
        {
            // Register to changes in SelectedItem
            DependencyPropertyDescriptor
                .FromProperty(SelectedItemProperty, typeof(ComboBox))
                .AddValueChanged(this, OnSelectedItemChanged);

            // Let us clean up after ourselves
            Unloaded += OnUnloaded;
        }

        private void OnUnloaded(object sender, RoutedEventArgs e)
        {
            // Clean up registrations
            Unloaded -= OnUnloaded;
            DependencyPropertyDescriptor
                .FromProperty(SelectedItemProperty, typeof(ComboBox))
                .RemoveValueChanged(this, OnSelectedItemChanged);
        }

        private void OnSelectedItemChanged(object sender, EventArgs e)
        {
            // Unbind listener for previous SelectedItem
            var selected = _oldSelectedItem as INotifyPropertyChanged;
            if (selected != null)
                selected.PropertyChanged -= UpdateSelectedItem;

            _oldSelectedItem = SelectedItem;

            // Bind new listener for SelectedItem
            selected = SelectedItem as INotifyPropertyChanged;
            if (selected != null)
                selected.PropertyChanged += UpdateSelectedItem;
        }

        private void UpdateSelectedItem(object sender, PropertyChangedEventArgs e)
        {
            // Invoke the update method that will reload any bound value
            // from the SelectedItem
            UpdateMethod.Invoke(this, null);
        }
    }
}