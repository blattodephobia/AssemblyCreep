using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssemblyCreep.Models
{
    public class SelectionChangedEventArgs<T> : EventArgs
    {
        /// <summary>
        /// Determines whether all of the items in the <see cref="AffectedItems"/> property are selected or not.
        /// </summary>
        public bool Selected { get; private set; }

        /// <summary>
        /// Gets the collection of items affected by a single selection changed event.
        /// </summary>
        public IEnumerable<T> AffectedItems { get; private set; }

        public SelectionChangedEventArgs(bool selected, params T[] affectedItems) :
            this(selected, affectedItemsCollection: affectedItems)
        {
        }

        public SelectionChangedEventArgs(bool selected, IEnumerable<T> affectedItemsCollection)
        {
            Selected = selected;
            AffectedItems = affectedItemsCollection;
        }
    }
}
