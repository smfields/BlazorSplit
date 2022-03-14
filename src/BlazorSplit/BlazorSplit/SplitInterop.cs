using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BlazorSplit;

public class SplitInterop
{
    private readonly Lazy<Task> _importTask;
    private readonly IJSRuntime _jsRuntime;

    public SplitInterop(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
        _importTask = new Lazy<Task>(() => jsRuntime.InvokeVoidAsync(
            "import", "https://cdnjs.cloudflare.com/ajax/libs/split.js/1.6.0/split.min.js").AsTask());
    }

    public async Task<SplitInstance> CreateInstance(List<ElementReference> elementReferences, SplitOptions? options = null)
    {
        await _importTask.Value;

        IJSObjectReference instanceReference;
        if (options == null)
            instanceReference = await _jsRuntime.InvokeAsync<IJSObjectReference>("Split", elementReferences);
        else
        {
            // Custom serialization to ensure we send null values as undefined and not null
            var optionsJson = JsonSerializer.SerializeToDocument(options, new JsonSerializerOptions
            {
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            });
            instanceReference = await _jsRuntime.InvokeAsync<IJSObjectReference>("Split", elementReferences, optionsJson);
        }

        return new SplitInstance(instanceReference);
    }
}