using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Simple.Testing.Framework;

namespace Nancy.Commandly.Tests
{
    public class Generator : ISpecificationGenerator
    {
        private readonly Assembly[] assemblies;

        public Generator(params Assembly[] assemblies)
        {
            this.assemblies = assemblies;
        }

        public IEnumerable<SpecificationToRun> GetSpecifications()
        {
            return (from asm in assemblies
                    from type in asm.GetTypes()
                    from spec in TypeReader.GetSpecificationsIn(type)
                    where spec != null
                    select spec);
        }
    }
}