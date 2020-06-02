using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;
using Xunit.Sdk;

namespace Xunit {
	/// <summary>
	/// Exception thrown when parser unexpectedly failed.
	/// </summary>
	[SuppressMessage("Design", "CA1032:Implement standard exception constructors", Justification = "<Pending>")]
	[SuppressMessage("Design", "RCS1194:Implement exception constructors.", Justification = "<Pending>")]
	public sealed class FailedException : XunitException {
		/// <summary>
		/// Creates a new instance of the <see cref="FailedException"/> class.
		/// </summary>
		public FailedException() : base("Parser succeeded when expected to fail") { }
	}
}
