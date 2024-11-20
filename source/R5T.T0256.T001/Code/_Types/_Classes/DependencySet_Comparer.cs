using System;
using System.Collections.Generic;

using R5T.T0142;


namespace R5T.T0256.T001
{
    /// <summary>
    /// Orders dependency sets from least dependent to most dependent.
    /// </summary>
    [DataTypeMarker]
    public class DependencySet_Comparer<TKey> : IComparer<IDependencySet<TKey>>
    {
        public IComparer<TKey> Key_Comparer { get; }


        public DependencySet_Comparer(
            IComparer<TKey> key_Comparer)
        {
            this.Key_Comparer = key_Comparer;
        }

        public DependencySet_Comparer()
            : this(Instances.ComparisonOperator.Get_Comparer_DefaultForType<TKey>())
        {
        }

        public int Compare(
            IDependencySet<TKey> x,
            IDependencySet<TKey> y)
            => Instances.DependencySetOperator.Compare(
                x,
                y,
                this.Key_Comparer);
    }
}
