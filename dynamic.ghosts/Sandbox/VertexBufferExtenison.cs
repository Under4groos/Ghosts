using System;
using System.Runtime.CompilerServices;

namespace Sandbox
{
	// Token: 0x0200015A RID: 346
	[Description("Extensions for Surfaces")]
	public static class VertexBufferExtenison
	{
		// Token: 0x06000FA8 RID: 4008 RVA: 0x0003E0C8 File Offset: 0x0003C2C8
		[Description("Add a vertex using this position and everything else from Default")]
		public static void Add(this VertexBuffer self, Vector3 pos)
		{
			Vertex v = self.Default;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			v.Position = pos;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			self.Add(v);
		}

		// Token: 0x06000FA9 RID: 4009 RVA: 0x0003E0F8 File Offset: 0x0003C2F8
		[Description("Add a vertex using this position and UV, and everything else from Default")]
		public static void Add(this VertexBuffer self, Vector3 pos, Vector2 uv)
		{
			Vertex v = self.Default;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			v.Position = pos;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			v.TexCoord0.x = uv.x;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			v.TexCoord0.y = uv.y;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			self.Add(v);
		}

		// Token: 0x06000FAA RID: 4010 RVA: 0x0003E155 File Offset: 0x0003C355
		[Description("Add a triangle to the vertex buffer. Will include indices if they're enabled.")]
		public static void AddTriangle(this VertexBuffer self, Vertex a, Vertex b, Vertex c)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			self.Add(a);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			self.Add(b);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			self.Add(c);
			if (self.Indexed)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				self.AddTriangleIndex(3, 2, 1);
			}
		}

		// Token: 0x06000FAB RID: 4011 RVA: 0x0003E194 File Offset: 0x0003C394
		[Description("Add a quad to the vertex buffer. Will include indices if they're enabled.")]
		public static void AddQuad(this VertexBuffer self, Rect rect)
		{
			Vector2 pos = rect.Position;
			Vector2 size = rect.Size;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			self.AddQuad(pos, new Vector2(pos.x + size.x, pos.y), pos + size, new Vector2(pos.x, pos.y + size.y));
		}

		// Token: 0x06000FAC RID: 4012 RVA: 0x0003E210 File Offset: 0x0003C410
		[Description("Add a quad to the vertex buffer. Will include indices if they're enabled.")]
		public static void AddQuad(this VertexBuffer self, Vertex a, Vertex b, Vertex c, Vertex d)
		{
			if (self.Indexed)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				self.Add(a);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				self.Add(b);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				self.Add(c);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				self.Add(d);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				self.AddTriangleIndex(4, 3, 2);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				self.AddTriangleIndex(2, 1, 4);
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			self.Add(a);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			self.Add(b);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			self.Add(c);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			self.Add(c);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			self.Add(d);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			self.Add(a);
		}

		// Token: 0x06000FAD RID: 4013 RVA: 0x0003E2BC File Offset: 0x0003C4BC
		[Description("Add a quad to the vertex buffer. Will include indices if they're enabled.")]
		public static void AddQuad(this VertexBuffer self, Vector3 a, Vector3 b, Vector3 c, Vector3 d)
		{
			if (self.Indexed)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				self.Add(a, new Vector2(0f, 0f));
				RuntimeHelpers.EnsureSufficientExecutionStack();
				self.Add(b, new Vector2(1f, 0f));
				RuntimeHelpers.EnsureSufficientExecutionStack();
				self.Add(c, new Vector2(1f, 1f));
				RuntimeHelpers.EnsureSufficientExecutionStack();
				self.Add(d, new Vector2(0f, 1f));
				RuntimeHelpers.EnsureSufficientExecutionStack();
				self.AddTriangleIndex(4, 3, 2);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				self.AddTriangleIndex(2, 1, 4);
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			self.Add(a, new Vector2(0f, 0f));
			RuntimeHelpers.EnsureSufficientExecutionStack();
			self.Add(b, new Vector2(1f, 0f));
			RuntimeHelpers.EnsureSufficientExecutionStack();
			self.Add(c, new Vector2(1f, 1f));
			RuntimeHelpers.EnsureSufficientExecutionStack();
			self.Add(c, new Vector2(1f, 1f));
			RuntimeHelpers.EnsureSufficientExecutionStack();
			self.Add(d, new Vector2(0f, 1f));
			RuntimeHelpers.EnsureSufficientExecutionStack();
			self.Add(a, new Vector2(0f, 0f));
		}

		// Token: 0x06000FAE RID: 4014 RVA: 0x0003E404 File Offset: 0x0003C604
		[Description("Add a quad to the vertex buffer. Will include indices if they're enabled.")]
		public static void AddQuad(this VertexBuffer self, Ray origin, Vector3 width, Vector3 height)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			self.Default.Normal = origin.Direction;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			Vector3 normal = width.Normal;
			self.Default.Tangent = new Vector4(ref normal, 1f);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			self.AddQuad(origin.Origin - width - height, origin.Origin + width - height, origin.Origin + width + height, origin.Origin - width + height);
		}

		// Token: 0x06000FAF RID: 4015 RVA: 0x0003E4A4 File Offset: 0x0003C6A4
		[Description("Add a cube to the vertex buffer. Will include indices if they're enabled.")]
		public static void AddCube(this VertexBuffer self, Vector3 center, Vector3 size, Rotation rot, Color32 color = default(Color32))
		{
			Color32 oldColor = self.Default.Color;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			self.Default.Color = color;
			Vector3 f = rot.Forward * size.x * 0.5f;
			Vector3 i = rot.Left * size.y * 0.5f;
			Vector3 u = rot.Up * size.z * 0.5f;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			self.AddQuad(new Ray(center + f, f.Normal), i, u);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			self.AddQuad(new Ray(center - f, -f.Normal), i, -u);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			self.AddQuad(new Ray(center + i, i.Normal), -f, u);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			self.AddQuad(new Ray(center - i, -i.Normal), f, u);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			self.AddQuad(new Ray(center + u, u.Normal), f, i);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			self.AddQuad(new Ray(center - u, -u.Normal), f, -i);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			self.Default.Color = oldColor;
		}
	}
}
