using System;
using System.Collections.Generic;

using R5T.T0142;


namespace R5T.T0256.T001
{
    /// <inheritdoc cref="IDependencySet_Direct{TKey}"/>
    [DataTypeMarker]
    public class DependencySet_Direct<TKey> : DependencySet<TKey>, IDependencySet_Direct<TKey>
    {
        #region Static

        public static new DependencySet_Direct<TKey> Constructor(
            TKey name,
            HashSet<TKey> dependencies)
            => new DependencySet_Direct<TKey>(
                name,
                dependencies);

        #endregion


        public DependencySet_Direct(
            TKey name,
            HashSet<TKey> dependencies)
            : base(
                  name,
                  dependencies)
        {
        }
    }
}
