using Microsoft.JSInterop;

namespace BlazorSplit;

public class SplitInstance : IAsyncDisposable
{
    private readonly IJSObjectReference _instanceReference;

    public SplitInstance(IJSObjectReference instanceReference)
    {
        _instanceReference = instanceReference;
    }

    public async Task Destroy(bool? preserveStyles = null, bool? preserveGutters = null)
    {
        await _instanceReference.InvokeVoidAsync("destroy", preserveStyles, preserveGutters);
    }

    public async ValueTask DisposeAsync()
    {
        await Destroy();
    }
}