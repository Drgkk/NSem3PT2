using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using NSem3PT2;

namespace UnitTestProject1
{
    [TestClass]
    public class QueueListTests
    {
        /// <summary>
        /// Checks that default constructor creates an empty list
        /// </summary>
        [TestMethod]
        public void DefaultConstructor_CreatesEmptyList()
        {
            //arrange
            //act
            QueueList ql = new QueueList();
            //assert
            Assert.AreEqual("{}", ql.ToString());
            Assert.IsFalse(ql.Equals(null));
        }

        /// <summary>
        /// Checks that params constructor works
        /// </summary>
        [TestMethod]
        public void ParamsConstructor_CreatesListWithElementsAndIndexer()
        {
            //arrange
            //act
            QueueList ql = new QueueList(1.0, 2.0, 3.0);
            //assert
            Assert.AreEqual("{, (1), (2), (3)}", ql.ToString());
        }

        /// <summary>
        /// Checks that indexers work
        /// </summary>
        [TestMethod]
        public void Indexer_GetElement_GetsCorrectElement()
        {
            //arrange
            QueueList ql = new QueueList(1.0, 2.0, 3.0);
            //act
            //assert
            Assert.AreEqual(1.0, ql[0]);
            Assert.AreEqual(2.0, ql[1]);
            Assert.AreEqual(3.0, ql[2]);
        }

        

        /// <summary>
        /// Check that copy constructor creates an independent copy
        /// </summary>
        [TestMethod]
        public void CopyConstructor_CreatesIndependentCopy()
        {
            //arrange
            QueueList original = new QueueList(5.0, 6.0);
            QueueList copy = new QueueList(original);
            //act
            QueueNode removed = original.RemoveHead();
            //assert
            Assert.AreEqual("{, (5), (6)}", copy.ToString());
            Assert.AreEqual("{, (6)}", original.ToString());
            Assert.IsNull(removed.next);
        }

        /// <summary>
        /// Check Add elements correctly adds them
        /// </summary>
        [TestMethod]
        public void Add_AddElements_WorksCorrectlyAcrossMultipleCalls()
        {
            //arrange
            QueueList ql = new QueueList();
            //act
            ql.Add(10.0);
            ql.Add(20.0);
            ql.Add(30.0);
            //assert
            Assert.AreEqual("{, (10), (20), (30)}", ql.ToString());
        }

        /// <summary>
        /// Check RemoveHead correctly removes the first element
        /// </summary>
        [TestMethod]
        public void RemoveHead_RemoveFirstElement_Works()
        {
            //arrange
            QueueList ql = new QueueList();
            //act
            ql.Add(10.0);
            ql.Add(20.0);
            ql.Add(30.0);
            QueueNode qN = ql.RemoveHead();
            //assert
            Assert.AreEqual("{, (20), (30)}", ql.ToString());
            Assert.AreEqual(10, qN.value);
        }


        /// <summary>
        /// Check that get indexer throws exceptions at incorrect indexes
        /// </summary>
        [TestMethod]
        public void Indexer_Get_ThrowsOnInvalidIndex()
        {
            //arrange
            QueueList ql = new QueueList();
            //act
            //assert
            Assert.ThrowsException<IndexOutOfRangeException>(() =>
            {
                double d = ql[0];
            });
            Assert.ThrowsException<IndexOutOfRangeException>(() =>
            {
                double d =  ql[-1];
            });
        }

        /// <summary>
        /// Check Equals for empty lists
        /// </summary>
        [TestMethod]
        public void Equals_EmptyLists_BehaveCorrectly()
        {
            //arrange
            QueueList a = new QueueList();
            QueueList b = new QueueList();
            //act
            //assert
            Assert.IsTrue(a.Equals(b));
        }

        /// <summary>
        /// Check Equals for same and different lists
        /// </summary>
        [TestMethod]
        public void Equals_SameAndDifferentLists_BehaveCorrectly()
        {
            //arrange
            QueueList a = new QueueList(1, 2, 3);
            QueueList b = new QueueList(1, 2, 3);
            QueueList c = new QueueList(3, 2, 1);
            //act
            //assert
            Assert.IsTrue(a.Equals(b));
            Assert.IsFalse(a.Equals(c));
        }
    }

    [TestClass]
    public class QueueTests
    {
        /// <summary>
        /// Check that default constructor creates an empty list
        /// </summary>
        [TestMethod]
        public void DefaultConstructor_CreatesEmptyQueue()
        {
            //arrange
            //act
            Queue q = new Queue();
            //assert
            Assert.AreEqual("Size: 0 {}", q.ToString());
            Assert.IsTrue(!q);
            Assert.IsFalse(q ? true : false);
        }

        /// <summary>
        /// Check that empty lists returns false and non-empty true
        /// </summary>
        [TestMethod]
        public void BooleanOperators_EmptyAndNonEmptyLists_Works()
        {
            //arrange
            Queue q1 = new Queue();
            Queue q2 = new Queue(1, 2, 3);
            //act
            //assert
            Assert.IsFalse(q1 ? true : false);
            Assert.IsTrue(q2 ? true : false);
        }

        /// <summary>
        /// Check that ! operator reverses boolean value
        /// </summary>
        [TestMethod]
        public void ExclamationMarkOperator_EmptyAndNonEmptyLists_ReversesBooleanValues()
        {
            //arrange
            Queue q1 = new Queue();
            Queue q2 = new Queue(1, 2, 3);
            //act
            //assert
            Assert.IsTrue(!q1);
            Assert.IsFalse(!q2);
        }


