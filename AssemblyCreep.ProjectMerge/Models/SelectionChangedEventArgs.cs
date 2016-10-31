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
        /// Gets a collection of all items that have been affected by selection change.
        /// </summary>
        public IEnumerable<T> AffectedItems { get; private set; }

        /// <summary>
        /// Determines whether all of the <see cref="AffectedItems"/> are selected or not.
        /// </summary>
        public bool Selected { get; private set; }

        public SelectionChangedEventArgs(bool selected, params T[] affectedItems) :
            this(selected, affectedItemsCollection: affectedItems)
        {
        }

        public SelectionChangedEventArgs(bool selected, IEnumerable<T> affectedItemsCollection)
        {
            AffectedItems = affectedItemsCollection ?? Enumerable.Empty<T>();
        }
    }
}
