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
            // Check if maintenance mode is enabled
            if (IsSupermarketOpen())
            {
                await context.Response.WriteAsync("Supermarket is closed");
                return;
            }

            await _next(context);
        }

        private bool IsSupermarketOpen()
        {
            // Implement your own logic here to determine if maintenance mode is enabled
            return false; // Return true for demo purposes
        }
    }
}
