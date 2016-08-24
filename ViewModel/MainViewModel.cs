using System.Collections.ObjectModel;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;

namespace ComboBoxBindingExample.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private ItemViewModel _selectedItem;

        public MainViewModel()
        {
            Items = new ObservableCollection<ItemViewModel>
            {
                new ItemViewModel("Banana", "Yellow"),
                new ItemViewModel("Orange", "Orange"),
                new ItemViewModel("Apple", "Red"),
                new ItemViewModel("Pear", "Green")
            };

            SelectedItem = Items[0];

            AddCommand = new RelayCommand(() => Items.Add(new ItemViewModel()));
        }

        public ICommand AddCommand { get; }

        public ObservableCollection<ItemViewModel> Items { get; set; }

        public ItemViewModel SelectedItem
        {
            get { return _selectedItem; }
            set { Set(() => SelectedItem, ref _selectedItem, value); }
        }

    }
}