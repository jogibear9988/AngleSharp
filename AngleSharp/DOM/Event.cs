﻿namespace AngleSharp.DOM
{
    using System;

    /// <summary>
    /// Represents an event argument.
    /// </summary>
    sealed class Event : IEvent
    {
        /// <summary>
        /// Gets a dummy placeholder event.
        /// </summary>
        public static readonly Event Empty = new Event();

        #region Properties

        /// <summary>
        /// Gets the type of event.
        /// </summary>
        public String Type
        {
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        /// Gets the original target of the event.
        /// </summary>
        public IEventTarget OriginalTarget
        {
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        /// Gets the current target (if bubbled).
        /// </summary>
        public IEventTarget CurrentTarget
        {
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        /// Gets the phase of the event.
        /// </summary>
        public EventPhase Phase
        {
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        /// Gets if the event is actually bubbling.
        /// </summary>
        public Boolean IsBubbling
        {
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        /// Gets if the event is cancelable.
        /// </summary>
        public Boolean IsCancelable
        {
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        /// Gets if the default behavior has been prevented.
        /// </summary>
        public Boolean IsDefaultPrevented
        {
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        /// Gets if the event is trusted.
        /// </summary>
        public Boolean IsTrusted
        {
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        /// Gets the originating timestamp.
        /// </summary>
        public DateTime Time
        {
            get { throw new NotImplementedException(); }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Prevents further propagation of the event.
        /// </summary>
        public void Stop()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Stops the immediate propagation.
        /// </summary>
        public void StopImmediately()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Prevents the default behavior.
        /// </summary>
        public void Cancel()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Initializes the event.
        /// </summary>
        /// <param name="type">The type of the event.</param>
        /// <param name="bubbles">If the event is bubbling.</param>
        /// <param name="cancelable">If the event is cancelable.</param>
        public void Init(String type, Boolean bubbles, Boolean cancelable)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
