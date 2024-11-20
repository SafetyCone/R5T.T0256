using System;

using R5T.T0142;


namespace R5T.T0256.T001
{
    /// <summary>
    /// Interface representing a cumulative dependency-set relationship between keys.
    /// (All dependencies, including dependencies of dependencies, not just direct dependencies.)
    /// </summary>
    /// <remarks>
    /// See also: <seealso cref="IDependencySet_Direct{TKey}"/>
    /// </remarks>
    [DataTypeMarker]
    public interface IDependencySet_Cumulative<TKey> : IDependencySet<TKey>
    {
    }
}
