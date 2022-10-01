using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Sandbox.Internal;

namespace Sandbox
{
	// Token: 0x0200019A RID: 410
	public class WaterSceneObject : SceneCustomObject
	{
		// Token: 0x1700059C RID: 1436
		// (get) Token: 0x06001464 RID: 5220 RVA: 0x00052CDB File Offset: 0x00050EDB
		public Vector3 Velocity
		{
			get
			{
				return this._lastPosition - base.Position;
			}
		}

		// Token: 0x06001465 RID: 5221 RVA: 0x00052CEE File Offset: 0x00050EEE
		public WaterSceneObject(SceneWorld world, Water parent, Material material) : base(world)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.WaterMaterial = material;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.WaterParent = parent;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.CreateChildren();
		}

		// Token: 0x06001466 RID: 5222 RVA: 0x00052D1C File Offset: 0x00050F1C
		internal void CreateChildren()
		{
			if (this.WaterParent.EnableReflection)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.WaterReflection = new PlanarReflection(base.World, this.RenderBounds);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				base.AddChild("WaterReflection", this.WaterReflection);
			}
			if (this.WaterParent.EnableRipples)
			{
				this.WaterRipple = new WaterSceneObject.RippleCompute(this);
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.EnableRefraction = this.WaterParent.EnableReflection;
		}

