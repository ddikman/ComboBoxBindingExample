# ComboBoxBindingExample
Example of an issue with and solution for binding the WPF ComboBox with viewmodels.

## What is this?
I had huge issues get a data binding to work and as such I created this project to isolate and fix the issue.

The issue is that if you bind, as in the project, a  list of view models to an editable combo box the bound display value, as specified by SelectedItemValue doesn't get updated even if the view model properly triggers NotifyPropertyChanged.

## Solution
There are probably various other solutions to this but in the end what I went for was to wrap the original combo box and tap into the internal methods of the original class in order to force an update when detected.