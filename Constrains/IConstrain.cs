using System.Collections;
using System.Collections.Generic;

namespace CSPS {
	namespace Constrains {
		public interface IConstrain {
			IEnumerable<ConstrainResult> Propagate(IVariableAssignment assignment, IEnumerable<PropagationTrigger> triggers, ref IScratchpad scratchpad);
			bool Satisfied(IVariableAssignment assignment);
			List<Variable> Dependencies { get; } // Constant over constrain lifetime.
			string Identifier { get; }
		};
	}
};