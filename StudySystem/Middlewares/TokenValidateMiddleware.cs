namespace StudySystem.Middlewares
{
    public class TokenValidateMiddleware
    {
        private readonly ILogger _logger;  
        public TokenValidateMiddleware() { }
        public async Task Invoke(HttpContext context)
        {
            return ;
        }
    }
}
