using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GarbageCollector
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.Write("...čekám na nastavení Performace Counters\n\t(.NET Memory - #Gen X Collections, Gen X Heap Size, Finalization Survivors)");
			Console.ReadKey();

			Stopwatch stopwatch = Stopwatch.StartNew();
			long i = 0;
			while (true)
			{
				i++;
				using (MyClass mc = new MyClass(5000))
				{
					Console.WriteLine($"# {i:n0} in {stopwatch.ElapsedMilliseconds}ms = ({i / (stopwatch.ElapsedMilliseconds / 1000.0):n2}/s)");
				}
			}
		}
	}

	public class MyClass : IDisposable
	{
		byte[] data;

		public MyClass(int size)
		{
			data = Enumerable.Range(0, size).Select(i => (byte)(i % 256)).ToArray();
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		private void Dispose(bool disposing)
		{
			if (disposing)
			{
				// free managed resources
			}
			// free unmanaged resources
			Thread.Sleep(10); // 10 ms
			data = null;
		}

		~MyClass()
		{
			Dispose(false);
		}
	}
}
