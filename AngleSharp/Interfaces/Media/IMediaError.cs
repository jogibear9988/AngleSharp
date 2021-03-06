﻿namespace AngleSharp.DOM.Media
{
    /// <summary>
    /// Stores information about media errors.
    /// </summary>
    [DomName("MediaError")]
    public interface IMediaError
    {
        /// <summary>
        /// Gets the code that represents the media error.
        /// </summary>
        [DomName("code")]
        MediaErrorCode Code { get; }
    }
}
