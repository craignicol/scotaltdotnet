namespace Horn.Core.Spec.Unit.DependencyTree
{
    using System;
    using System.Collections.Generic;
    using Horn.Core.dsl;
    public interface IDependencyTree
    {
        IList<IBuildMetaData> BuildList { get; }
    }
}
