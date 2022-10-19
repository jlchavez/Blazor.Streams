﻿using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.Streams;

/// <summary>
/// <see href="https://streams.spec.whatwg.org/#typedefdef-readablestreamreader">Streams browser specs</see>
/// </summary>
public abstract class ReadableStreamReaderInProcess : ReadableStreamReader
{
    protected readonly IJSInProcessObjectReference inProcessHelper;

    /// <summary>
    /// Constructs a wrapper instance for a given JS Instance of a <see cref="ReadableStreamReader"/>.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <param name="inProcessHelper">An in process helper instance.</param>
    /// <param name="jSReference">A JS reference to an existing <see cref="ReadableStreamReader"/>.</param>
    internal ReadableStreamReaderInProcess(IJSRuntime jSRuntime, IJSInProcessObjectReference inProcessHelper, IJSObjectReference jSReference) : base(jSRuntime, jSReference)
    {
        this.inProcessHelper = inProcessHelper;
    }

    /// <summary>
    /// Sets the internal <c>reader</c> slot to <c>undefined</c>.
    /// </summary>
    /// <returns></returns>
    public void ReleaseLock()
    {
        ((IJSInProcessObjectReference)JSReference).InvokeVoid("releaseLock");
    }

    /// <summary>
    /// Gets a JS reference to the closed attribute.
    /// </summary>
    /// <returns></returns>
    public IJSObjectReference Closed => inProcessHelper.Invoke<IJSObjectReference>("getAttribyte", JSReference, "closed");

    /// <summary>
    /// Cancels the underlying stream which is equivalent to <see cref="ReadableStreamInProcess.Cancel"/>
    /// </summary>
    public void Cancel()
    {
        ((IJSInProcessObjectReference)JSReference).InvokeVoid("cancel");
    }
}