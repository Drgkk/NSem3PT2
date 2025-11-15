using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSem3PT2
{

    public class QueueNode
    {
        public double value;
        public QueueNode next;
        public QueueNode prev;

        public QueueNode(double value)
        {
            this.value = value;
            this.next = null;
            this.prev = null;
        }

        public QueueNode(QueueNode vnCopy) : this(vnCopy.value)
        {

        }

        public override string ToString()
        {
            return "(" + value + ")";
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is QueueNode)) return false;
            QueueNode vN = (QueueNode)obj;
            if (value == vN.value)
                return true;
            else
                return false;
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }
    }
    public class QueueList
    {
        

        QueueNode first;
        QueueNode last;
        private int size;

        public QueueList()
        {
            first = null;
            size = 0;
        }

        public QueueList(params double[] arr)
        {
            if (arr == null) return;
            for (int i = 0; i < arr.Length; i++)
            {
                Add(arr[i]);
            }
        }

        public QueueList(QueueList vlCopy)
        {
            if (vlCopy == null || vlCopy.size == 0) return;
            QueueNode iter = vlCopy.first;
            for (int i = 0; i < vlCopy.size; i++)
            {
                Add(iter.value);
                iter = iter.next;
            }
        }

        public void Add(double value)
        {
            QueueNode newNode = new QueueNode(value);
            if (first == null)
            {
                first = newNode;
                last = newNode;
                newNode.next = first;
                newNode.prev = first;
            }
            else
            {
                newNode.next = first;
                newNode.prev = last;
                last.next = newNode;
                first.prev = newNode;
                last = newNode;
            }
            size++;
        }

        public QueueNode RemoveHead()
        {
            QueueNode tempFirst = first;
            if (size == 1)
            {
                first = null;
                last = null;
                size--;
                tempFirst.next = null;
                tempFirst.prev = null;
                return tempFirst;
            }

            QueueNode temp = first.next;
            first.next.prev = last;
            last.next = first.next;
            first.next = null;
            first.prev = null;

            first = temp;
            size--;
            tempFirst.next = null;
            tempFirst.prev = null;
            return tempFirst;
        }


        public double this[int coord]
        {
            get
            {
                if (coord < 0 || coord >= size) throw new IndexOutOfRangeException();
                QueueNode iter = first;
                for (int i = 0; i < coord; i++)
                {
                    iter = iter.next;
                }
                return iter.value;
            }
            set
            {
                if (coord < 0 || coord >= size) throw new IndexOutOfRangeException();
                QueueNode iter = first;
                for (int i = 0; i < coord; i++)
                {
                    iter = iter.next;
                }

                iter.value = value;
            }
        }

        public override string ToString()
        {
            string text = "{";
            if (first != null)
            {
                QueueNode iterNode = first;
                for (int i = 0; i < size; i++)
                {
                    text += iterNode.ToString() + ", ";
                    iterNode = iterNode.next;
                }
            }


            text += "}";

            return text;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is QueueList)) return false;
            QueueList vL = (QueueList)obj;
            if (this.size != vL.size)
                return false;
            if (this.size == 0 && vL.size == 0) return true;
            QueueNode thisIterNode = first;
            QueueNode otherIterNode = vL.first;
            for (int i = 0; i < size; i++)
            {
                if (!thisIterNode.Equals(otherIterNode)) return false;
                thisIterNode = thisIterNode.next;
                otherIterNode = otherIterNode.next;
            }
            return true;
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }
    }

    public class Queue : IQueue
    {
        private QueueList list;
        public int size { get; set; }

        public Queue()
        {
            list = new QueueList();
            size = 0;
        }

        public Queue(params double[] arr)
        {
            if (arr == null) return;
            list = new QueueList(arr);
            size = arr.Length;
        }

        public Queue(Queue qCopy)
        {
            if (qCopy == null || qCopy.size == 0) return;
            this.size = qCopy.size;
            this.list = new QueueList(qCopy.list);
        }

        public double this[int coord]
        {
            get
            {
                return list[coord];
            }
            set
            {
                list[coord] = value;
            }
        }

        public double ReadHead()
        {
            return list[0];
        }

        public override string ToString()
        {
            return "Size: " + size + " " + list.ToString();
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Queue)) return false;
            Queue vL = (Queue)obj;
            if (this.size != vL.size)
                return false;
            if (this.size == 0 && vL.size == 0) return true;
            if (this.list == null && vL.list == null) return true;
            if (this.list == null || vL.list == null) return false;
            if (!this.list.Equals(vL.list)) return false;
            return true;
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }


        public static Queue operator >(Queue q, QueueNode v)
        {
            if (q.size == 0) throw new ArgumentNullException();
            v.value = q.list.RemoveHead().value;
            q.size--;
            return q;
        }

        public static Queue operator <(Queue q, QueueNode v)
        {
            q.list.Add(v.value);
            q.size++;
            return q;
        }

        public static bool operator true(Queue q)
        {
            return q.size != 0;
        }

        public static bool operator false(Queue q)
        {
            return q.size == 0;
        }

        public static bool operator !(Queue q)
        {
            return !(q ? true : false);
        }

        public static bool operator ==(Queue q, Queue v)
        {
            return q.Equals(v);
        }

        public static bool operator !=(Queue q, Queue v)
        {
            return !(q == v);
        }

        public static explicit operator double[](Queue q)
        {
            if (q.size == 0) throw new ArgumentNullException();
            double[] arr = new double[q.size];
            for (int i = 0; i < q.size; i++)
            {
                arr[i] = q.list[i];
            }
            return arr;
        }

        public static implicit operator Queue(double[] arr)
        {
            if (arr == null) throw new ArgumentNullException();
            Queue q = new Queue();
            for (int i = 0; i < arr.Length; i++)
            {
                q = q < new QueueNode(arr[i]);
            }
            return q;
        }

    }
}
