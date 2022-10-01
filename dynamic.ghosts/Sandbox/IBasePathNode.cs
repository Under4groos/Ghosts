using System;

namespace Sandbox
{
	// Token: 0x02000171 RID: 369
	public interface IBasePathNode
	{
		// Token: 0x170004A2 RID: 1186
		// (get) Token: 0x060010E1 RID: 4321
		[Description("Node's incoming tangent in world space coordinates.")]
		Vector3 WorldTangentIn { get; }

		// Token: 0x170004A3 RID: 1187
		// (get) Token: 0x060010E2 RID: 4322
		[Description("Node's outgoing tangent in world space coordinates.")]
		Vector3 WorldTangentOut { get; }

		// Token: 0x170004A4 RID: 1188
		// (get) Token: 0x060010E3 RID: 4323
		[Description("Node's position in world space coordinates.")]
		Vector3 WorldPosition { get; }
	}
}
