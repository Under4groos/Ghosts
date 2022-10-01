using System;
using System.Text.Json.Serialization;
using ModelDoc;

namespace Sandbox
{
	// Token: 0x02000155 RID: 341
	[GameData("nav_data")]
	[Description("Carries navigation related data.")]
	public class ModelNavData
	{
		// Token: 0x17000450 RID: 1104
		// (get) Token: 0x06000F8E RID: 3982 RVA: 0x0003DDDB File Offset: 0x0003BFDB
		// (set) Token: 0x06000F8F RID: 3983 RVA: 0x0003DDE3 File Offset: 0x0003BFE3
		[JsonPropertyName("nav_attribute_avoid")]
		[DefaultValue(false)]
		[Description("During map compile this model would mark its volume as an area that should be avoided by AI.")]
		public bool Avoid { get; set; }
	}
}
