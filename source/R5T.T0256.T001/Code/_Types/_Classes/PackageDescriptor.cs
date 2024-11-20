using System;

using R5T.T0142;


namespace R5T.T0256.T001
{
    /// <summary>
    /// Describes a package (NuGet, npm, or otherwise).
    /// </summary>
    /// <remarks>
    /// This is dead stupid-simple package descriptor type.
    /// Useful for basic data storage.
    /// <para>
    /// Prior work:
    /// <list type="bullet">
    /// <item>R5T.T0152.PackageReference: More specific for Visual Studio project files (Identity: string, Version: Version, PrivateAssets: string)</item>
    /// <item>R5T.T0205.PackageReference: strongly-typed properties (Name: IPackageName, Version: IPackageVersion)</item>
    /// <item>R5T.T0206.PackageReference: (Name: string, Version: Version)</item>
    /// <item>R5T.T0208.PackageReference: (Identity: string, Version: string)</item>
    /// <item>D8S.S0000.PackageDescriptor: record (Name: string, VersionName: string)</item>
    /// <item>D8S.S0011.L000.PackageDescriptor: (PackageName: string, PackageVersion: string)</item>
    /// </list>
    /// </para>
    /// </remarks>
    [DataTypeMarker]
    public class PackageDescriptor : IEquatable<PackageDescriptor>
    {
        /// <summary>
        /// Example: <inheritdoc cref="Y0006.Documentation.For_Packages.Example_PackageName" path="descendant::value"/>.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Example: "<inheritdoc cref="Y0006.Documentation.For_Versions.Example_Version" path="descendant::value"/>".
        /// </summary>
        /// <remarks>
        /// As a string, the version can contain version names as well (like "<inheritdoc cref="Y0006.Documentation.For_Versions.Example_VersionName" path="descendant::value"/>").
        /// </remarks>
        public string Version { get; set; }


        public bool Equals(PackageDescriptor other)
        {
            var output = Instances.PackageDescriptorOperator.Equals_ByValue(this, other);
            return output;
        }

        public override bool Equals(object obj)
        {
            var objAsPackageReference = obj as PackageDescriptor;

            var output = this.Equals(objAsPackageReference);
            return output;
        }

        public override int GetHashCode()
        {
            return Instances.PackageDescriptorOperator.Get_HashCode(this);
        }

        public override string ToString()
        {
            var output = Instances.PackageDescriptorOperator.To_String(this);
            return output;
        }
    }
}