        /// <summary>
        /// Check Params Constructor
        /// </summary>
        [TestMethod]
        public void ParamsConstructor_CreatesQueue()
        {
            //arrange
            //act
            Queue q = new Queue(1.0, 2.0);
            //assert
            Assert.AreEqual(2, q.size);
            Assert.AreEqual("Size: 2 {, (1), (2)}", q.ToString());
        }

        /// <summary>
        /// Check Params Constructor
        /// </summary>
        [TestMethod]
        public void ReadHead_NonEmptyList_ReadsHeadAndDoesNotRemoveIt()
        {
            //arrange
            Queue q = new Queue(1.0, 2.0);
            //act
            double d = q.ReadHead();
            //assert
            Assert.AreEqual(2, q.size);
            Assert.AreEqual("Size: 2 {, (1), (2)}", q.ToString());
            Assert.AreEqual(1.0, d);
        }

        /// <summary>
        /// Check that copy constructor creates an independent copy
        /// </summary>
        [TestMethod]
        public void CopyConstructor_CopiesQueueIndependently()
        {
            //arrange
            Queue original = new Queue(7.0, 8.0);
            //act
            Queue copy = new Queue(original);
            //assert
            Assert.AreEqual(original.size, copy.size);
            Assert.AreEqual(original.ToString(), copy.ToString());

            //act
            QueueNode temp = new QueueNode(0.0);
            original = original > temp;
            //assert
            Assert.AreEqual("Size: 2 {, (7), (8)}", copy.ToString());
            Assert.AreEqual(1, original.size);
        }

       

        /// <summary>
        /// Check '<' operator behaviour
        /// </summary>
        [TestMethod]
        public void InsertOperator_Enqueue_Work()
        {
            //arrange
            Queue q = new Queue(100.0, 200.0);
            //act
            q = q < new QueueNode(300.0); // enqueue 300
            //assert
            Assert.AreEqual(3, q.size);
            Assert.AreEqual("Size: 3 {, (100), (200), (300)}", q.ToString());
        }

        /// <summary>
        /// Check '>' operator behaviour
        /// </summary>
        [TestMethod]
        public void RemoveOperator_Dequeue_Work()
        {
            //arrange
            Queue q = new Queue(100.0, 200.0);
            //act
            QueueNode receiver = new QueueNode(-1.0);
            q = q > receiver;
            //assert
            Assert.AreEqual(100.0, receiver.value);
            Assert.AreEqual(1, q.size);
            Assert.AreEqual("Size: 1 {, (200)}", q.ToString());
        }

        /// <summary>
        /// Check '>' operator on empty Queue throws ArgumentNullException
        /// </summary>
        [TestMethod]
        public void RemoveOperator_EmptyQueue_ThrowsArgumentNullException()
        {
            //arrange
            Queue empty = new Queue();
            //act
            //assert
            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                QueueNode qN = new QueueNode(0);
                empty = empty > qN;
            });
        }

        /// <summary>
        /// Check '<' operator on empty Queue correctly works
        /// </summary>
        [TestMethod]
        public void InsertOperator_EmptyQueue_Works()
        {
            //arrange
            Queue empty = new Queue();
            //act
            empty = empty < new QueueNode(50.5);
            //assert
            Assert.AreEqual(1, empty.size);
            Assert.AreEqual("Size: 1 {, (50,5)}", empty.ToString());
        }


        /// <summary>
        /// Check Explicit Queue to Array Conversion works
        /// </summary>
        [TestMethod]
        public void ExplicitQueueToArrayConversion_ConvertQueueToArray_Work()
        {
            //arrange
            Queue q = new Queue(4.0, 5.0);
            //act
            double[] arr = (double[])q;
            //assert
            CollectionAssert.AreEqual(new double[] { 4.0, 5.0 }, arr);
        }

        /// <summary>
        /// Check that explicit Empty Queue to Array conversion throws ArgumentNullException
        /// </summary>
        [TestMethod]
        public void ExplicitQueueToArrayConversion_ConvertEmptyQueueToArray_ThrowsArgumentNullException()
        {
            //arrange
            Queue q = new Queue();
            //act
            //assert
            Assert.ThrowsException<ArgumentNullException>(() => (double[])q);
            
        }

        /// <summary>
        /// Check Implicit Array to Queue Conversion works
        /// </summary>
        [TestMethod]
        public void ImplicitArrayToQueueConversion_ConvertArrayToQueue_Work()
        {
            //arrange
            Queue q2 = new Queue();
            double[] input = new double[] { 9.0, 8.0 };
            //act
            q2 = input;
            //arrange
            Assert.AreEqual(2, q2.size);
            Assert.AreEqual("Size: 2 {, (9), (8)}", q2.ToString());
        }

        /// <summary>
        /// Check that explicit Null Array to Queue conversion throws ArgumentNullException
        /// </summary>
        [TestMethod]
        public void ImplicitArrayToQueueConversion_ConvertNullArrayToQueue_ThrowsArgumentNullException()
        {
            //arrange
            double[] arr = null;
            //act
            //assert
            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                Queue q = arr;
            });

        }

        /// <summary>
        /// Check that == and != operators work, also with nulls
        /// </summary>
        [TestMethod]
        public void EqualsAndEqualityOperators_QueuesAndNulls_WorkCorrectly()
        {
            //arrange
            Queue a = new Queue(1.0, 2.0);
            Queue b = new Queue(1.0, 2.0);
            Queue c = new Queue(2.0, 3.0);
            //act
            //assert
            Assert.IsTrue(a == b);
            Assert.IsFalse(a != b);
            Assert.IsFalse(a == c);
            Assert.IsFalse(a == null);
            Assert.IsTrue(a != null);
        }
    }
}
