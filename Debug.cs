using System;

namespace CSPS {
	static class Debug {
		public static bool doDebug = false;

		public static void Write(string fmt, params object[] args) {
			if (doDebug) {
				Console.Write(fmt, args);
			}
		}

		public static void WriteLine(string fmt, params object[] args) {
			if (doDebug) {
				Console.WriteLine(fmt, args);
			}
		}
	}
}
