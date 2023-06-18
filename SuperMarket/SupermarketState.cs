namespace SuperMarket
{
	public class SupermarketState
	{
		private readonly RequestDelegate _next;

		public SupermarketState(RequestDelegate next)
		{
			_next = next;
		}

		public async Task Invoke(HttpContext context)
		{
			if (IsSupermarketClosed())
			{
				await context.Response.WriteAsync("Supermarket is closed");
				return;
			}

			await _next(context);
		}

		private bool IsSupermarketClosed()
		{
			//Logica para determinar si se encuentra abierto
			return false;
		}
	}
}
