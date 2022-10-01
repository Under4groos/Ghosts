using System;
using System.Runtime.CompilerServices;
using Sandbox.Internal;

namespace Sandbox
{
	// Token: 0x02000194 RID: 404
	public class PlanarReflection : ScenePortal
	{
		// Token: 0x060013ED RID: 5101 RVA: 0x0004FC7E File Offset: 0x0004DE7E
		public PlanarReflection(SceneWorld world, BBox renderBounds = default(BBox)) : base(world, PlanarReflection.CreateBoundingMeshModel(renderBounds), Transform.Zero, true, (int)Math.Min(512f, Math.Max(Screen.Width, Screen.Height)))
		{
		}

		// Token: 0x060013EE RID: 5102 RVA: 0x0004FCB0 File Offset: 0x0004DEB0
		[Description("Updates the reflection information before the render")]
		public void Update(Vector3 reflectionOffset, Vector3 reflectionNormal, float clipPlaneOffset = 0f)
		{
			if (!base.Parent.IsValid())
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				GlobalGameNamespace.Log.Warning("PlanarReflection does not have a Parent");
				return;
			}
			Vector3 planeOffset = reflectionNormal * clipPlaneOffset;
			Plane p = new Plane(reflectionOffset + planeOffset, reflectionNormal);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.SetClipPlane(p);
			Matrix viewMatrix = Matrix.CreateWorld(CurrentView.Position, CurrentView.Rotation.Forward, CurrentView.Rotation.Up);
			Vector3 reflectionPosition = this.ReflectMatrix(viewMatrix, p).Transform(CurrentView.Position);
			Rotation reflectionRotation = this.ReflectRotation(CurrentView.Rotation, reflectionNormal);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.ViewPosition = reflectionPosition;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.ViewRotation = reflectionRotation;
		}

		// Token: 0x060013EF RID: 5103 RVA: 0x0004FD68 File Offset: 0x0004DF68
		[Description("Update on the render thread")]
		public void OnRender()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.UpdateAspectRatio();
		}

		// Token: 0x060013F0 RID: 5104 RVA: 0x0004FD78 File Offset: 0x0004DF78
		[Description("Updates the aspect ratio of the reflection to match the view")]
		public void UpdateAspectRatio()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Aspect = Render.Viewport.Size.x / Render.Viewport.Size.y;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.FieldOfView = MathF.Atan(MathF.Tan(CurrentView.FieldOfView.DegreeToRadian() * 0.5f) * (base.Aspect * 0.75f)).RadianToDegree() * 2f;
		}

		// Token: 0x060013F1 RID: 5105 RVA: 0x0004FDF8 File Offset: 0x0004DFF8
		[Description("Creates a bounding mesh model for the water reflection. This way the water reflection scene will only be rendered when the water is visible in player view")]
		public static Model CreateBoundingMeshModel(BBox renderBounds = default(BBox))
		{
			if (renderBounds == default(BBox))
			{
				renderBounds = new BBox(new Vector3(-10000f, -10000f, -10000f), new Vector3(10000f, 10000f, 10000f));
			}
			Mesh mesh = new Mesh(Material.Load("debug/debugempty.vmat"), MeshPrimitiveType.Triangles);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			mesh.CreateVertexBuffer<SimpleVertex>(1, SimpleVertex.Layout, default(Span<SimpleVertex>));
			RuntimeHelpers.EnsureSufficientExecutionStack();
			mesh.Bounds = renderBounds;
			return Model.Builder.AddMesh(mesh).Create();
		}

		// Token: 0x060013F2 RID: 5106 RVA: 0x0004FE8C File Offset: 0x0004E08C
		[Description("Returns true if the renderer is currently rendering the world using a reflection view")]
		public static bool IsRenderingReflection()
		{
			return Render.Viewport.Size.x == Render.Viewport.Size.y;
		}

		// Token: 0x060013F3 RID: 5107 RVA: 0x0004FEC8 File Offset: 0x0004E0C8
		[Description("Returns a reflected matrix given a plane ( Reflection normal and distance )")]
		public Matrix ReflectMatrix(Matrix m, Plane plane)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			m.Numerics.M11 = 1f - 2f * plane.Normal.x * plane.Normal.x;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			m.Numerics.M21 = -2f * plane.Normal.x * plane.Normal.y;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			m.Numerics.M31 = -2f * plane.Normal.x * plane.Normal.z;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			m.Numerics.M41 = -2f * -plane.Distance * plane.Normal.x;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			m.Numerics.M12 = -2f * plane.Normal.y * plane.Normal.x;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			m.Numerics.M22 = 1f - 2f * plane.Normal.y * plane.Normal.y;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			m.Numerics.M32 = -2f * plane.Normal.y * plane.Normal.z;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			m.Numerics.M42 = -2f * -plane.Distance * plane.Normal.y;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			m.Numerics.M13 = -2f * plane.Normal.z * plane.Normal.x;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			m.Numerics.M23 = -2f * plane.Normal.z * plane.Normal.y;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			m.Numerics.M33 = 1f - 2f * plane.Normal.z * plane.Normal.z;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			m.Numerics.M43 = -2f * -plane.Distance * plane.Normal.z;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			m.Numerics.M14 = 0f;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			m.Numerics.M24 = 0f;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			m.Numerics.M34 = 0f;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			m.Numerics.M44 = 1f;
			return m;
		}

		// Token: 0x060013F4 RID: 5108 RVA: 0x00050171 File Offset: 0x0004E371
		[Description("Returns a reflected matrix given a reflection normal")]
		private Rotation ReflectRotation(Rotation source, Vector3 normal)
		{
			return Rotation.LookAt(Vector3.Reflect(source * Vector3.Forward, normal), Vector3.Reflect(source * Vector3.Up, normal));
		}
	}
}
