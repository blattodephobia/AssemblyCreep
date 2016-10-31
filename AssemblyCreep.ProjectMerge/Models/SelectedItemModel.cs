using System.IO;

namespace AssemblyCreep.Models
{
    public class SelectedItemModel<T>
    {
        public bool IsSelected { get; set; }

        public T Item { get; private set; }

        public object Source { get; set; }

        public string Description { get; set; }

        public SelectedItemModel(T item)
        {
            Item = item;
        }
    }
}
