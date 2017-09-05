using System.Collections.Generic;
using System.ComponentModel;
using Blog.Libraries.Core.Infrastructure;

namespace Blog.Libraries.Core.ComponentModel
{

    public class TypeConverterRegistartionStartupTask : IStartupTask
    {
        public void Execute()
        {
            //List
            TypeDescriptor.AddAttributes(typeof(List<string>), new TypeConverterAttribute(typeof(GenericListTypeConverter<string>)));

            //Dictionary
            TypeDescriptor.AddAttributes(typeof(Dictionary<string, string>),
                new TypeConverterAttribute(typeof(GenericDictionaryTypeConverter<string, string>)));
        }

        public int Order => 0;

    }

}
