using System;

namespace Sandbox
{
	// Token: 0x0200016A RID: 362
	public interface IModelBreakCommand
	{
		// Token: 0x06001080 RID: 4224
		[Description("This will be called after an entity with this model breaks via <see cref=\"T:Sandbox.Breakables\">Breakables</see> class.")]
		void OnBreak(Breakables.Result result);
	}
}
