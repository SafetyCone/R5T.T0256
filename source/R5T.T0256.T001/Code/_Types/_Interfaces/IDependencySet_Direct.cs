using System;

using R5T.T0142;


namespace R5T.T0256.T001
{
    /// <summary>
    /// Interface representing a direct dependency-set relationship between keys.
    /// (Just direct dependencies, not dependencies of dependencies.)
    /// </summary>
    /// <remarks>
    /// See also: <seealso cref="IDependencySet_Cumulative{TKey}"/>
    /// </remarks>
    [DataTypeMarker]
    public interface IDependencySet_Direct<TKey> : IDependencySet<TKey>
    {
    }
}
