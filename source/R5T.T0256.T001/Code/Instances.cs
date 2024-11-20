using System;


namespace R5T.T0256.T001
{
    public static class Instances
    {
        public static L0066.IComparisonOperator ComparisonOperator => L0066.ComparisonOperator.Instance;
        public static L0066.IComparisonResults ComparisonResults => L0066.ComparisonResults.Instance;
        public static IDependencySetOperator DependencySetOperator => T001.DependencySetOperator.Instance;
        public static L0066.IEqualityOperator EqualityOperator => L0066.EqualityOperator.Instance;
        public static L0066.INullOperator NullOperator => L0066.NullOperator.Instance;
        public static L0066.IObjectOperator ObjectOperator => L0066.ObjectOperator.Instance;
        public static IPackageDescriptorOperator PackageDescriptorOperator => T001.PackageDescriptorOperator.Instance;
    }
}