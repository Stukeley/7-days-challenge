using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Sorting_Algorithms_Comparison
{
	// Note - all sorts will be ascending
	internal class Program
	{
		private static void Main()
		{
			Console.WriteLine("Welcome. Press 1 to run the algorithm for pre-set values, or 2 if you want to input your own.");
			int c = Console.Read();

			int[] arr;

			// Even though c is int, it's the ASCII code of the input character. To get numerical value, subtract 48 from it
			if (c - 48 == 2)
			{
				// Input
				Console.WriteLine("Please input any integer then press enter, as many times as you want. Input anything else to stop.");

				// List to store temporary values that will later be made into an array
				var list = new List<int>();

				while (true)
				{
					string input = Console.ReadLine();

					bool parsed = int.TryParse(input, out int number);

					if (parsed)
					{
						list.Add(number);
					}
					else
					{
						if (list.Count == 0)
						{
							Console.WriteLine("Nothing was input! The algorithm will stop now.");
							return;
						}

						arr = new int[list.Count];

						for (int i = 0; i < list.Count; i++)
						{
							arr[i] = list[i];
						}

						break;
					}
				}
			}
			else
			{
				// No input - anything other than 2, really
				// We'll assume 1000 random values
				arr = new int[10000];

				var rng = new Random();

				for (int i = 0; i < 10000; i++)
				{
					arr[i] = rng.Next();
				}
			}

			// Invoke the methods one by one, measure time it takes to complete
			// Note - for recursive sorts we need to start the timer here, not inside of the function

			BubbleSort(arr);

			InsertSort(arr);

			BucketSort(arr);

			// Quick sort setup

			var stopwatch = new Stopwatch();
			stopwatch.Start();
			QuickSort(arr, 0, arr.Length - 1);
			stopwatch.Stop();

			Console.WriteLine($"Quick sort complete! Time elapsed: {stopwatch.Elapsed}");
		}

		// Setup function - swaps two integer values
		private static void Swap(ref int a, ref int b)
		{
			int temp = a;
			a = b;
			b = temp;
		}

		// Bubble sort - expected to be the least effective
		private static void BubbleSort(int[] arr)
		{
			var stopwatch = new Stopwatch();
			stopwatch.Start();

			int n = arr.Length;
			do
			{
				for (int i = 0; i < n - 1; i++)
				{
					if (arr[i] > arr[i + 1])
					{
						Swap(ref arr[i], ref arr[i + 1]);
					}
				}
				n--;
			}
			while (n > 1);

			stopwatch.Stop();

			Console.WriteLine($"Bubble sort complete! Time elapsed: {stopwatch.Elapsed}");
		}

		private static void InsertSort(int[] arr)
		{
			var stopwatch = new Stopwatch();
			stopwatch.Start();

			int n = arr.Length;

			for (int i = 0; i < n - 1; i++)
			{
				for (int j = i + 1; j > 0; j--)
				{
					if (arr[i] > arr[j])
					{
						Swap(ref arr[i], ref arr[j]);
					}
				}
			}

			stopwatch.Stop();

			Console.WriteLine($"Insertion sort complete! Time elapsed: {stopwatch.Elapsed}");
		}

		private static void BucketSort(int[] arr, int numberOfBuckets = 10)
		{
			var stopwatch = new Stopwatch();
			stopwatch.Start();

			var result = new List<int>();

			// Let's assume 10 buckets
			var bucket = new List<int>[numberOfBuckets];

			for (int i = 0; i < numberOfBuckets; i++)
			{
				bucket[i] = new List<int>();
			}

			for (int i = 0; i < arr.Length; i++)
			{
				int index = arr[i] % numberOfBuckets;
				bucket[index].Add(arr[i]);
			}

			for (int i = 0; i < numberOfBuckets; i++)
			{
				bucket[i].Sort();
				result.AddRange(bucket[i]);
			}

			stopwatch.Stop();

			Console.WriteLine($"Bucket sort complete! Time elapsed: {stopwatch.Elapsed}");
		}

		private static void QuickSort(int[] arr, int left, int right)
		{
			int i = left;
			int j = right;
			int pivot = arr[(left + right) / 2];

			while (i < j)
			{
				while (arr[i] < pivot)
				{
					i++;
				}
				while (arr[j] > pivot)
				{
					j--;
				}

				Swap(ref arr[i], ref arr[j]);
				i++;
				j--;
			}

			if (left < j)
			{
				QuickSort(arr, left, j);
			}

			if (right > i)
			{
				QuickSort(arr, i, right);
			}
		}
	}
}
