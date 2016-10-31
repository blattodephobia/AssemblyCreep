using Prism.Mvvm;
using System;
using System.IO;

namespace AssemblyCreep.Models
{
    public class SelectedItemViewModel<T> : BindableBase
    {
        private bool _isSelected;
        public bool IsSelected
        {
            get
            {
                return _isSelected;
            }

            set
            {
                SetProperty(ref _isSelected, value);
            }
        }

        public T Item { get; private set; }

        public object Source { get; set; }

        public string Description { get; set; }

        public SelectedItemViewModel(T item)
        {
            Item = item;
        }
    }
}
