namespace Postech.GroupEight.TechChallenge.ContactUpdate.Infra.Http.Interfaces
{
    public interface IHttp
    {
        void On<TRequest, TResponse>(string method, string url, Func<TRequest?, IDictionary<string, object?>, Task<TResponse>> callback, int successfulStatusCode);
        void Run();
    }
}