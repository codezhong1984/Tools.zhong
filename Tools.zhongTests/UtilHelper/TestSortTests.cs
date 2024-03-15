using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tools.zhong.UtilHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tools.zhong.UtilHelper.Tests
{
    [TestClass()]
    public class TestSortTests
    {
        [TestMethod()]
        public void TestSort()
        {
            int[] list = { 20, 30, 10, 5, 98, 23, 45, 23, 12 };

            //冒泡
            for (int i = 0; i < list.Length - 1; i++)
            {
                for (int j = 0; j < list.Length - 1; j++)
                {
                    if (list[j] > list[j + 1])
                    {
                        var tempVal = list[j];
                        list[j] = list[j + 1];
                        list[j + 1] = tempVal;
                    }
                }
            }

            //选择排序
            int[] list2 = { 20, 30, 10, 5, 98, 23, 45, 23, 12 };
            for (int i = 0; i < list2.Length; i++)
            {
                var minIndex = i;
                for (int j = i + 1; j < list2.Length; j++)
                {
                    if (list2[minIndex] > list2[j])
                    {
                        minIndex = j;
                    }
                }
                if (i != minIndex)
                {
                    var tempVal = list2[i];
                    list2[i] = list2[minIndex];
                    list2[minIndex] = tempVal;
                }
            }

            //快速排序
            int[] arr = { 12, 30, 10, 5, 98, 23, 45, 23, 20 };
            QuickSort(arr, 0, arr.Length - 1);

            Assert.IsTrue(true);
        }

        private static void QuickSort(int[] arr, int left, int right)
        {
            if (left < right)
            {
                // 选取基准元素并将其放到正确位置上
                int pivotIndex = Partition(arr, left, right);

                // 对左侧子数组进行快速排序
                QuickSort(arr, left, pivotIndex - 1);

                // 对右侧子数组进行快速排序
                QuickSort(arr, pivotIndex + 1, right);
            }
        }

        private static int Partition(int[] arr, int left, int right)
        {
            int pivot = arr[right];
            int i = left;

            for (int j = left; j <= right; j++)
            {
                if (arr[j] < pivot)
                {
                    Swap(ref arr[i], ref arr[j]);
                    i++;
                }
            }

            Swap(ref arr[i], ref arr[right]);

            return i;
        }

        private static void Swap(ref int a, ref int b)
        {
            int temp = a;
            a = b;
            b = temp;
        }

        private static void PrintArray(int[] arr)
        {
            foreach (var num in arr)
            {
                Console.Write($"{num} ");
            }
            Console.WriteLine();
        }
    }
}