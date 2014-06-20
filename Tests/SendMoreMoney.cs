using System;

namespace CSPS {
	namespace Tests {
		public class SendMoreMoney: Test {
			public override void Run() {
				Problem problem = new Problem();

				//   S E N D
				// + M O R E
				// =========
				// M O N E Y
				//
				// Variables: S, E, N, D, M, O, R, Y

				string[] names = "S E N D M O R Y".Split();
				Variable[] v = problem.Variables.AddIntegers(8, 0, 10, x => names[x]);

				Variable S = v[0], E = v[1], N = v[2], D = v[3], M = v[4], O = v[5], R = v[6], Y = v[7];

				problem.Constrains.Add(Constrain.AllDifferent(v));

				// problem.Constrains.Add(Constrain.Plus(N, D, M));
				// problem.Constrains.Add(Constrain.Plus(E, N, D));
				// problem.Constrains.Add(Constrain.Plus(Y, E, N));
				//
				// problem.Constrains.Add(
				//	Constrain.Equal(
				//		(S * 1000 + E * 100 + N * 10 + D) + (M * 1000 + O * 100 + R * 10 + E),
				//		M * 10000 + O * 1000 + N * 100 + E * 10 + Y
				//	)
				// );

				// TODO: dalsi test: maximalizace
				Solver solver = new Solver();
				IVariableAssignment result;

				Assert(solver.Solve(problem, out result));
				foreach (var variable in v) {
					Console.WriteLine("{0} <= {1}", variable.Identifier, result[variable].Value);
				}
			}
		}
	}
}
