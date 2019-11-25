﻿using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;

namespace CompatBot.Commands.Attributes
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = false)]
    internal class RequiresDm: CheckBaseAttribute
    {
        private const string Source = "https://cdn.discordapp.com/attachments/417347469521715210/534798232858001418/24qx11.jpg";
        private static readonly Lazy<byte[]> Poster = new Lazy<byte[]>(() =>
        {
            using var client = HttpClientFactory.Create();
            return client.GetByteArrayAsync(Source).ConfigureAwait(true).GetAwaiter().GetResult();
        });

        public override async Task<bool> ExecuteCheckAsync(CommandContext ctx, bool help)
        {
            if (ctx.Channel.IsPrivate || help)
                return true;

            using (var stream = new MemoryStream(Poster.Value))
                await ctx.RespondWithFileAsync("senpai_plz.jpg", stream).ConfigureAwait(false);
            return false;
        }
    }
}