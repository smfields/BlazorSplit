using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BlazorSplit;

public class SplitInterop
{
    private readonly IJSRuntime                     _jsRuntime;
    private readonly Lazy<Task> _importTask;

    public SplitInterop(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
        _importTask = new (() => jsRuntime.InvokeVoidAsync(
            "import", "https://cdnjs.cloudflare.com/ajax/libs/split.js/1.6.0/split.min.js").AsTask());
    }

    public async Task<SplitInstance> CreateInstance(List<ElementReference> elementReferences, SplitOptions? options = null)
    {
        await _importTask.Value;
        
        IJSObjectReference instanceReference;
        if (options == null)
        {
            instanceReference = await _jsRuntime.InvokeAsync<IJSObjectReference>("Split", elementReferences);
        }
        else
        {
            instanceReference = await _jsRuntime.InvokeAsync<IJSObjectReference>("Split", elementReferences, options);
        }

        return new SplitInstance(instanceReference);
    }
}
