using System;
using System.Collections.Generic;
using System.Linq;

using R5T.L0066.Extensions;
using R5T.T0132;


namespace R5T.T0256.T001
{
    [FunctionalityMarker]
    public partial interface IDependencySetOperator : IFunctionalityMarker
    {
        public IEnumerable<TDependencySet> Order_LeastDependent_ToMostDependent<TDependencySet, TKey>(
            IEnumerable<TDependencySet> dependencySets)
            where TDependencySet : IDependencySet<TKey>
        {
            var output = dependencySets.ToList();

            var comparer = this.Get_Comparer<TKey>() as IComparer<TDependencySet>;

            output.Sort(comparer);

            return output;
        }

        public IEnumerable<TDependencySet> Order_MostDependent_ToLeastDependent<TDependencySet, TKey>(
            IEnumerable<TDependencySet> dependencySets)
            where TDependencySet : IDependencySet<TKey>
            => this.Order_LeastDependent_ToMostDependent<TDependencySet, TKey>(
                dependencySets)
                .Reverse()
                ;

        /// <summary>
        /// Chooses <see cref="Order_MostDependent_ToLeastDependent{TDependencySet, TKey}(IEnumerable{TDependencySet})"/> as the default.
        /// </summary>
        public IEnumerable<TDependencySet> Order<TDependencySet, TKey>(
            IEnumerable<TDependencySet> dependencySets)
            where TDependencySet : IDependencySet<TKey>
            => this.Order_MostDependent_ToLeastDependent<TDependencySet, TKey>(dependencySets);

        /// <inheritdoc cref="DependencySet_Comparer{TKey}"/>
        public DependencySet_Comparer<TKey> Get_Comparer<TKey>()
            => new DependencySet_Comparer<TKey>();

        /// <summary>
        /// Compares two dependency sets based on their 
        /// </summary>
        public int Compare<TKey>(
            IDependencySet<TKey> x,
            IDependencySet<TKey> y,
            IComparer<TKey> key_Comparer)
        {
            var x_ContainsDependency_y = x.Contains(y.Name);
            if(x_ContainsDependency_y)
            {
                return Instances.ComparisonResults.GreaterThan;
            }

            var y_ContainsDependency_x = y.Contains(x.Name);
            if(y_ContainsDependency_x)
            {
                return Instances.ComparisonResults.LessThan;
            }

            // Else, compare based on x and y names.
            var output = key_Comparer.Compare(
                x.Name,
                y.Name);

            return output;
        }

        public int Compare<TKey>(
           IDependencySet<TKey> x,
           IDependencySet<TKey> y)
           => this.Compare(
               x,
               y,
               Instances.ComparisonOperator.Get_Comparer_DefaultForType<TKey>());

        public DependencySet_Cumulative<TKey> Get_CumulativeSet<TKey>(
            DependencySet_Direct<TKey> dependencySet,
            Dictionary<TKey, DependencySet_Direct<TKey>> directSets_ByName,
            Dictionary<TKey, DependencySet_Cumulative<TKey>> cumulativeSets_ByName,
            IEqualityComparer<TKey> key_EqualityComparer)
        {
            var dependencies = dependencySet.Dependencies;

            var dependencies_Accumulator = new HashSet<TKey>(
                dependencies,
                key_EqualityComparer);

            foreach (var dependency in dependencies)
            {
                var cumulativeSet_ForDependency_Exists = cumulativeSets_ByName.ContainsKey(dependency);
                if (!cumulativeSet_ForDependency_Exists)
                {
                    var directSet_ForDependency = directSets_ByName[dependency];

                    var cumulativeSet_ForDependency_Temp = this.Get_CumulativeSet(
                        directSet_ForDependency,
                        directSets_ByName,
                        cumulativeSets_ByName,
                        key_EqualityComparer);

                    cumulativeSets_ByName.Add(
                        cumulativeSet_ForDependency_Temp.Name,
                        cumulativeSet_ForDependency_Temp);
                }

                var cumulativeSet_ForDependency = cumulativeSets_ByName[dependency];

                dependencies_Accumulator.Add_Range(cumulativeSet_ForDependency.Dependencies);
            }

            var output = this.Get_DependencySet_Cumulative_From(
                dependencySet.Name,
                dependencies_Accumulator);

            return output;
        }

        public Dictionary<TKey, DependencySet_Cumulative<TKey>> Get_CumulativeSets_ByName<TKey>(
            IEnumerable<DependencySet_Direct<TKey>> directSets,
            IEqualityComparer<TKey> key_EqualityComparer)
        {
            var directSets_ByName = directSets
                .ToDictionary(
                    x => x.Name);

            var output = new Dictionary<TKey, DependencySet_Cumulative<TKey>>(key_EqualityComparer);

            foreach (var directSet in directSets_ByName.Values)
            {
                var cumulativeSet_ForDirectSet_Exists = output.ContainsKey(directSet.Name);
                if (!cumulativeSet_ForDirectSet_Exists)
                {
                    var cumulativeSet_ForDirectSet = this.Get_CumulativeSet(
                        directSet,
                        directSets_ByName,
                        output,
                        key_EqualityComparer);

                    output.Add(
                        cumulativeSet_ForDirectSet.Name,
                        cumulativeSet_ForDirectSet);
                }
            }

            return output;
        }

