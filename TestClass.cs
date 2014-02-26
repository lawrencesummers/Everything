using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LearnAttribute;
namespace Everything
{
    public class TestClass
    {
        [Transactionable]
        public void Foo()
        { }
        public void Bar()
        { }
        [Transactionable]
        public void Baz()
        { }
    }
}
