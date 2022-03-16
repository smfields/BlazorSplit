using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BlazorSplit;

public class SplitInterop : IAsyncDisposable
{
    private readonly Lazy<Task<IJSObjectReference>> _moduleTask;

    public SplitInterop(IJSRuntime jsRuntime)
    {
        _moduleTask = new Lazy<Task<IJSObjectReference>>(() => jsRuntime.InvokeAsync<IJSObjectReference>(
            "import", "./_content/smfields.BlazorSplit/split-interop.js").AsTask());
    }

    public async Task<SplitInstance> CreateInstance(List<ElementReference> elementReferences, SplitOptions? options = null)
    {
        IJSObjectReference module = await _moduleTask.Value;

        IJSObjectReference instanceReference;
        if (options == null)
            instanceReference = await module.InvokeAsync<IJSObjectReference>("createInstance", elementReferences);
        else
        {
            // Custom serialization to ensure we send null values as undefined and not null
            var optionsJson = JsonSerializer.SerializeToDocument(options, new JsonSerializerOptions
            {
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            });
            instanceReference = await module.InvokeAsync<IJSObjectReference>("createInstance", elementReferences, optionsJson);
        }

        return new SplitInstance(instanceReference);
    }
    
    public async ValueTask DisposeAsync()
    {
        if (_moduleTask.IsValueCreated)
        {
            IJSObjectReference module = await _moduleTask.Value;
            await module.DisposeAsync();
        }
    }
}