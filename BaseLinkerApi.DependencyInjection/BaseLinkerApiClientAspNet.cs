﻿using System.Runtime.CompilerServices;
using BaseLinkerApi.Common;
using BaseLinkerApi.DependencyInjection.Options;
using Microsoft.Extensions.Options;

[assembly:InternalsVisibleTo("BaseLinkerApi.Tests")]

namespace BaseLinkerApi.DependencyInjection;

internal class BaseLinkerApiClientAspNet : IBaseLinkerApiClient
{
    private readonly BaseLinkerApiClient _client;

    public BaseLinkerApiClientAspNet(HttpClient httpClient, IOptions<BaseLinkerOptions> options)
    {
        ArgumentNullException.ThrowIfNull(options.Value.Token);
        _client = new BaseLinkerApiClient(httpClient, options.Value.Token)
        {
            ThrowExceptions = options.Value.ThrowExceptions,
            UseRequestLimit = options.Value.UseRequestLimit
        };
    }

    public Task<TOutput> SendAsync<TOutput>(IRequest<TOutput> request, CancellationToken cancellationToken = default) where TOutput : ResponseBase
        => _client.SendAsync(request, cancellationToken);
}