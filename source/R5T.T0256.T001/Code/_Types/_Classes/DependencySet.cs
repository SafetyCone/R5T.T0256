using System;
using System.Collections.Generic;
using System.Linq;

using R5T.T0142;


namespace R5T.T0256.T001
{
    /// <inheritdoc cref="IDependencySet{TKey}"/>
    [DataTypeMarker]
    public class DependencySet<TKey> : IDependencySet<TKey>
    {
        #region Static

        public static DependencySet<TKey> Constructor(
            TKey name,
            HashSet<TKey> dependencies)
            => new DependencySet<TKey>(
                name,
                dependencies);

        #endregion


        public HashSet<TKey> Dependencies { get; }


        public TKey Name { get; set; }

        TKey[] IDependencySet<TKey>.Dependencies
            => this.Dependencies.ToArray();


        public DependencySet(
            TKey name,
            HashSet<TKey> dependencies)
        {
            this.Name = name;
            this.Dependencies = dependencies;
        }

        public bool Contains(TKey key)
            => this.Dependencies.Contains(key);
    }
}
