using System;
using System.Collections.Generic;

using R5T.T0142;


namespace R5T.T0256.T001
{
    /// <inheritdoc cref="IDependencySet_Direct{TKey}"/>
    [DataTypeMarker]
    public class DependencySet_Cumulative<TKey> : DependencySet<TKey>, IDependencySet_Cumulative<TKey>
    {
        #region Static

        public static new DependencySet_Cumulative<TKey> Constructor(
            TKey name,
            HashSet<TKey> dependencies)
            => new DependencySet_Cumulative<TKey>(
                name,
                dependencies);

        #endregion


        public DependencySet_Cumulative(
            TKey name,
            HashSet<TKey> dependencies)
            : base(
                  name,
                  dependencies)
        {
        }
    }
}
