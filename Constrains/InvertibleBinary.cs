using System;
using System.Linq;
using System.Collections.Generic;

namespace CSPS {
	namespace Constrains {
		public class InvertibleBinary: AbstractConstrain {
			// TODO: local typedef?
			private Variable a, b, c;
			private Func<Value, Value, Value> ab_to_c, ac_to_b, bc_to_a;

			public InvertibleBinary(Variable a, Variable b, Variable c,
				Func<Value, Value, Value> ab_to_c,
				Func<Value, Value, Value> ac_to_b,
				Func<Value, Value, Value> bc_to_a) {
				this.a = a; this.b = b; this.c = c;
				this.ab_to_c = ab_to_c; this.ac_to_b = ac_to_b; this.bc_to_a = bc_to_a;
				OperatorName = "f";
			}

			public override IEnumerable<ConstrainResult> Propagate(IVariableAssignment assignment, IEnumerable<PropagationTrigger> triggers, ref IScratchpad scratchpad) {
				Log("Propagating");
				// TODO: AC with supports
				if (assignment[a].Assigned) {
					if (assignment[b].Assigned && !assignment[c].Assigned && ab_to_c != null) {
						Value A = assignment[a].Value, B = assignment[b].Value,
							C = ab_to_c(A, B);
						Log("a={0} b={1} ==> c={2}", A, B, C);
						return Assign(c, C);
					} else if (assignment[c].Assigned && !assignment[b].Assigned && ac_to_b != null) {
						Value A = assignment[a].Value, C = assignment[c].Value,
							B = ac_to_b(A, C);
						Log("a={0} c={2} ==> b={1}", A, B, C);
						return Assign(b, B);
					} else if (assignment[b].Assigned && assignment[c].Assigned) {
						Value A = assignment[a].Value, B = assignment[b].Value, C = assignment[c].Value;
						// TODO: tady bych nemel spolehat ze mam zrovna ab_to_c...
						if (Value.Equal(C, ab_to_c(A, B))) {
							Log("Success.");
							return Success;
						} else {
							Log("Failure!");
							return Failure;
						}
					}
				} else {
					if (assignment[b].Assigned && assignment[c].Assigned && bc_to_a != null) {
						Value B = assignment[b].Value, C = assignment[c].Value,
						    A = bc_to_a(B, C);
						Log("b={1} c={2} ==> a={0}", A, B, C);
						return Assign(a, A);
					}
				}
				return Nothing;
				// TODO: AC with supports
			}

			public override string Identifier {
				get {
					return string.Format("<{3}({0},{1})={2}>", a.Identifier, b.Identifier, c.Identifier, OperatorName);
				}
			}

			public string OperatorName {
				get; set;
			}

			public override List<Variable> Dependencies {
				get {
					return new List<Variable>() { a, b, c };
				}
			}
		}
	}
}
