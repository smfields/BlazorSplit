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
        try
        {
            await Destroy();
        }
        catch (JSDisconnectedException)
        {
            // Silently ignore since we don't need to dispose if we've been disconnected
        }
    }
}