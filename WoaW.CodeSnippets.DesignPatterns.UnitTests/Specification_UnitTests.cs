using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WoaW.Patterns.Specification;
using System.Collections.Generic;
using System.Linq;

namespace WoaW.CodeSnippets.DesignPatterns.UnitTests
{
    [TestClass]
    public class Specification_UnitTests
    {
        class Resource
        {
            public int Property { get; set; }
        }
        class ConcretSpecification : CompositeSpecification<Resource>
        {
            public int _propertyValue { get; set; }

            public ConcretSpecification(int value)
            {
                _propertyValue = value;
            }

            public override bool IsSatisfiedBy(Resource entity)
            {
                return entity.Property == _propertyValue;
            }
        }

        private List<Resource> _list = new List<Resource>();

        public Specification_UnitTests()
        {
            _list.Add( new Resource() { Property = 0 });
            _list.Add( new Resource() { Property = 1 });
            _list.Add( new Resource() { Property = 2 });
            _list.Add( new Resource() { Property = 3 });
            _list.Add( new Resource() { Property = 4 });
        }

        private ICollection<Resource> Find(ConcretSpecification spec)
        {
            return _list.Where(s=>spec.IsSatisfiedBy(s)).ToList();
        }

        [TestMethod]
        public void TestMethod1()
        {
            var s = new ConcretSpecification(2);
            var result = Find(s).ToList();
            CollectionAssert.AllItemsAreNotNull(result);
            Assert.AreEqual(1, result.Count);
            var item = result[0];
            Assert.AreEqual(2, item.Property);
        }
    }
}
