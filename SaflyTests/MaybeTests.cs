using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Safely;
using System.Collections.Generic;
using System.Linq;

namespace SaflyTests
{
    [TestClass]
    public class MaybeTests
    {
        private TestContext testContextInstance;

		/// <summary>
		///Gets or sets the test context which provides
		///information about and functionality for the current test run.
		///</summary>
		public TestContext TestContext
		{
			get
			{
				return testContextInstance;
			}
			set
			{
				testContextInstance = value;
			}
		}		

		[TestMethod]
		public void Let_on_An_Class_returns_value()
		{
			var testValue = "Erin Was here";
			var td = new TestableDummy
			{
				DummyString = testValue,
				DummyValueType = 5
			};

			Assert.AreEqual(td.Let(c => c.DummyString), testValue);
		}

		[TestMethod]
		public void Let_will_return_FailVale_Function_when_class_is_null()
		{
			TestableDummy td = null;

            Assert.AreEqual(td.Let(c => c.DummyString, () => "Tom"), "Tom");            
		}

        [TestMethod]
        public void Let_will_return_FailVale_when_class_is_null()
        {
            TestableDummy td = null;

            Assert.AreEqual(td.Let(c => c.DummyString, "Tom"), "Tom");
        }

        [TestMethod]
        public void Let_will_return_Null_when_class_is_null()
        {
            TestableDummy td = null;

            Assert.IsNull(td.Let(c => c.DummyString));
        }

		[TestMethod]
		public void Let_on_Dictionary_returns_Value()
		{
			var testValue = "Erin Was here";
			var testKey = "tiger";
			var td = new TestableDummy
			{
				DummyString = testValue,
				DummyValueType = 5,
				DummyDictionary = new Dictionary<string, string>()
			};
			td.DummyDictionary[testKey] = testValue;

			Assert.AreEqual(td.DummyDictionary.Let(testKey), testValue);
		}

		[TestMethod]
		public void Let_on_Dictionary_Without_Key_Returns_Null()
		{
			var td = new TestableDummy
			{
				DummyDictionary = new Dictionary<string, string>()
			};

			Assert.IsNull(td.DummyDictionary.Let("TestValueLookup"));
		}

		[TestMethod]
		public void Let_on_null_Dictionary_will_Return_Null()
		{
			var td = new TestableDummy
			{
				DummyValueType = 5,
			};

			Assert.IsNull(td.DummyDictionary.Let("TestValueLookup"));
		}

        [TestMethod]
        public void Let_on_null_Dictionary_will_Return_Result_From_Fail_Func()
        {
            var td = new TestableDummy
            {
                DummyValueType = 5,
            };

            Assert.AreEqual(td.DummyDictionary.Let("TestValueLookup", () => "Erin"), "Erin");
        }

		[TestMethod]
		public void Return_on_a_class_returns_value()
		{
			var testValue = "Erin Was here";
			var td = new TestableDummy
			{
				DummyString = testValue,
				DummyValueType = 5
			};

			Assert.AreEqual(td.Let(c => c.DummyString, "monkey"), testValue);
		}

		[TestMethod]
		public void Return_on_a_class_returns_ValueType_value()
		{

			var td = new TestableDummy
			{
				DummyValueType = 5
			};
			Assert.AreEqual(td.Let(c => c.DummyValueType, 8), 5);
		}

		[TestMethod]
		public void Return_on_null_class_returns_failValue()
		{
			var testValue = "Erin Was here";
			var td = new TestableDummy
			{
				DummyValueType = 5
			};
			Assert.AreNotEqual(td.Let(c => c.DummyString, "monkey"), testValue);
		}

		[TestMethod]
		public void Return_on_a_class_returns_ValueType_failvalue()
		{

			var td = new TestableDummy
			{
				DummyString = "hi ho hi ho"
			};
			Assert.AreNotEqual(td.Let(c => c.DummyValueType, 8), 5);
		}

		[TestMethod]
		public void Return_on_Dictionary_will_return_Value()
		{
			var testValue = "Erin Was here";
			var testKey = "tiger";
			var td = new TestableDummy
			{
				DummyString = testValue,
				DummyValueType = 5,
				DummyDictionary = new Dictionary<string, string>()
			};
			td.DummyDictionary[testKey] = testValue;
			Assert.AreEqual(td.DummyDictionary.Let(testKey, "notTestValue"), testValue);
		}

		[TestMethod]
		public void Return_on_Dictionary_no_key_return_fail_value()
		{
			var testValue = "Erin Was here";
			var testKey = "tiger";
			var td = new TestableDummy
			{
				DummyString = testValue,
				DummyValueType = 5,
				DummyDictionary = new Dictionary<string, string>()
			};
			td.DummyDictionary[testKey] = testValue;
			Assert.AreNotEqual(td.DummyDictionary.Let("Fire", "notTestValue"), testValue);
		}

