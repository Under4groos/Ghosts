using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Sandbox.Internal;

namespace Sandbox
{
	// Token: 0x0200014D RID: 333
	public static class LeaderboardExtensions
	{
		// Token: 0x06000F36 RID: 3894 RVA: 0x0003D600 File Offset: 0x0003B800
		internal static void ClientDisconnect(Client cl)
		{
			List<TaskCompletionSource<LeaderboardUpdate?>> cancelled = null;
			Dictionary<ValueTuple<long, int>, TaskCompletionSource<LeaderboardUpdate?>> pendingSubmissions = LeaderboardExtensions.PendingSubmissions;
			lock (pendingSubmissions)
			{
				if (LeaderboardExtensions.PendingSubmissions.Count == 0)
				{
					return;
				}
				IEnumerable<KeyValuePair<ValueTuple<long, int>, TaskCompletionSource<LeaderboardUpdate?>>> pendingSubmissions2 = LeaderboardExtensions.PendingSubmissions;
				Func<KeyValuePair<ValueTuple<long, int>, TaskCompletionSource<LeaderboardUpdate?>>, bool> <>9__0;
				Func<KeyValuePair<ValueTuple<long, int>, TaskCompletionSource<LeaderboardUpdate?>>, bool> predicate;
				if ((predicate = <>9__0) == null)
				{
					predicate = (<>9__0 = (([TupleElementNames(new string[]
					{
						"PlayerId",
						"Guid"
					})] KeyValuePair<ValueTuple<long, int>, TaskCompletionSource<LeaderboardUpdate?>> x) => x.Key.Item1 == cl.PlayerId));
				}
				foreach (KeyValuePair<ValueTuple<long, int>, TaskCompletionSource<LeaderboardUpdate?>> pair in pendingSubmissions2.Where(predicate).ToArray<KeyValuePair<ValueTuple<long, int>, TaskCompletionSource<LeaderboardUpdate?>>>())
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					if (cancelled == null)
					{
						cancelled = new List<TaskCompletionSource<LeaderboardUpdate?>>();
					}
					RuntimeHelpers.EnsureSufficientExecutionStack();
					cancelled.Add(pair.Value);
					RuntimeHelpers.EnsureSufficientExecutionStack();
					LeaderboardExtensions.PendingSubmissions.Remove(pair.Key);
				}
			}
			if (cancelled == null)
			{
				return;
			}
			foreach (TaskCompletionSource<LeaderboardUpdate?> taskCompletionSource in cancelled)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				taskCompletionSource.SetResult(null);
			}
		}

		// Token: 0x06000F37 RID: 3895 RVA: 0x0003D730 File Offset: 0x0003B930
		[Description("Attempts to submit a new score to this leaderboard from the given client. This can only be called server-side, and only with a leaderboard for the currently running addon.")]
		public static Task<LeaderboardUpdate?> Submit(this Leaderboard leaderboard, Client client, int score, bool alwaysReplace = false)
		{
			LeaderboardExtensions.<Submit>d__2 <Submit>d__;
			<Submit>d__.<>t__builder = AsyncTaskMethodBuilder<LeaderboardUpdate?>.Create();
			<Submit>d__.leaderboard = leaderboard;
			<Submit>d__.client = client;
			<Submit>d__.score = score;
			<Submit>d__.alwaysReplace = alwaysReplace;
			<Submit>d__.<>1__state = -1;
			<Submit>d__.<>t__builder.Start<LeaderboardExtensions.<Submit>d__2>(ref <Submit>d__);
			return <Submit>d__.<>t__builder.Task;
		}

		// Token: 0x06000F38 RID: 3896 RVA: 0x0003D78C File Offset: 0x0003B98C
		[ClientRpc]
		internal static void ClientSubmit(int guid, string lbName, int score, bool alwaysReplace)
		{
			if (!LeaderboardExtensions.ClientSubmit__RpcProxy(guid, lbName, score, alwaysReplace, null))
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			LeaderboardExtensions.ClientSubmitAsync(guid, lbName, score, alwaysReplace);
		}

		// Token: 0x06000F39 RID: 3897 RVA: 0x0003D7C0 File Offset: 0x0003B9C0
		private static Task ClientSubmitAsync(int guid, string lbName, int score, bool alwaysReplace)
		{
			LeaderboardExtensions.<ClientSubmitAsync>d__4 <ClientSubmitAsync>d__;
			<ClientSubmitAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<ClientSubmitAsync>d__.guid = guid;
			<ClientSubmitAsync>d__.lbName = lbName;
			<ClientSubmitAsync>d__.score = score;
			<ClientSubmitAsync>d__.alwaysReplace = alwaysReplace;
			<ClientSubmitAsync>d__.<>1__state = -1;
			<ClientSubmitAsync>d__.<>t__builder.Start<LeaderboardExtensions.<ClientSubmitAsync>d__4>(ref <ClientSubmitAsync>d__);
			return <ClientSubmitAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000F3A RID: 3898 RVA: 0x0003D81C File Offset: 0x0003BA1C
		[ConCmd.ServerAttribute(null)]
		internal static void ServerFinishSubmit(int guid, bool success, int score, bool changed, int newGlobalRank, int oldGlobalRank)
		{
			if (!Host.IsServer)
			{
				ConsoleSystem.Run("ServerFinishSubmit", new object[]
				{
					guid,
					success,
					score,
					changed,
					newGlobalRank,
					oldGlobalRank
				});
				return;
			}
			Client client = ConsoleSystem.Caller;
			Dictionary<ValueTuple<long, int>, TaskCompletionSource<LeaderboardUpdate?>> pendingSubmissions = LeaderboardExtensions.PendingSubmissions;
			TaskCompletionSource<LeaderboardUpdate?> tcs;
			lock (pendingSubmissions)
			{
				if (!LeaderboardExtensions.PendingSubmissions.TryGetValue(new ValueTuple<long, int>(client.PlayerId, guid), out tcs))
				{
					return;
				}
				RuntimeHelpers.EnsureSufficientExecutionStack();
				LeaderboardExtensions.PendingSubmissions.Remove(new ValueTuple<long, int>(client.PlayerId, guid));
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			tcs.SetResult(success ? new LeaderboardUpdate?(new LeaderboardUpdate(score, changed, newGlobalRank, oldGlobalRank)) : null);
		}

		// Token: 0x06000F3B RID: 3899 RVA: 0x0003D910 File Offset: 0x0003BB10
		private static bool ClientSubmit__RpcProxy(int guid, string lbName, int score, bool alwaysReplace, To? toTarget = null)
		{
			if (!Prediction.FirstTime)
			{
				return false;
			}
			if (!Host.IsServer)
			{
				Prediction.Watch("ClientSubmit", new object[]
				{
					guid,
					lbName,
					score,
					alwaysReplace
				});
				return true;
			}
			using (NetWrite writer = NetWrite.StartRpc(-1550248496, null))
			{
				if (!NetRead.IsSupported(guid))
				{
					GlobalGameNamespace.Log.Error("[ClientRpc] ClientSubmit is not allowed to use Int32 for the parameter 'guid'!");
					return false;
				}
				writer.Write<int>(guid);
				if (!NetRead.IsSupported(lbName))
				{
					GlobalGameNamespace.Log.Error("[ClientRpc] ClientSubmit is not allowed to use String for the parameter 'lbName'!");
					return false;
				}
				writer.Write<string>(lbName);
				if (!NetRead.IsSupported(score))
				{
					GlobalGameNamespace.Log.Error("[ClientRpc] ClientSubmit is not allowed to use Int32 for the parameter 'score'!");
					return false;
				}
				writer.Write<int>(score);
				if (!NetRead.IsSupported(alwaysReplace))
				{
					GlobalGameNamespace.Log.Error("[ClientRpc] ClientSubmit is not allowed to use Boolean for the parameter 'alwaysReplace'!");
					return false;
				}
				writer.Write<bool>(alwaysReplace);
				writer.SendRpc(toTarget, null);
			}
			return false;
		}

		// Token: 0x06000F3C RID: 3900 RVA: 0x0003DA2C File Offset: 0x0003BC2C
		internal static void ClientSubmit(To toTarget, int guid, string lbName, int score, bool alwaysReplace)
		{
			LeaderboardExtensions.ClientSubmit__RpcProxy(guid, lbName, score, alwaysReplace, new To?(toTarget));
		}

		// Token: 0x040004C7 RID: 1223
		[TupleElementNames(new string[]
		{
			"PlayerId",
			"Guid"
		})]
		private static readonly Dictionary<ValueTuple<long, int>, TaskCompletionSource<LeaderboardUpdate?>> PendingSubmissions = new Dictionary<ValueTuple<long, int>, TaskCompletionSource<LeaderboardUpdate?>>();
	}
}
