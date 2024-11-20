using System;


namespace R5T.T0256.T001
{
    public class PackageDescriptorOperator : IPackageDescriptorOperator
    {
        #region Infrastructure

        public static IPackageDescriptorOperator Instance { get; } = new PackageDescriptorOperator();


        private PackageDescriptorOperator()
        {
        }

        #endregion
    }
}
