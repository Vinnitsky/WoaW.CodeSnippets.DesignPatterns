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

        private IEnumerable<Resource> Find(ISpecification<Resource> spec)
        {
            return _list.Where(s=>spec.IsSatisfiedBy(s));
        }

        [TestMethod]
        public void SingleCondition_SucccessTest()
        {
            var s = new ConcretSpecification(2);
            var result = Find(s).ToList();
            CollectionAssert.AllItemsAreNotNull(result);
            Assert.AreEqual(1, result.Count);
            var item = result[0];
            Assert.AreEqual(2, item.Property);
        }

        [TestMethod]
        public void CombinedCondition_ByOr_SuccessTest()
        {
            var s1 = new ConcretSpecification(1);
            var s2 = new ConcretSpecification(2);
            var s = s1.Or(s2);
            var result = Find(s).ToList();

            CollectionAssert.AllItemsAreNotNull(result);
            Assert.AreEqual(2, result.Count);

            var item1 = result[0];
            Assert.AreEqual(1, item1.Property);
            var item2 = result[1];
            Assert.AreEqual(2, item2.Property);
        }
    }
}