        public Dictionary<TKey, DependencySet_Cumulative<TKey>> Get_CumulativeSets_ByName<TKey>(IEnumerable<DependencySet_Direct<TKey>> directSets)
            => this.Get_CumulativeSets_ByName(
                directSets,
                Instances.EqualityOperator.Get_EqualityComparer_DefaultForType<TKey>());

        public TDependencySet Get_From<TDependencySet, TKey>(
            TKey name,
            HashSet<TKey> dependencies,
            Func<TKey, HashSet<TKey>, TDependencySet> constructor)
            where TDependencySet : IDependencySet<TKey>
            => constructor(
                name,
                dependencies);

        public TDependencySet Get_From<TDependencySet, TKey>(
            TKey name,
            IEnumerable<TKey> dependencies,
            Func<TKey, HashSet<TKey>, TDependencySet> constructor,
            IEqualityComparer<TKey> key_EqualityComparer)
            where TDependencySet : IDependencySet<TKey>
        {
            var dependencies_HashSet = new HashSet<TKey>(
                dependencies,
                key_EqualityComparer);

            var output = this.Get_From(
                name,
                dependencies_HashSet,
                constructor);

            return output;
        }

        public TDependencySet Get_From<TDependencySet, TKey>(
            TKey name,
            IEnumerable<TKey> dependencies,
            Func<TKey, HashSet<TKey>, TDependencySet> constructor)
            where TDependencySet : IDependencySet<TKey>
            => this.Get_From(
                name,
                dependencies,
                constructor,
                Instances.EqualityOperator.Get_EqualityComparer_DefaultForType<TKey>());


        public DependencySet<TKey> Get_From<TKey>(
            TKey name,
            HashSet<TKey> dependencies)
            => this.Get_From(
                name,
                dependencies,
                DependencySet<TKey>.Constructor);

        public DependencySet<TKey> Get_From<TKey>(
            TKey name,
            IEnumerable<TKey> dependencies,
            IEqualityComparer<TKey> key_EqualityComparer)
            => this.Get_From(
                name,
                dependencies,
                DependencySet<TKey>.Constructor,
                key_EqualityComparer);

        public DependencySet<TKey> Get_From<TKey>(
            TKey name,
            IEnumerable<TKey> dependencies)
            => this.Get_From(
                name,
                dependencies,
                DependencySet<TKey>.Constructor);


        public DependencySet<TKey> Get_DependencySet_From<TKey>(
            TKey name,
            HashSet<TKey> dependencies)
            => this.Get_From(
                name,
                dependencies);

        public DependencySet<TKey> Get_DependencySet_From<TKey>(
            TKey name,
            IEnumerable<TKey> dependencies,
            IEqualityComparer<TKey> key_EqualityComparer)
            => this.Get_From(
                name,
                dependencies,
                key_EqualityComparer);

        public DependencySet<TKey> Get_DependencySet_From<TKey>(
            TKey name,
            IEnumerable<TKey> dependencies)
            => this.Get_From(
                name,
                dependencies);


        public DependencySet_Direct<TKey> Get_DependencySet_Direct_From<TKey>(
            TKey name,
            HashSet<TKey> dependencies)
            => this.Get_From(
                name,
                dependencies,
                DependencySet_Direct<TKey>.Constructor);

        public DependencySet_Direct<TKey> Get_DependencySet_Direct_From<TKey>(
            TKey name,
            IEnumerable<TKey> dependencies,
            IEqualityComparer<TKey> key_EqualityComparer)
            => this.Get_From(
                name,
                dependencies,
                DependencySet_Direct<TKey>.Constructor,
                key_EqualityComparer);

        public DependencySet_Direct<TKey> Get_DependencySet_Direct_From<TKey>(
            TKey name,
            IEnumerable<TKey> dependencies)
            => this.Get_From(
                name,
                dependencies,
                DependencySet_Direct<TKey>.Constructor);

        public DependencySet_Direct<TKey> Get_DependencySet_Direct_From<TKey>(
            TKey name,
            params TKey[] dependencies)
            => this.Get_DependencySet_Direct_From(
                name,
                dependencies.AsEnumerable());


        public DependencySet_Cumulative<TKey> Get_DependencySet_Cumulative_From<TKey>(
            TKey name,
            HashSet<TKey> dependencies)
            => this.Get_From(
                name,
                dependencies,
                DependencySet_Cumulative<TKey>.Constructor);

        public DependencySet_Cumulative<TKey> Get_DependencySet_Cumulative_From<TKey>(
            TKey name,
            IEnumerable<TKey> dependencies,
            IEqualityComparer<TKey> key_EqualityComparer)
            => this.Get_From(
                name,
                dependencies,
                DependencySet_Cumulative<TKey>.Constructor,
                key_EqualityComparer);

        public DependencySet_Cumulative<TKey> Get_DependencySet_Cumulative_From<TKey>(
            TKey name,
            IEnumerable<TKey> dependencies)
            => this.Get_From(
                name,
                dependencies,
                DependencySet_Cumulative<TKey>.Constructor);
    }
}
