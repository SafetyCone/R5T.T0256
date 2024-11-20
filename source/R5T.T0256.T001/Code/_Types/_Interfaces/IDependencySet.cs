using System;

using R5T.T0142;


namespace R5T.T0256.T001
{
    /// <summary>
    /// Interface representing a dependency-set relationship between keys.
    /// </summary>
    [DataTypeMarker]
    public interface IDependencySet<TKey>
    {
        TKey Name { get; }

        TKey[] Dependencies { get; }


        bool Contains(TKey key);
    }
}
