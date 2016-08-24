using GalaSoft.MvvmLight;

namespace ComboBoxBindingExample.ViewModel
{
    public class ItemViewModel : ViewModelBase
    {
        private string _name;
        private string _value;

        public ItemViewModel() : this("New", "Not set")
        { }

        public ItemViewModel(string name, string value)
        {
            Name = name;
            Value = value;
        }

        public string Name
        {
            get { return _name; }
            set { Set(() => Name, ref _name, value); }

        }

        public string Value
        {
            get { return _value; }
            set { Set(() => Value, ref _value, value); }
        }


        public override string ToString()
        {
            return Name;
        }
    }
}