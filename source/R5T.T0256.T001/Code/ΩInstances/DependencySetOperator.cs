using System;


namespace R5T.T0256.T001
{
    public class DependencySetOperator : IDependencySetOperator
    {
        #region Infrastructure

        public static IDependencySetOperator Instance { get; } = new DependencySetOperator();


        private DependencySetOperator()
        {
        }

        #endregion
    }
}