		// Token: 0x06001467 RID: 5223 RVA: 0x00052D98 File Offset: 0x00050F98
		[Description("Updates that are carried outside render thread")]
		public virtual void Update()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this._lastPosition = base.Position;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Transform = this.WaterParent.Transform;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Position = this.WaterParent.Position;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Bounds = this.WaterParent.CollisionBounds + this.WaterParent.Position;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.RenderBounds = this.WaterParent.CollisionBounds + this.WaterParent.Position;
			if (this.WaterReflection != null)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.WaterReflection.Bounds = this.RenderBounds;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				PlanarReflection waterReflection = this.WaterReflection;
				if (waterReflection != null)
				{
					waterReflection.Update(new Vector3(0f, 0f, this.WaterHeight), base.Rotation.Up, 2f);
				}
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			WaterSceneObject.RippleCompute waterRipple = this.WaterRipple;
			if (waterRipple == null)
			{
				return;
			}
			waterRipple.Update();
		}

		// Token: 0x06001468 RID: 5224 RVA: 0x00052EA0 File Offset: 0x000510A0
		[Description("Updates that are carried inside render thread")]
		public override void RenderSceneObject()
		{
			if (PlanarReflection.IsRenderingReflection())
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			Render.SetupLighting(this);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			PlanarReflection waterReflection = this.WaterReflection;
			if (waterReflection != null)
			{
				waterReflection.OnRender();
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			WaterSceneObject.RippleCompute waterRipple = this.WaterRipple;
			if (waterRipple != null)
			{
				waterRipple.OnRender();
			}
			bool bViewIntersectingWater = this.ViewIntersetingBBox(this.RenderBounds);
			int[] res = this.ApproximatePlaneResolution(this.RenderBounds);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.vbUnderwaterStencil = this.MakeCuboid(this.RenderBounds.Mins, this.RenderBounds.Maxs);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.vbWaterSurface = this.MakeTesselatedPlane(this.RenderBounds.Mins, this.RenderBounds.Maxs, res[0], res[1]);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			Render.CopyFrameBuffer();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			Render.CopyDepthBuffer();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			RenderAttributes attributes = Render.Attributes;
			string text = "WaterHeight";
			attributes.Set(text, this.WaterHeight);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			RenderAttributes attributes2 = Render.Attributes;
			text = "D_REFLECTION";
			bool flag = this.WaterReflection != null;
			attributes2.SetCombo(text, flag);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			RenderAttributes attributes3 = Render.Attributes;
			text = "D_REFRACTION";
			flag = true;
			attributes3.SetCombo(text, flag);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			RenderAttributes attributes4 = Render.Attributes;
			text = "D_RIPPLES";
			flag = (this.WaterRipple != null);
			attributes4.SetCombo(text, flag);
			if (this.WaterRipple != null)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				RenderAttributes attributes5 = Render.Attributes;
				text = "SplashTexture";
				WaterSceneObject.RippleCompute.RippleCascade cascade = this.WaterRipple.Cascade;
				int num = -1;
				attributes5.Set(text, cascade.Texture, num);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				RenderAttributes attributes6 = Render.Attributes;
				text = "SplashRadius";
				attributes6.Set(text, this.WaterRipple.SplashConstants.Radius);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				RenderAttributes attributes7 = Render.Attributes;
				text = "SplashViewPosition";
				attributes7.Set(text, this.WaterRipple.SplashConstants.ViewPosition);
			}
			if (bViewIntersectingWater)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				RenderAttributes attributes8 = Render.Attributes;
				text = "D_UNDERWATER";
				flag = true;
				attributes8.SetCombo(text, flag);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.vbUnderwaterStencil.Draw(this.WaterMaterial);
			}
			if (this.WaterReflection != null)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				RenderAttributes attributes9 = Render.Attributes;
				text = "ReflectionTexture";
				Texture colorTarget = this.WaterReflection.ColorTarget;
				int num = -1;
				attributes9.Set(text, colorTarget, num);
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			RenderAttributes attributes10 = Render.Attributes;
			text = "D_UNDERWATER";
			flag = false;
			attributes10.SetCombo(text, flag);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			RenderAttributes attributes11 = Render.Attributes;
			text = "D_VIEW_INTERSECTING_WATER";
			attributes11.SetCombo(text, bViewIntersectingWater);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			VertexBuffer vertexBuffer = this.vbWaterSurface;
			if (vertexBuffer != null)
			{
				vertexBuffer.Draw(this.WaterMaterial);
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.RenderSceneObject();
		}

		// Token: 0x06001469 RID: 5225 RVA: 0x00053130 File Offset: 0x00051330
		[Description("Checks if the view is intersecting the water bounding box Could probably be moved to a method inside BBox.cs to check if Vector3 is in bbox")]
		internal bool ViewIntersetingBBox(BBox bbox)
		{
			Vector3 viewPos = CurrentView.Position;
			float nearZ = 15f;
			return viewPos.x >= bbox.Mins.x - nearZ && viewPos.x <= bbox.Maxs.x + nearZ && viewPos.y >= bbox.Mins.y - nearZ && viewPos.y <= bbox.Maxs.y + nearZ && viewPos.z >= bbox.Mins.z - nearZ && viewPos.z <= bbox.Maxs.z + nearZ;
		}

		// Token: 0x0600146A RID: 5226 RVA: 0x000531D6 File Offset: 0x000513D6
		internal void DebugView()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			GlobalGameNamespace.DebugOverlay.Box(this.RenderBounds.Mins, this.RenderBounds.Maxs, Color.Red, 0f, false);
		}

		// Token: 0x0600146B RID: 5227 RVA: 0x00053208 File Offset: 0x00051408
		public int[] ApproximatePlaneResolution(BBox bounds)
		{
			Vector2 Bounds = new Vector2(bounds.Maxs.x - bounds.Mins.x, bounds.Maxs.y - bounds.Mins.y);
			int[] res = new int[]
			{
				(int)(Bounds.x / 512f),
				(int)(Bounds.y / 512f)
			};
			RuntimeHelpers.EnsureSufficientExecutionStack();
			return new int[]
			{
				Math.Clamp(res[0], 1, 256),
				Math.Clamp(res[1], 1, 256)
			};
		}

		// Token: 0x0600146C RID: 5228 RVA: 0x000532A8 File Offset: 0x000514A8
		public VertexBuffer MakeTesselatedPlane(Vector3 from, Vector3 to, int xRes, int yRes)
		{
			VertexBuffer vb = new VertexBuffer();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			vb.Init(true);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.WaterHeight = Math.Max(from.z, to.z);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			xRes = Math.Clamp(xRes, 1, 256);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			yRes = Math.Clamp(yRes, 1, 256);
			for (int x = 0; x <= xRes; x++)
			{
				for (int y = 0; y <= yRes; y++)
				{
					float fX = from.x.LerpTo(to.x, (float)x / (float)xRes, true);
					float fY = from.y.LerpTo(to.y, (float)y / (float)yRes, true);
					Vector3 vPos = new Vector3(fX, fY, this.WaterHeight);
					RuntimeHelpers.EnsureSufficientExecutionStack();
					vPos -= base.Transform.Position;
					Vector2 uv = new Vector2((float)x / (float)xRes, (float)y / (float)yRes);
					RuntimeHelpers.EnsureSufficientExecutionStack();
					vb.Add(new Vertex(vPos, Vector3.Down, Vector3.Right, uv));
				}
			}
			for (int y2 = 0; y2 < yRes; y2++)
			{
				for (int x2 = 0; x2 < xRes; x2++)
				{
					int i = y2 + x2 * yRes + x2;
					RuntimeHelpers.EnsureSufficientExecutionStack();
					vb.AddRawIndex(i + yRes + 1);
					RuntimeHelpers.EnsureSufficientExecutionStack();
					vb.AddRawIndex(i + 1);
					RuntimeHelpers.EnsureSufficientExecutionStack();
					vb.AddRawIndex(i);
					RuntimeHelpers.EnsureSufficientExecutionStack();
					vb.AddRawIndex(i + 1);
					RuntimeHelpers.EnsureSufficientExecutionStack();
					vb.AddRawIndex(i + yRes + 1);
					RuntimeHelpers.EnsureSufficientExecutionStack();
					vb.AddRawIndex(i + yRes + 2);
				}
			}
			return vb;
		}

		// Token: 0x0600146D RID: 5229 RVA: 0x0005345B File Offset: 0x0005165B
		[Description("Creates a ripple to be simulated at the given position")]
		public void AddRipple(Vector2 position, float force)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			WaterSceneObject.RippleCompute waterRipple = this.WaterRipple;
			if (waterRipple == null)
			{
				return;
			}
			waterRipple.AddRipple(position, force);
		}

		// Token: 0x0600146E RID: 5230 RVA: 0x00053474 File Offset: 0x00051674
		[Description("Makes a vertex buffer cube for fog with the given bounds")]
		public VertexBuffer MakeCuboid(Vector3 mins, Vector3 maxs)
		{
			VertexBuffer vertexBuffer = new VertexBuffer();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			vertexBuffer.Init(true);
			Vector3 center = (mins + maxs) / 2f - base.Transform.Position;
			Vector3 size = maxs - mins;
			Rotation rot = new Rotation();
			Vector3 f = rot.Forward * -size.x * 0.5f;
			Vector3 i = rot.Left * -size.y * 0.5f;
			Vector3 u = rot.Up * -size.z * 0.5f;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			vertexBuffer.AddQuad(new Ray(center + f, f.Normal), i, u);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			vertexBuffer.AddQuad(new Ray(center - f, -f.Normal), i, -u);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			vertexBuffer.AddQuad(new Ray(center + i, i.Normal), -f, u);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			vertexBuffer.AddQuad(new Ray(center - i, -i.Normal), f, u);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			vertexBuffer.AddQuad(new Ray(center + u, u.Normal), f, i);
			return vertexBuffer;
		}

		// Token: 0x04000693 RID: 1683
		public BBox RenderBounds;

		// Token: 0x04000694 RID: 1684
		public float WaterHeight;

		// Token: 0x04000695 RID: 1685
		protected Material WaterMaterial;

		// Token: 0x04000696 RID: 1686
		protected VertexBuffer vbWaterSurface;

		// Token: 0x04000697 RID: 1687
		private Water WaterParent;

		// Token: 0x04000698 RID: 1688
		protected PlanarReflection WaterReflection;

		// Token: 0x04000699 RID: 1689
		protected WaterSceneObject.RippleCompute WaterRipple;

		// Token: 0x0400069A RID: 1690
		internal bool EnableRefraction;

		// Token: 0x0400069B RID: 1691
		internal Vector3 _lastPosition;

		// Token: 0x0400069C RID: 1692
		protected VertexBuffer vbUnderwaterStencil;

		// Token: 0x0200025D RID: 605
		public class RippleCompute
		{
			// Token: 0x060019AF RID: 6575 RVA: 0x0006B304 File Offset: 0x00069504
			public RippleCompute(WaterSceneObject parent)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Water = parent;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.compute = new ComputeShader("ripplecompute_cs");
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Cascade = new WaterSceneObject.RippleCompute.RippleCascade(512f);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.SplashConstants = default(WaterSceneObject.RippleCompute.RippleConstants);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.SplashConstants.ViewPosition = new Vector2(0f, 0f);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.SplashConstants.ViewPositionLast = new Vector2(0f, 0f);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.SplashConstants.Radius = 0f;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.SplashConstants.Splashes = new List<WaterSceneObject.RippleCompute.SplashInformation>();
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.SplashConstants.TimeDelta = 0.016666668f;
			}

			// Token: 0x060019B0 RID: 6576 RVA: 0x0006B3E8 File Offset: 0x000695E8
			internal float GetSmallestRadiusFromBBox(BBox box)
			{
				return Math.Min(box.Mins.x - box.Maxs.x, Math.Min(box.Mins.y - box.Maxs.y, box.Mins.z - box.Maxs.z));
			}

			// Token: 0x060019B1 RID: 6577 RVA: 0x0006B44C File Offset: 0x0006964C
			[Description("Updates per frame data for the splash to render on the GPU next")]
			public void Update()
			{
				float Radius = Math.Abs(this.Cascade.Radius);
				float fHeight = Math.Max(this.Water.RenderBounds.Mins.z, this.Water.RenderBounds.Maxs.z);
				Vector3 vPos = new Vector3(CurrentView.Position.x + CurrentView.Rotation.Forward.x * Radius, CurrentView.Position.y + CurrentView.Rotation.Forward.y * Radius, fHeight);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				vPos = new Vector3(MathF.Floor(vPos.x / 64f) * 64f, MathF.Floor(vPos.y / 64f) * 64f, vPos.z);
				if (this.Water.RenderBounds.Size.x > Radius * 2f && this.Water.RenderBounds.Size.y > Radius * 2f)
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					vPos.x = Math.Clamp(vPos.x, this.Water.RenderBounds.Mins.x + Radius, this.Water.RenderBounds.Maxs.x - Radius);
					RuntimeHelpers.EnsureSufficientExecutionStack();
					vPos.y = Math.Clamp(vPos.y, this.Water.RenderBounds.Mins.y + Radius, this.Water.RenderBounds.Maxs.y - Radius);
				}
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.SplashConstants.ViewPositionLast = this.SplashConstants.ViewPosition;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.SplashConstants.ViewPosition = vPos;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.SplashConstants.Radius = Radius;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.SplashConstants.TimeDelta = Math.Min(Time.Delta, 0.5f);
				foreach (Entity ent in Entity.FindInBox(new BBox(vPos + new Vector3(-Radius, -Radius, 8f), vPos + new Vector3(Radius, Radius, -8f))))
				{
					new Vector3(ent.Position.x, ent.Position.y, fHeight);
					Vector3 velocityDelta = new Vector3(ent.Velocity * Time.Delta);
					float radius = this.GetSmallestRadiusFromBBox(ent.WorldSpaceBounds);
					if (velocityDelta.Length >= 0.1f)
					{
						RuntimeHelpers.EnsureSufficientExecutionStack();
						this.SplashConstants.Splashes.Add(new WaterSceneObject.RippleCompute.SplashInformation
						{
							Position = new Vector2(ent.Position.x, ent.Position.y),
							Radius = radius,
							Strength = velocityDelta.Length
						});
					}
				}
			}

			// Token: 0x060019B2 RID: 6578 RVA: 0x0006B794 File Offset: 0x00069994
			internal void DebugView(BBox box, float height)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				GlobalGameNamespace.DebugOverlay.Box(box.Mins, box.Maxs, Color.Cyan, 0f, false);
				foreach (WaterSceneObject.RippleCompute.SplashInformation splash in this.SplashConstants.Splashes)
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					GlobalGameNamespace.DebugOverlay.Box(new Vector3(splash.Position, height) + new Vector3(splash.Radius, splash.Radius, splash.Strength), new Vector3(splash.Position, height) + new Vector3(-splash.Radius, -splash.Radius, -splash.Strength), Color.Red, 0f, false);
				}
			}

			// Token: 0x060019B3 RID: 6579 RVA: 0x0006B888 File Offset: 0x00069A88
			[Description("Encodes the information of the splash into a float4")]
			public Vector4 EncodeSplash(WaterSceneObject.RippleCompute.SplashInformation splash)
			{
				return new Vector4(splash.Position.x, splash.Position.y, splash.Radius, splash.Strength);
			}

			// Token: 0x060019B4 RID: 6580 RVA: 0x0006B8B4 File Offset: 0x00069AB4
			internal void SendAttributes()
			{
				if (!this.Cascade.Texture.IsLoaded || !EngineConVars.r_water_ripples)
				{
					return;
				}
				RuntimeHelpers.EnsureSufficientExecutionStack();
				RenderAttributes attributes = this.compute.Attributes;
				string text = "ViewPosition";
				attributes.Set(text, this.SplashConstants.ViewPosition);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				RenderAttributes attributes2 = this.compute.Attributes;
				text = "ViewPositionLast";
				attributes2.Set(text, this.SplashConstants.ViewPositionLast);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				RenderAttributes attributes3 = this.compute.Attributes;
				text = "Radius";
				float num = this.SplashConstants.Radius * 2f;
				attributes3.Set(text, num);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				RenderAttributes attributes4 = this.compute.Attributes;
				text = "TimeDelta";
				attributes4.Set(text, this.SplashConstants.TimeDelta);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				RenderAttributes attributes5 = this.compute.Attributes;
				text = "Splashes";
				int num2 = this.SplashConstants.Splashes.Count;
				attributes5.Set(text, num2);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				RenderAttributes attributes6 = this.compute.Attributes;
				text = "WaterHeight";
				num = Math.Max(this.Water.RenderBounds.Mins.z, this.Water.RenderBounds.Maxs.z);
				attributes6.Set(text, num);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				RenderAttributes attributes7 = this.compute.Attributes;
				text = "WaterVelocity";
				Vector3 velocity = this.Water.Velocity;
				attributes7.Set(text, velocity);
				if (this.SplashConstants.Splashes.Count > 0)
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					RenderAttributes attributes8 = this.compute.Attributes;
					text = "SplashInformation0";
					Vector4 vector = this.EncodeSplash(this.SplashConstants.Splashes[0]);
					attributes8.Set(text, vector);
				}
				else
				{
					RenderAttributes attributes9 = this.compute.Attributes;
					text = "SplashInformation0";
					attributes9.Set(text, Vector4.Zero);
				}
				RuntimeHelpers.EnsureSufficientExecutionStack();
				RenderAttributes attributes10 = this.compute.Attributes;
				text = "SplashTexture";
				WaterSceneObject.RippleCompute.RippleCascade cascade = this.Cascade;
				num2 = -1;
				attributes10.Set(text, cascade.Texture, num2);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				RenderAttributes attributes11 = this.compute.Attributes;
				text = "SplashTextureLast";
				WaterSceneObject.RippleCompute.RippleCascade cascade2 = this.Cascade;
				num2 = -1;
				attributes11.Set(text, cascade2.PrevTexture, num2);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				RenderAttributes attributes12 = this.compute.Attributes;
				text = "SplashTextureSize";
				num2 = this.Cascade.Texture.Width;
				attributes12.Set(text, num2);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				Render.CopyDepthBuffer();
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.SplashConstants.Splashes.Clear();
			}

			// Token: 0x060019B5 RID: 6581 RVA: 0x0006BB40 File Offset: 0x00069D40
			[Description("Renders the splash texture on the compute shader on the GPU")]
			public void OnRender()
			{
				if (!this.Cascade.Texture.IsLoaded)
				{
					return;
				}
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Cascade.Swap();
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.SendAttributes();
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.compute.Dispatch(this.Cascade.Texture.Width, this.Cascade.Texture.Height, 1);
			}

			// Token: 0x060019B6 RID: 6582 RVA: 0x0006BBAC File Offset: 0x00069DAC
			public void AddRipple(Vector2 position, float force)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.SplashConstants.Splashes.Add(new WaterSceneObject.RippleCompute.SplashInformation
				{
					Position = position,
					Radius = 2f,
					Strength = force
				});
			}

			// Token: 0x04000A05 RID: 2565
			internal WaterSceneObject Water;

			// Token: 0x04000A06 RID: 2566
			internal ComputeShader compute;

			// Token: 0x04000A07 RID: 2567
			public WaterSceneObject.RippleCompute.RippleCascade Cascade;

			// Token: 0x04000A08 RID: 2568
			public float Radius = 512f;

			// Token: 0x04000A09 RID: 2569
			public WaterSceneObject.RippleCompute.RippleConstants SplashConstants;

			// Token: 0x02000279 RID: 633
			public class RippleCascade
			{
				// Token: 0x060019E8 RID: 6632 RVA: 0x0006C128 File Offset: 0x0006A328
				public RippleCascade(float radius = 512f)
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.Radius = radius;
					ImageFormat format = ImageFormat.RG1616F;
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.Texture = Texture.Create(256, 256, ImageFormat.RGBA8888).WithUAVBinding().WithFormat(format).Finish();
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.PrevTexture = Texture.Create(256, 256, ImageFormat.RGBA8888).WithUAVBinding().WithFormat(format).Finish();
				}

				// Token: 0x060019E9 RID: 6633 RVA: 0x0006C1B4 File Offset: 0x0006A3B4
				public void Swap()
				{
					Texture temp = this.Texture;
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.Texture = this.PrevTexture;
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.PrevTexture = temp;
				}

				// Token: 0x04000A40 RID: 2624
				public Texture Texture;

				// Token: 0x04000A41 RID: 2625
				internal Texture PrevTexture;

				// Token: 0x04000A42 RID: 2626
				public float Radius;
			}

			// Token: 0x0200027A RID: 634
			public struct SplashInformation
			{
				// Token: 0x04000A43 RID: 2627
				public Vector3 Position;

				// Token: 0x04000A44 RID: 2628
				public float Radius;

				// Token: 0x04000A45 RID: 2629
				public float Strength;
			}

			// Token: 0x0200027B RID: 635
			public struct RippleConstants
			{
				// Token: 0x04000A46 RID: 2630
				public Vector2 ViewPosition;

				// Token: 0x04000A47 RID: 2631
				public Vector2 ViewPositionLast;

				// Token: 0x04000A48 RID: 2632
				public float Radius;

				// Token: 0x04000A49 RID: 2633
				public float TimeDelta;

				// Token: 0x04000A4A RID: 2634
				public List<WaterSceneObject.RippleCompute.SplashInformation> Splashes;
			}
		}
	}
}
