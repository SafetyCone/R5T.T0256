using System;

using R5T.T0132;


namespace R5T.T0256.T001
{
    [FunctionalityMarker]
    public partial interface IPackageDescriptorOperator : IFunctionalityMarker
    {
        public int Get_HashCode(PackageDescriptor a)
        {
            var output = HashCode.Combine(
                a.Name,
                a.Version);

            return output;
        }

        public bool Equals_ByName(PackageDescriptor a, PackageDescriptor b)
        {
            if (Instances.NullOperator.NullCheckDeterminesEquality(a, b, out var areEqual))
            {
                return areEqual;
            }

            var output = a.Name == b.Name;
            return output;
        }

        public bool Equals_ByValue(PackageDescriptor a, PackageDescriptor b)
        {
            if (Instances.NullOperator.NullCheckDeterminesEquality(a, b, out var areEqual))
            {
                return areEqual;
            }

            var output = true
               && a.Name == b.Name
               && a.Version == b.Version
               ;

            return output;
        }

        public string To_String(PackageDescriptor packageReference)
        {
            var output = $"{packageReference.Name} ({packageReference.Version})";
            return output;
        }
    }
}
