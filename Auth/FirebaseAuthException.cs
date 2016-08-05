using System;
using System.Runtime.Serialization;

namespace MrQuickie
{
	/*
	 * Sign In Exceptions 
	 */
	public class FirebaseIncorrectPasswordException : Exception
	{
		/// <summary>
		/// Default constructor
		/// </summary>
		public FirebaseIncorrectPasswordException() : base()
		{
		}

		/// <summary>
		/// Argument constructor
		/// </summary>
		/// <param name="message">This is the description of the exception</param>
		public FirebaseIncorrectPasswordException(String message) : base(message)
		{
		}

		/// <summary>
		/// Argument constructor with inner exception
		/// </summary>
		/// <param name="message">This is the description of the exception</param>
		/// <param name="innerException">Inner exception</param>
		public FirebaseIncorrectPasswordException(String message, Exception innerException) : base(message, innerException)
		{
		}

		/// <summary>
		/// Argument constructor with serialization support
		/// </summary>
		/// <param name="info">Instance of SerializationInfo</param>
		/// <param name="context">Instance of StreamingContext</param>
		protected FirebaseIncorrectPasswordException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}

	public class FirebaseInvalidEmailException : Exception
	{
		/// <summary>
		/// Default constructor
		/// </summary>

		public FirebaseInvalidEmailException() : base()
		{
		}

		/// <summary>
		/// Argument constructor
		/// </summary>
		/// <param name="message">This is the description of the exception</param>

		public FirebaseInvalidEmailException(String message) : base(message)
		{
		}

		/// <summary>
		/// Argument constructor with inner exception
		/// </summary>
		/// <param name="message">This is the description of the exception</param>
		/// <param name="innerException">Inner exception</param>

		public FirebaseInvalidEmailException(String message, Exception innerException) : base(message, innerException)
		{
		}

		/// <summary>
		/// Argument constructor with serialization support
		/// </summary>
		/// <param name="info">Instance of SerializationInfo</param>
		/// <param name="context">Instance of StreamingContext</param>

		protected FirebaseInvalidEmailException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}

	/*
	 * Create User Exceptions
	 */
	public class FirebaseUsedEmailException : Exception
	{
		/// <summary>
		/// Default constructor
		/// </summary>

		public FirebaseUsedEmailException() : base()
		{
		}

		/// <summary>
		/// Argument constructor
		/// </summary>
		/// <param name="message">This is the description of the exception</param>

		public FirebaseUsedEmailException(String message) : base(message)
		{
		}

		/// <summary>
		/// Argument constructor with inner exception
		/// </summary>
		/// <param name="message">This is the description of the exception</param>
		/// <param name="innerException">Inner exception</param>

		public FirebaseUsedEmailException(String message, Exception innerException) : base(message, innerException)
		{
		}

		/// <summary>
		/// Argument constructor with serialization support
		/// </summary>
		/// <param name="info">Instance of SerializationInfo</param>
		/// <param name="context">Instance of StreamingContext</param>

		protected FirebaseUsedEmailException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}

	public class FirebaseWeakPasswordException : Exception
	{
		/// <summary>
		/// Default constructor
		/// </summary>

		public FirebaseWeakPasswordException() : base()
		{
		}

		/// <summary>
		/// Argument constructor
		/// </summary>
		/// <param name="message">This is the description of the exception</param>

		public FirebaseWeakPasswordException(String message) : base(message)
		{
		}

		/// <summary>
		/// Argument constructor with inner exception
		/// </summary>
		/// <param name="message">This is the description of the exception</param>
		/// <param name="innerException">Inner exception</param>

		public FirebaseWeakPasswordException(String message, Exception innerException) : base(message, innerException)
		{
		}

		/// <summary>
		/// Argument constructor with serialization support
		/// </summary>
		/// <param name="info">Instance of SerializationInfo</param>
		/// <param name="context">Instance of StreamingContext</param>

		protected FirebaseWeakPasswordException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}

