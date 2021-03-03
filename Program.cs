using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;

namespace Algorithms_lab2
{
    public class BinarySearchTree<T>
    {

        // Class containing left and 
        // right child of current node
        // and key value
        public class Node<T>
        {
            public int key;
            public Node<T> left, right;

            public Node(int item)
            {
                key = item;
                left = right = null;
            }
        }

        // Root of BST
        Node<T> root;
        int sum = 0;
        // Constructor
        public BinarySearchTree()
        {
            root = null;
        }

        // This method mainly calls insertRec()
        public void Insert(int key)
        {
            root = InsertRec(root, key);
        }
        public int FindSum()
        {
            sum = 0;
            if (root == null)
                return 0;
            return FindSumRec(root);
        }
        int FindSumRec(Node<T> root)
        {
            sum += root.key;
            if (root.left != null)
                FindSumRec(root.left);
            if (root.right != null)
                FindSumRec(root.right);
            return sum;
        }
        // A recursive function to insert 
        // a new key in BST 
        Node<T> InsertRec(Node<T> root, int key)
        {

            // If the tree is empty, 
            // return a new node 
            if (root == null)
            {
                root = new Node<T>(key);
                return root;
            }

            // Otherwise, recur down the tree 
            if (key < root.key)
                root.left = InsertRec(root.left, key);
            else if (key > root.key)
                root.right = InsertRec(root.right, key);

            // Return the (unchanged) node pointer 
            return root;
        }

        // This method mainly calls InorderRec()
        public void Inorder()
        {
            InorderRec(root);
        }

        // A utility function to 
        // do inorder traversal of BST
        void InorderRec(Node<T> root)
        {
            if (root != null)
            {
                InorderRec(root.left);
                Console.WriteLine(root.key);
                InorderRec(root.right);
            }
        }
        int CompareTwoValues(int t, int d)
        {
            if (t > d)
            {
                return 1;
            }
            else if (t < d)
            {
                return -1;
            }
            else
            {
                return 0;
            }
        }
        public void Delete(int t)
        {
            root = DeleteByRecursion(root, t);
        }
        Node<T> DeleteByRecursion(Node<T> root, int key)
        {
            if (root == null)
                return root;

            if (CompareTwoValues(root.key, key) == 1)
                root.left = DeleteByRecursion(root.left, key);
            else if (CompareTwoValues(root.key, key) == -1)
                root.right = DeleteByRecursion(root.right, key);
            else {
                // node contains only one child
                if (root.left == null)
                    return root.right;
                else if (root.right == null)
                    return root.left;
                root.key = MinValue(root.right);
                root.right = DeleteByRecursion(root.right, root.key);
            }
            return root;
        }
    
        int MinValue(Node<T> root)
        {
            //initially minval = root
            int minval = root.key;
            //find minval
            while (root.left != null)
            {
                minval = root.left.key;
                root = root.left;
            }
            return minval;
        }
        public Node<T> SearchByValue(int t)
        {
            return SearchByValue(root, t);
        }

        Node<T> SearchByValue(Node<T> current, int value)
        {
            if (current == null)
            {
                return null;
            }
            if (value == current.key)
            {
                return current;
            }
            return CompareTwoValues(current.key, value) == 1
                    ? SearchByValue(current.left, value)
                    : SearchByValue(current.right, value);
        }

        


    }
    class Test
    {
        public static BinarySearchTree<int> tree = new BinarySearchTree<int>();
        public static void Main(string[] args)
        {

            int[] array = new int[10000];
            Random rand = new Random();

            for (int i = 0; i < array.Length; i++)
                array[i] = i;

            Console.WriteLine("Тестирование наихудшего случая:");
            TestInsert(array);
            TestDelete(array);
            TestSum();
            TestFind(array);

            for (int i = 0; i < array.Length; i++)
                array[i] = rand.Next(-20,20);

            Console.WriteLine();

            Console.WriteLine("Тестирование среднего случая:");
            TestInsert(array);
            TestDelete(array);
            TestSum();
            TestFind(array);

            for (int i = 0; i < array.Length; i++)
                array[i] = 1;

            Console.WriteLine();

            Console.WriteLine("Тестирование наилучшего случая:");
            TestInsert(array);
            TestDelete(array);
            TestSum();
            TestFind(array);

        }
        public static void TestInsert(int[] arr)
        {
            Stopwatch stopwatch = new Stopwatch();

            stopwatch.Start();
            for (int i = 0; i < arr.Length; i++)
            {
                tree.Insert(arr[i]);
            }
            stopwatch.Stop();
            Console.WriteLine("Время на добавление: " + stopwatch.ElapsedTicks * 100 + "ns");
        }
        public static void TestDelete(int[] arr)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            for (int i = 0; i < arr.Length; i++)
            {
                tree.Delete(arr[i]);
            }
            stopwatch.Stop();
            Console.WriteLine("Время на удаление: " + stopwatch.ElapsedTicks * 100 + "ns");
        }
        public static void TestSum()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            tree.FindSum();
            stopwatch.Stop();
            Console.WriteLine("Время на на подсчет суммы: " + stopwatch.ElapsedTicks * 100 + "ns");
        }
        public static void TestFind(int[] arr)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            for (int i = 0; i < arr.Length; i++)
            {
                tree.SearchByValue(arr[i]);
            }
            stopwatch.Stop();
            Console.WriteLine("Время на удаление: " + stopwatch.ElapsedTicks * 100 + "ns");
        }
    }
}