		[TestMethod]
		public void Return_on_null_dictionary_returns_null()
		{
			var testValue = "Erin Was here";
			var testKey = "tiger";
			var td = new TestableDummy
			{
				DummyString = testValue,
				DummyValueType = 5
			};
			Assert.AreNotEqual(td.DummyDictionary.Let(testKey, "notTestValue"), testValue);
		}

		[TestMethod]
		public void Each_on_IEnumerable()
		{
			var source = new[] { "a", "b", "c" };

			var items = new List<string>();
			source.Each(i => { items.Add(i); });

			Assert.AreEqual("a,b,c", String.Join(",", items));
		}


        [TestMethod]
        public void Each_on_IEnumerable_returns_value()
        {
            var source = new List<TestableDummy> { new TestableDummy { DummyString = "Hi" }, new TestableDummy { DummyString = "Hi" } };
            var result = source.Each(c => c.DummyString = c.DummyString + "One");
            Assert.IsTrue(result.All(c => c.DummyString == "HiOne"));            
        }

        [TestMethod]
        public void Let_on_IEnumerable_is_null()
        {
            string[] source = null;

            Assert.IsNull(source.Each(c => c.ToLowerInvariant()));
        }
        
        [TestMethod]
        public void IfDo_returns_value()
        {
            var source = new[] { "a", "b", "c" };

            var items = source.IfDo(c => c.Count() == 3, c => { });

            Assert.AreEqual("a,b,c", String.Join(",", items));
        }

        [TestMethod]
        public void IfDo_returns_null_when_given_null()
        {
            List<string> source = null;
            var items = source.IfDo(c => c.Count() == 3, c => { });
            Assert.IsNull(items);
        }

        [TestMethod]
        public void IfDo_does_do()
        {
            var source = new[] { "a", "b", "c" };
            var did = false;
            var didAgain = false;

            source
                .IfDo(c => c.Count() == 3, c => { did = true; })
                .IfDo(c => c.Count() > 4, c => { didAgain = true; });

            Assert.AreEqual(did, true);
            Assert.AreEqual(didAgain, false);
        }

        [TestMethod]
        public void If_returns_null() 
        {
            var td = new TestableDummy
            {
                DummyString = "bob"
            };
            var k = td.If(c => c.DummyString == "hi");
            Assert.IsNull(k);
        }

        [TestMethod]
        public void If_returns_TestItem()
        {
            var td = new TestableDummy
            {
                DummyString = "bob"
            };
            var k = td.If(c => c.DummyString == "bob");
            Assert.IsNotNull(k);
        }

        [TestMethod]
        public void If_returns_Null_when_given_Null()
        {
            TestableDummy td = null;            
            var k = td.If(c => c.DummyString == "bob");
            Assert.IsNull(k);
        }

        [TestMethod]
        public void Unless_returns_TestItem()
        {
            var td = new TestableDummy
            {
                DummyString = "bob"
            };
            var k = td.Unless(c => c.DummyString == "tom");
            Assert.IsNotNull(k);
        }

        [TestMethod]
        public void Unless_returns_Null_when_given_Null()
        {
            TestableDummy td = null;
            var k = td.Unless(c => c.DummyString == "bob");
            Assert.IsNull(k);
        }

        [TestMethod]
        public void Unless_returns_null()
        {
            var td = new TestableDummy
            {
                DummyString = "bob"
            };
            var k = td.Unless(c => c.DummyString == "bob");
            Assert.IsNull(k);
        }

        [TestMethod]
        public void Do_Returns_Null_When_Given_null()
        {
            TestableDummy td = null;
            Assert.IsNull(td.Do(c => c.DummyValueType = 5));            
        }

        [TestMethod]
        public void Do_Returns_Value_With_Update_State()
        {
            var td = new TestableDummy { DummyValueType = 4 };
            var p = td.Do(c => c.DummyValueType = 5);
            Assert.IsNotNull(p);
            Assert.AreEqual(p.DummyValueType, 5);
        }

        [TestMethod]
        public void Recover_Returns_Fail_Value_If_null() {
            TestableDummy td = null;
            Assert.IsNotNull(td.Recover(() => new TestableDummy { DummyValueType = 5 }));            
        }

        [TestMethod]
        public void Recover_Returns_Value_If_not_null()
        {
            TestableDummy td = new TestableDummy { DummyValueType = 5 };
            var p = td.Recover(() => new TestableDummy { DummyValueType = 6 });
            Assert.AreEqual(p.DummyValueType, 5);
        }
	}

	public class TestableDummy
	{
		public string DummyString { get; set; }
		public int DummyValueType { get; set; }
		public Dictionary<string, string> DummyDictionary { get; set; }
	}

}
