using System.Collections.Generic;

namespace yenoMoG.Armura.Domain.Responses
{
	public class Response<TValue>
	{
		public bool IsFailure => Messages.Count > 0;
		public bool IsSuccess => !IsFailure;
		public TValue Value { get; set; }
		public Dictionary<string, string> Messages { get; }
		private Response(TValue value)
		{
			Value = value;
			Messages = new Dictionary<string, string>();
		}
		public static Response<TValue> Ok(TValue value)
		{
			return new Response<TValue>(value);
		}
		public static Response<TValue> Fail(string key, string message)
		{
			var response = new Response<TValue>(default);
			response.Messages.Add(key, message);
			return response;
		}
	}

	public class Response
	{
		public bool IsFailure => Messages.Count > 0;
		public bool IsSuccess => !IsFailure;
		public Dictionary<string, string> Messages { get; }
		private Response()
		{
			Messages = new Dictionary<string, string>();
		}
		public static Response Ok()
		{
			return new Response();
		}
		public static Response Fail(string key, string message)
		{
			var response = new Response();
			response.Messages.Add(key, message);
			return response;
		}
	}
}
