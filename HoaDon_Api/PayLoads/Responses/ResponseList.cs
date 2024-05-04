using System.Net.NetworkInformation;

namespace HoaDon_Api.PayLoads.Responses
{
    public class ResponseList<T>
    {
        public T Data { get; set; }
        public ResponseList() { }
        public ResponseList( T data)
        {
            Data = data;
        }
        public ResponseList<T> ResponseSuccses(T data)
        {
            return new ResponseList<T>(data);
        }

        public ResponseList<T> ResponseError(T data)
        {
            return new ResponseList<T>(data);
        }
    }
}
